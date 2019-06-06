﻿#region + Using Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

#endregion


// projname: WpfApp1_ListControlTest.TopoPts
// tpname: TopoPoints
// username: jeffs
// created:  5/21/2019 7:40:37 PM


// adjustments
// 1. start using "Initialize(TopoStartPoint)"
//		until this is done, nothing works (at least what I can do);
//		this just placed the XYZ into point
// 2. just have "add()" - this will be deferred automatically until "complete(TopoEndPoint)"
//		this places the XYZ into point
//		this updates all calculated values
//		this assigns property handlers 0 to end
//		changes flag to complete - which allows properties to work
//	3. adjust topopoint to allow revising XYZ at one time to reduce property change events
//		need to adjust XYZ to allow providing one or more values and setting other values to NaN
//	4. eliminate collection property change from propagating property change events 
//		delegate end point to a different event handler??


namespace WpfApp1_ListControlTest.TopoPts
{
	/*
	 * a list of topographic points
	 * the index number of the tp is its sequence in the point array
	 * this means that TopoPoint(s) must be kept in the correct order
	 *
	 * point arrangement
	 *
	 */


	public class TopoPoints : ObservableCollection<TopoPoint> //, INotifyPropertyChanged
	{
		private const int created                 = 0;
		private const int gotStartPoint           = 1;
		private const int gotPoints               = 2;
		private const int gotEndPoint             = 3;
		private const int complete                = 4;
		private const int processPropChangeUpdate = 5;
		private const int batchMode             = 6;
		private const int xyzMode               = 7;
		private const int statusLength            = 8;

		private bool[] Status;

		private string _message;

	#region > constructor

		public TopoPoints()
		{
			// reset and clear everything - prep for new info
			Clear();
		}

	#endregion

	#region > superseding methods

		public new void Add(TopoPoint t) => throw new NotImplementedException();
		public new void Insert(int idx, TopoPoint item) => throw new NotImplementedException();

	#endregion

	#region > Properties

		public string message
		{
			get => _message;
			set
			{
				_message = value;
				OnPropertyChanged("message");
			}
		}

		public string append
		{
			set
			{
				message += value;
				Debug.WriteLine(value);
			}
		}

		public int EndPointIdx => Items.Count - 1;

		public TopoStartPoint StartPoint
		{
			get => Items[0] as TopoStartPoint;
			set => Items[0].XYZ = value.XYZ;
		}

		public XYZ StartXYZ
		{
			get => Items[0].XYZ;
			set => Items[0].XYZ = value;
		}

		public TopoEndPoint EndPoint
		{
			get => Items[EndPointIdx] as TopoEndPoint;
			set => Items[EndPointIdx].XYZ = value.XYZ;
		}

		public XYZ EndXYZ
		{
			get => Items[EndPointIdx].XYZ;
			set => Items[EndPointIdx].XYZ = value;
		}

	#endregion

	#region >> Public Methods

		public void Initialize(TopoStartPoint start)
		{
			Items[0]                 =  start;
			Items[0].PropertyChanged += TopoPoints_PropertyChangedStartPt;

			Status[processPropChangeUpdate] = false;
			Status[gotStartPoint]           = true;
		}

		public void Finalize(TopoEndPoint end)
		{
			if (!CheckStatus(gotEndPoint)) { throw new InvalidOperationException("TopoPoints Not Ready to Finalize"); }

			end.Index = EndPointIdx;

			Items[EndPointIdx]                 =  end;
			Items[EndPointIdx].PropertyChanged += TopoPoints_PropertyChangedEndPt;

			Status[gotEndPoint] = true;

			ReIndex();

			OnPropertyChanged("StartPointIndex");
			OnPropertyChanged("StartPointX");
			OnPropertyChanged("StartPointY");
			OnPropertyChanged("StartPointZ");

			Status[complete] = true;
			Status[processPropChangeUpdate] = true;
		}

		// only allowed after we have a start point
		// reindex only after "complete",
		public void Add(XYZ xyz)
		{
			Insert(EndPointIdx, xyz);
		}

