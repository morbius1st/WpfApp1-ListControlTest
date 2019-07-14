#region + Using Directives

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using WpfApp1_ListControlTest.TopoPtsData3.TopoPts3.Support;

#endregion


// projname: WpfApp1_ListControlTest.TopoPts
// name: TopoPoints
// username: jeffs
// created:  5/21/2019 7:40:37 PM

namespace WpfApp1_ListControlTest.TopoPtsData3.TopoPts3
{
	/*
	 * a list of topographic points
	 * the index number of the tpt is its sequence in the point array
	 * this means that index must be updated for each modification
	 * to the array (reindexed)
	 *
	 * point arrangement
	 * 0 = startpoint
	 * # = intermediate topo points
	 * EndIdx = endpoint
	 *
	 *
	 * delegates:
	 *
	 * UpdateFirstItem
	 * UpdateMiddleItem
	 * UpdateLastItem
	 *
	 * this allows the data in the array to be revised when the
	 * array is modified / reindexed
	 *
	 *
	 */

	public class TopoPoints3 : ObservableCollection<TopoPoint3>
	{
		private const int created = 0;
		private const int gotStartPoint = 1;
		private const int gotPoints = 2;
		private const int gotEndPoint = 3;
		private const int complete = 4;
		private const int processPropChangeUpdate = 5;
		private const int batchMode = 6;
		private const int xyzMode = 7;
		private const int statusLength = 8;

		private bool[] Status;

		private string _message;

		private AftrReindexItem aftrReindexItem;



	#region > deletages

		public delegate void AftrReindexItem(int idx, TopoPoint3 precedingTpt3);


	#endregion


	#region > constructor

		public TopoPoints3()
		{
			// reset and clear everything - prep for new info
			Clear();
		}

	#endregion

	#region > superseding methods

		public new void Add(TopoPoint3 t) => throw new NotImplementedException();
		public new void Insert(int idx, TopoPoint3 item) => throw new NotImplementedException();

	#endregion

	#region > Properties

	#if DEBUG

		public string Message
		{
			get => _message;
			set
			{
				_message = value;
				OnPropertyChanged("Message");
			}
		}

		public string Append
		{
			set
			{
				Message += value;
				Debug.Write(value);
			}
		}
	#endif

		public int EndIdx => Items.Count - 1;

		public AftrReindexItem AfterReindexItem
		{
			get => aftrReindexItem;
			set => aftrReindexItem = value;
		}

	#endregion

	#region >> Public Methods

		// start the process of creating a topopoints collection
		public void Initialize(TopoStartPoint start)
		{
			Append = "\ninit | @ TopoPoints3| *** Initialize ***\n";

			base.SetItem(0, start);

			Items[0].PropertyChanged += TopoPoints_PropertyChangedStartPt;

			Status[processPropChangeUpdate] = false;
			Status[gotStartPoint] = true;
		}

		// end the initial process of creating a topopoints collection
		public void Finalize(TopoEndPoint end)
		{
			if (!CheckStatus(gotEndPoint)) { throw new InvalidOperationException("TopoPoints Not Ready to Finalize"); }

			end.Index = EndIdx;

			base.SetItem(EndIdx, end);

			Items[EndIdx].PropertyChanged += TopoPoints_PropertyChangedEndPt;

			Status[gotEndPoint] = true;

			ReIndex();

			Status[complete] = true;
			Status[processPropChangeUpdate] = true;

			Append = "\nfin  | @ TopoPoints3| *** Finalize ***\n";
		}

		// only allowed after we have a start point
		// reindex only after "complete",
		public void Add(XYZ3 xyz)
		{
			Insert(EndIdx, xyz);
		}

		// only allowed after we have a start point
		// does reindex only after "complete"
		public void Insert(int idx, XYZ3 xyz)
		{
			if (!Status[gotStartPoint]) { throw new InvalidOperationException("Insert Disallowed Now"); }

			if (!xyz.IsValid) { throw new ArgumentException("Invalid Point"); }

			if (idx <= 0 || idx > EndIdx) { throw new ArgumentException("Invalid Index"); }

			Append = "\ninsrt| @ TopoPoints3| insert | idx| [" + idx + "]";

			base.Insert(idx, (new TopoPoint3(xyz)));

			Items[idx].PropertyChanged += TopoPoints_PropertyChanged;

			Status[gotPoints] = true;

			if (Status[complete] && !Status[batchMode])
			{
				Status[processPropChangeUpdate] = false;
				ReIndex(idx, false);
				Status[processPropChangeUpdate] = true;
			}
		}

