#region + Using Directives

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using WpfApp1_ListControlTest.TopoPtsData2.Support;

#endregion


// projname: WpfApp1_ListControlTest.TopoPts
// tpname: TopoPoints
// username: jeffs
// created:  5/21/2019 7:40:37 PM

namespace WpfApp1_ListControlTest.TopoPtsData2
{
	/*
	 * a list of topographic points
	 * the index number of the tp is its sequence in the point array
	 * this means that TopoPoint(s) must be kept in the correct order
	 *
	 * point arrangement
	 * 0 = startpoint
	 * # = intermediate topo points
	 * EndIdx = endpoint
	 *
	 */

	public class TopoPoints2 : ObservableCollection<TopoPoint2>
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

	#region > constructor

		public TopoPoints2()
		{
			// reset and clear everything - prep for new info
			Clear();
		}

	#endregion

	#region > superseding methods

		public new void Add(TopoPoint2 t) => throw new NotImplementedException();
		public new void Insert(int idx, TopoPoint2 item) => throw new NotImplementedException();

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

	#endregion

	#region >> Public Methods

		// start the process of creating a topopoints collection
		public void Initialize(TopoStartPoint start)
		{
			Items[0] = start;
			Items[0].PropertyChanged += TopoPoints_PropertyChangedStartPt;

			Status[processPropChangeUpdate] = false;
			Status[gotStartPoint] = true;
		}

		// end the initial process of creating a topopoints collection
		public void Finalize(TopoEndPoint end)
		{
			if (!CheckStatus(gotEndPoint)) { throw new InvalidOperationException("TopoPoints Not Ready to Finalize"); }

			end.Index = EndIdx;

			Items[EndIdx] = end;
			Items[EndIdx].PropertyChanged += TopoPoints_PropertyChangedEndPt;

			Status[gotEndPoint] = true;

			ReIndex();

			Status[complete] = true;
			Status[processPropChangeUpdate] = true;
		}

		// only allowed after we have a start point
		// reindex only after "complete",
		public void Add(XYZ2 xyz)
		{
			Insert(EndIdx, xyz);
		}

		// only allowed after we have a start point
		// does reindex only after "complete"
		public void Insert(int idx, XYZ2 xyz)
		{
			if (!Status[gotStartPoint]) { throw new InvalidOperationException("Insert Disallowed Now"); }

			if (!xyz.IsValid) { throw new ArgumentException("Invalid Point"); }

			if (idx <= 0 || idx > EndIdx) { throw new ArgumentException("Invalid Index"); }

			base.Insert(idx, new TopoPoint2(xyz));

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
			foreach (TopoPoint2 tp in Items)
			{
				tp.PropertyChanged -= TopoPoints_PropertyChanged;
			}

			base.ClearItems();

			// clear status
			Status = new bool[statusLength];

			Items.Insert(0, new TopoPoint2(XYZ2.Empty));
			Items.Insert(1, new TopoPoint2(XYZ2.Empty));

			UpdateStartPointValues();
			UpdateEndPointValues();

			// default is all = false
			Status[created] = true;
		}

		// the element matching tp
		// then reindex - do this be finding the index of the
		// element and using the remove at routine
		public new bool Remove(TopoPoint2 tp)
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
		public new void CopyTo(TopoPoint2[] array, int arrayIndex)
		{
			Items.CopyTo(array, arrayIndex);
		}

		public void BatchBegin()
		{
			Status[batchMode] = true;
		}

		public void BatchFinalize()
		{
			ReIndex();

			Status[batchMode] = false;
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
			int idx = ((TopoPoint2) sender).Index;

			string name = e.PropertyName;

			if (name.Equals("Message"))
			{
				Message = ((TopoPoint2) sender).Message;
				return;
			}

			if (name.Equals("XYZstart")) { Status[xyzMode] = true; }

			if (name.Equals("XYZend"))
			{
				Status[xyzMode] = false;
			}

		#if DEBUG

			bool result = eventProcessingTest(name);

			DisplayPropChangeInfo(idx, sender, e, "@ TopoPoints2| @propChange| midpt| ", name);

		#endif

			if (eventProcessingTest(name))
			{
				Append = "     | @ TopoPoints2| @propChange| pre-reindex\n\n";

				ReIndex(idx, false);
			}

			Append = "     | @ TopoPoints2| @propChange| post-reindex\n\n";
		}

		// only startpoint property change events
		// this will, depending on the defer flag, 
		// update the start point and the point that follows
		private void TopoPoints_PropertyChangedStartPt(object sender, PropertyChangedEventArgs e)
		{
			int idx = ((TopoPoint2) sender).Index;

		#if DEBUG
			DisplayPropChangeInfo(idx, sender, e, "startpt", " ");
		#endif

			if (eventProcessingTest(e.PropertyName))
			{
				UpdateItem(idx + 1, idx);
			}

			OnPropertyChanged("StartPoint" + e.PropertyName);
		}

		// only endpoint events
		// this will, depending on the defer flag, 
		// update the end point
		private void TopoPoints_PropertyChangedEndPt(object sender, PropertyChangedEventArgs e)
		{
			int idx = ((TopoPoint2) sender).Index;

		#if DEBUG
			DisplayPropChangeInfo(idx, sender, e, "endPt ", " ");
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

			Append = "reidx| @ TopoPoints2| pre-reindex| "
				+ " start| " + start
				+ " end| " + j
				+ "\n\n"
				;

			for (i = start; i < j; i++)
			{
				UpdateItem(i, i - 1);
			}

			for (; i < Items.Count; i++)
			{
				Items[i].Index = i;
			}

			if (start == 0)
			{
				UpdateStartPointValues();
			}

			if (j == Items.Count)
			{
				UpdateEndPointBaseValues();
			}

			Append = "reidx| @ TopoPoints2| post-reindex| "
				+ "\n"
				;
		}

		private void UpdateItem(int j, int i)
		{
			if (j > EndIdx || i < 0)
			{
				return;
			}

			Items[j].Update(j, Items[i]);
		}

	#if DEBUG
		private void DisplayPropChangeInfo(int idx,
			object sender, PropertyChangedEventArgs e, string who,
			string which
			)
		{
			Append = "(get)| " + who
				+ "  idx| " + idx
				+ "  property name| " + e.PropertyName
				+ " (" + which + ")"
//				+ "  value| " 
//				+ ((TopoPoint2) sender).ToString()
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

			foreach (TopoPoint2 tp in Items)
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

		public XYZ2 EndPointXYZ => Items[EndIdx].XYZ;

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