		// only allowed after we have a start point
		// does reindex only after "complete"
		public void Insert(int idx, XYZ xyz)
		{
			if (!Status[gotStartPoint]) { throw new InvalidOperationException("Insert Disallowed Now"); }

			if (!xyz.IsValid) { throw new ArgumentException("Invalid Point"); }

			if (idx <= 0 || idx > EndPointIdx) { throw new ArgumentException("Invalid Index"); }

			base.Insert(idx, new TopoPoint(xyz));

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
			foreach (TopoPoint tp in Items)
			{
				tp.PropertyChanged -= TopoPoints_PropertyChanged;
			}

			ClearItems();

			// clear status
			Status = new bool[statusLength];

			Items.Insert(0, new TopoPoint(XYZ.Empty));
			Items.Insert(1, new TopoPoint(XYZ.Empty));

			UpdateStartPointValues();
			UpdateEndPointValues();

			// default is all = false
			Status[created] = true;
		}

		// the element matching tp
		// then reindex - do this be finding the index of the
		// element and using the remove at routine
		public new bool Remove(TopoPoint tp)
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
			if (index <= 0 || index >= EndPointIdx)  { throw new ArgumentException("Invalid Index"); }

			// remove using the base method to prevent extra events
			base.RemoveAt(index);

			ReIndex(index, false);
		}

		// export the collection members to an array
		public new void CopyTo(TopoPoint[] array, int arrayIndex)
		{
//			Clear();

			Items.CopyTo(array, arrayIndex);
//
//			UpdateStartPointValues();
//			UpdateEndPointValues();
//
//			StatusToAllTrue();
		}

	#endregion

		public void BatchBegin()
		{
			Status[batchMode] = true;
		}

		public void BatchFinalize()
		{
//			int end = last + 1;
//
//			if (first <= 0 || end > EndPointIdx) return;

//			ReIndex(first, false, end);
			ReIndex();

			Status[batchMode] = false;
		}

	#region > Event Handling

		private bool testEventProcessing(string property)
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
			int idx = ((TopoPoint) sender).Index;

			string name = e.PropertyName;

			if (name.Equals("XYZstart")) { Status[xyzMode] = true;}

			if (name.Equals("XYZend"))
			{
				Status[xyzMode] = false;
			}

			bool result = testEventProcessing(name);

#if DEBUG
			DisplayPropChangeInfo(idx, sender, e, "midpt| ", name);
#endif