		// clear the whole list to start anew
		public new void Clear()
		{
			foreach (TopoPoint3 tp in Items)
			{
				tp.PropertyChanged -= TopoPoints_PropertyChanged;
			}

			base.ClearItems();

			// clear status
			Status = new bool[statusLength];

//			Items.Insert(0, (new TopoEndPoint(new XYZ3(0, 0, 0)){Index = 1}));
//			Items.Insert(0, (new TopoStartPoint(new XYZ3(0, 0, 0)){Index = 0}));

			Items.Insert(0, (new TopoPoint3(XYZ3.Empty) {Index = 1}));
			Items.Insert(0, (new TopoPoint3(XYZ3.Empty) {Index = 0}));

			
			// issue events
			UpdateStartPointValues();
			UpdateEndPointValues();

			// default is all = false
			Status[created] = true;
		}

		// the element matching tp
		// then reindex - do this be finding the index of the
		// element and using the remove at routine
		public new bool Remove(TopoPoint3 tp)
		{
			int result = IndexOf(tp);

			if (result < 0) return false;

			RemoveAt(result);

			return true;
		}

		// remove the item at => index
		// once removed update the list as needed
		// index cannot be <= 0 (cannot remove start or before)
		// index cannot be >= endpointidx (cannot remove end or beyond)
		public new void RemoveAt(int index)
		{
			if (index <= 0 || index >= EndIdx) { throw new ArgumentException("Invalid Index"); }

			// remove using the base method to prevent extra events
			base.RemoveAt(index);

			ReIndex(index, false);
		}

		// export the collection members to an array
		public new void CopyTo(TopoPoint3[] array, int arrayIndex)
		{
			Items.CopyTo(array, arrayIndex);
		}

		public void BatchBegin()
		{
			Append = "\nbatch| @ TopoPoints3| *** batch begin ***\n";

			Status[batchMode] = true;
		}

		public void BatchFinalize()
		{
			ReIndex();

			Status[batchMode] = false;

			Append = "\nbatch| @ TopoPoints3| *** batch finalize ***\n";
		}

	#endregion

	#region > Event Handling

		private bool eventProcessingTest(string property)
		{
			return
				((property.Equals("X") ||
				property.Equals("Y") ||
				property.Equals("Z")
				||
				property.Equals("XYZend")
				)
				&&
				(Status[processPropChangeUpdate] &&
				!Status[batchMode] &&
				!Status[xyzMode])
				);
		}

		// standard event handler for all points except start and end points
		// this will, depending on the defer flag, update the current
		// point and the point that follows
		private void TopoPoints_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			int idx = ((TopoPoint3) sender).Index;

			string name = e.PropertyName;

			if (name.Equals("Message"))
			{
				Message = ((TopoPoint3) sender).Message;
				return;
			}

			if (name.Equals("XYZstart")) { Status[xyzMode] = true; }

			if (name.Equals("XYZend"))
			{
				Status[xyzMode] = false;
			}

		#if DEBUG

			bool result = eventProcessingTest(name);

			DisplayPropChangeInfo(idx, sender, e, "@ TopoPoints3| @propChange| midpt|", name);

		#endif

			if (eventProcessingTest(name))
			{
#if DEBUG
				Append = "     | @ TopoPoints3| @propChange| pre-reindex\n\n";
  #endif

				ReIndex(idx, false);
			}

#if DEBUG
			Append = "     | @ TopoPoints3| @propChange| post-reindex\n\n";
  #endif
		}

		// only startpoint property change events
		// this will, depending on the defer flag, 
		// update the start point and the point that follows
		private void TopoPoints_PropertyChangedStartPt(object sender, PropertyChangedEventArgs e)
		{
			int idx = ((TopoPoint3) sender).Index;

		#if DEBUG
			DisplayPropChangeInfo(idx, sender, e, "startpt", e.PropertyName);
		#endif

			if (eventProcessingTest(e.PropertyName))
			{
				UpdateItem(idx + 1, idx);
			}

			OnPropertyChanged("StartPoint" + e.PropertyName);
		}

		// only endpoint property change events
		// this will, depending on the defer flag, 
		// update the end point
		private void TopoPoints_PropertyChangedEndPt(object sender, PropertyChangedEventArgs e)
		{
			int idx = ((TopoPoint3) sender).Index;

		#if DEBUG
			DisplayPropChangeInfo(idx, sender, e, "@ topopoints3| endPt ", e.PropertyName);
		#endif

			if (eventProcessingTest(e.PropertyName))
			{
				UpdateItem(idx, idx - 1);
			}

			if (e.PropertyName.Equals("XYZ"))
			{
				UpdateEndPointBaseValues();
			}
			else
			{
				OnPropertyChanged("EndPoint" + e.PropertyName);
			}
		}

		private void OnPropertyChanged([CallerMemberName] string memberName = "")
		{
			base.OnPropertyChanged(new PropertyChangedEventArgs(memberName));
		}

	#endregion

	#region > Utility Functions

		private bool CheckStatus(int statusLevel)
		{
			Status[complete] = false;

			for (int i = 0; i < statusLevel; i++)
			{
				if (!Status[i]) return false;
			}

			return true;
		}