			if (testEventProcessing(name))
			{
				ReIndex(idx, false);
			}
		}

		// only startpoint property change events
		// this will, depending on the defer flag, 
		// update the start point and the point that follows
		private void TopoPoints_PropertyChangedStartPt(object sender, PropertyChangedEventArgs e)
		{
			int idx = ((TopoPoint) sender).Index;

#if DEBUG
			DisplayPropChangeInfo(idx, sender, e, "startpt", " ");
#endif

			if (testEventProcessing(e.PropertyName))
			{
				UpdateItem(idx + 1, idx);
			}

//			if (e.PropertyName.Equals("XYZ"))
//			{
//				OnPropertyChanged("StartPointIndex");
//				OnPropertyChanged("StartPointX");
//				OnPropertyChanged("StartPointY");
//				OnPropertyChanged("StartPointZ");
//			}
//			else
//			{
				OnPropertyChanged("StartPoint" + e.PropertyName);
//			}
		}

		// only endpoint events
		// this will, depending on the defer flag, 
		// update the end point
		private void TopoPoints_PropertyChangedEndPt(object sender, PropertyChangedEventArgs e)
		{
			int idx = ((TopoPoint) sender).Index;

#if DEBUG
			DisplayPropChangeInfo(idx, sender, e, "endPt ", " ");
#endif

			if (testEventProcessing(e.PropertyName))
			{
				UpdateItem(idx, idx - 1);
			}

			if (e.PropertyName.Equals("XYZ"))
			{
				OnPropertyChanged("EndPointIndex");
				OnPropertyChanged("EndPointX");
				OnPropertyChanged("EndPointY");
				OnPropertyChanged("EndPointZ");
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

		private void StatusToAllTrue()
		{
			for (int i = 0; i < statusLength; i++)
			{
				Status[i] = true;
			}
		}

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
			if (last < 0 || last > EndPointIdx) { throw new ArgumentException("Invalid Last Index"); }

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


			for (i = start; i < j; i++)
			{
				UpdateItem(i, i - 1);
			}

			for (; i < Items.Count; i++)
			{
				Items[i].Index = i;
			}

//			if (start == 0)
//			{
//				OnPropertyChanged("StartPointIndex");
//				OnPropertyChanged("StartPointX");
//				OnPropertyChanged("StartPointY");
//				OnPropertyChanged("StartPointZ");
//			}

			if (j == Items.Count)
			{
				OnPropertyChanged("EndPointIndex");
				OnPropertyChanged("EndPointX");
				OnPropertyChanged("EndPointY");
				OnPropertyChanged("EndPointZ");
			}
		}

		private void UpdateItem(int j, int i)
		{
			if (j > EndPointIdx || i < 0)
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
			append = who + " received| "
				+ "  idx| " + idx
				+ "  property name| " + e.PropertyName
				+ " (" + which + ")"
				+ "  value| " + ((TopoPoint) sender).ToString()
				+ "\n"
				;

//			Debug.Write("received| "
//				+ "  idx| " + idx
//				+ "  property name| " + e.PropertyName
//				+ "  value| " + ((TopoPoint) sender).ToString()
//				+ "\n"
//				);
		}
#endif

	#endregion

	#region > overrides

#if DEBUG

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder("count| " + Items.Count + "\n");

			foreach (TopoPoint tp in Items)
			{
				sb.AppendLine(
					$"idx| {tp.Index     ,4:D}"
					+ $" x| {tp.X        ,10:F2}"
					+ $" y| {tp.Y        ,10:F2}"
					+ $" z| {tp.Z        ,10:F2}"
					+ $" Δx| {tp.XΔ      ,8:F2}"
					+ $" Δy| {tp.YΔ      ,8:F2}"
					+ $" Δz| {tp.ZΔ      ,8:F2}"
					+ $" Δxy| {tp.XYΔ    ,10:F2}"
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

		public void UpdateStartPointValues()
		{
			OnPropertyChanged("StartPointIndex");
			OnPropertyChanged("StartPointX");
			OnPropertyChanged("StartPointY");
			OnPropertyChanged("StartPointZ");
		}


		// end point
		public int EndPointIndex => Items[EndPointIdx].Index;

		public double EndPointX => Items[EndPointIdx].X;
		public double EndPointY => Items[EndPointIdx].Y;
		public double EndPointZ => Items[EndPointIdx].Z;

		public double EndPointXΔ => Items[EndPointIdx].XΔ;
		public double EndPointYΔ => Items[EndPointIdx].YΔ;
		public double EndPointZΔ => Items[EndPointIdx].ZΔ;
		public double EndPointXYΔ => Items[EndPointIdx].XYΔ;

		public double EndPointSlope => Items[EndPointIdx].Slope;

		public bool EndPointCp => Items[EndPointIdx].ControlPoint;

		public void UpdateEndPointValues()
		{
			OnPropertyChanged("EndPointIndex");
			OnPropertyChanged("EndPointX");
			OnPropertyChanged("EndPointY");
			OnPropertyChanged("EndPointZ");
			OnPropertyChanged("EndPointXΔ");
			OnPropertyChanged("EndPointYΔ");
			OnPropertyChanged("EndPointZΔ");
			OnPropertyChanged("EndPointXYΔ");
			OnPropertyChanged("EndPointSlope");
		}

	#endregion
	}
}

//		private class Status2
//		{
//			public enum Stage
//			{
//				CREATED,
//				GOTSTARTPOINT,
//				GOTENDPOINT,
//				COMPLETE,
//				PROCESS_PROP_CHANGE_UPDATES,
//				LENGTH
//			}
//
//			public bool[] status;
//
//			public Status2()
//			{
//				status = new bool[(int) Stage.LENGTH];
//			}
//
//			private int CreatedAsInt => (int) Stage.CREATED;
//			public bool Created
//			{
//				get => status [CreatedAsInt]; 
//				set => status [CreatedAsInt] = value;
//			}
//
//			private int GotStartPointAsInt => (int) Stage.GOTSTARTPOINT;
//			public bool GotStartPoint
//			{
//				get => status[GotStartPointAsInt];
//				set => status[GotStartPointAsInt] = value;
//			}
//
//			private int GotEndPointAsInt => (int) Stage.GOTENDPOINT;
//			public bool GotEndPoint
//			{
//				get => status[GotEndPointAsInt];
//				set => status[GotEndPointAsInt] = value;
//			}
//
//			private int CompleteAsInt => (int) Stage.COMPLETE;
//			public bool Complete
//			{
//				get => status[CompleteAsInt];
//				set => status[CompleteAsInt] = value;
//			}
//
//			private int ProcessPropChangeUpdatesAsInt => (int) Stage.PROCESS_PROP_CHANGE_UPDATES;
//			public bool ProcessPropChangeUpdates
//			{
//				get => status[ProcessPropChangeUpdatesAsInt];
//				set => status[ProcessPropChangeUpdatesAsInt] = value;
//			}
//
//		}