		// reindex + update items starting from index, using index - 1
		// depending on all:
		// all == false, update index + 1 using index
		// all = true, update the remainder of the list
		private void ReIndex(int start = 0, bool all = true, int last = 0)
		{

			if (last < 0 || last > EndIdx) { throw new ArgumentException("Invalid Last Index"); }

			int i;
			int j;

			if (last == 0)
			{
				j = Items.Count;

				if (!all)
				{
					j = start + 2;

					j = j > Items.Count ? Items.Count : j;
				}
			}
			else
			{
				j = last + 1;
			}

			Append = "\nreidx| @ TopoPoints3| pre-reindex| "
				+ " start| " + start
				+ " end| " + (j - 1)
				+ "\n\n"
				;

			// this updates and reindexes
			for (i = start; i < j; i++)
			{
				UpdateItem(i, i - 1);
			}

			// i think this never runs
			for (; i < Items.Count; i++)
			{
				Items[i].Index = i;
			}

			if (start == 0)
			{
				// send events for start point values
				UpdateStartPointValues();
			}

			if (j == Items.Count)
			{
				// send events for end point values
				UpdateEndPointBaseValues();
			}

			Append = "\nreidx| @ TopoPoints3| post-reindex| "
				+ "\n"
				;
		}

		private void UpdateItem(int idx, int precedingIdx)
		{
			// do not index when not possible
			// also prevents the update of [0] as 
			// precedingIdx will be less than 0
			if (idx > EndIdx || precedingIdx < 0)
			{
				return;
			}

			Items[idx].Update(idx, Items[precedingIdx]);

			aftrReindexItem?.Invoke(idx, Items[precedingIdx]);

		}

	#if DEBUG
		private void DisplayPropChangeInfo(int idx,
			object sender, PropertyChangedEventArgs e, string who,
			string which
			)
		{
			Append = "(get)| " + who
				+ " idx| " + idx
				+ "  property name| " + e.PropertyName
				+ " (" + which + ")"
//				+ "  value| " 
//				+ ((TopoPoint3) sender).ToString()
				+ "\n"
				;
		}
	#endif

	#endregion

	#region > Overrides

	#if DEBUG

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder("count| " + Items.Count + "\n");

			foreach (TopoPoint3 tp in Items)
			{
				sb.AppendLine(
					$"idx| {tp.Index,4:D}"
					+ $" x| {tp.X,10:F2}"
					+ $" y| {tp.Y,10:F2}"
					+ $" z| {tp.Z,10:F2}"
					+ $" Δx| {tp.XΔ,8:F2}"
					+ $" Δy| {tp.YΔ,8:F2}"
					+ $" Δz| {tp.ZΔ,8:F2}"
					+ $" Δxy| {tp.XYΔ,10:F2}"
					+ $" slope| {tp.Slope,10:F7}"
					);
			}

			return sb.ToString();
		}
	#endif

	#endregion

	#region > start and end point properties

		// start point
		public int StartPointIndex => Items[0].Index;

		public double StartPointX => Items[0].X;
		public double StartPointY => Items[0].Y;
		public double StartPointZ => Items[0].Z;

		public bool StartPointCp => Items[0].ControlPoint;

		private void UpdateStartPointValues()
		{
			OnPropertyChanged("StartPointCp");
			OnPropertyChanged("StartPointIndex");
			OnPropertyChanged("StartPointX");
			OnPropertyChanged("StartPointY");
			OnPropertyChanged("StartPointZ");
		}


		// end point
		public int EndPointIndex => Items[EndIdx].Index;

		public double EndPointX => Items[EndIdx].X;
		public double EndPointY => Items[EndIdx].Y;
		public double EndPointZ => Items[EndIdx].Z;

		public XYZ3 EndPointXYZ => Items[EndIdx].XYZ;

		public double EndPointXΔ => Items[EndIdx].XΔ;
		public double EndPointYΔ => Items[EndIdx].YΔ;
		public double EndPointZΔ => Items[EndIdx].ZΔ;
		public double EndPointXYΔ => Items[EndIdx].XYΔ;
		public double EndPointXYZΔ => Items[EndIdx].XYZΔ;

		public double EndPointSlope => Items[EndIdx].Slope;

		public bool EndPointCp => Items[EndIdx].ControlPoint;

		private void UpdateEndPointBaseValues()
		{
			OnPropertyChanged("EndPointIndex");
			OnPropertyChanged("EndPointCp");
			OnPropertyChanged("EndPointX");
			OnPropertyChanged("EndPointY");
			OnPropertyChanged("EndPointZ");
		}

		private void UpdateEndPointValues()
		{
			UpdateEndPointBaseValues();
			OnPropertyChanged("EndPointXΔ");
			OnPropertyChanged("EndPointYΔ");
			OnPropertyChanged("EndPointZΔ");
			OnPropertyChanged("EndPointXYΔ");
			OnPropertyChanged("EndPointSlope");
		}

	#endregion
	}
}