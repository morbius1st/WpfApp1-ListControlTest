#region + Using Directives

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
		private const int length                  = processPropChangeUpdate + 1;

		private bool[] Status;

		private string _message;

		public string message
		{
			get => _message;
			set
			{
				_message = value;
				OnPropertyChanged("message");
			}
		}

	#region > constructor

		public TopoPoints()
		{
			// reset and clear everything - prep for new info
			Clear();
		}

	#endregion

		// standard event handler for all points except start and end points
		// this will, depending on the defer flag, update the current
		// point and the point that follows
		private void TopoPoints_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			int idx = ((TopoPoint) sender).Index;

			DisplayPropChangeInfo(idx, sender, e, "midpt  ");

			if (e.PropertyName.Equals("X") ||
				e.PropertyName.Equals("Y") ||
				e.PropertyName.Equals("Z") ||
				e.PropertyName.Equals("XYZ") 
				&& Status[processPropChangeUpdate]
				)
			{
				UpdateItem(idx, idx - 1);
				UpdateItem(idx + 1, idx);
			}
		}

		// only startpoint property change events
		// this will, depending on the defer flag, 
		// update the start point and the point that follows
		private void TopoPoints_PropertyChangedStartPt(object sender, PropertyChangedEventArgs e)
		{
			int idx = ((TopoPoint) sender).Index;

			DisplayPropChangeInfo(idx, sender, e, "startpt");

			if (e.PropertyName.Equals("X") ||
				e.PropertyName.Equals("Y") ||
				e.PropertyName.Equals("Z") ||
				e.PropertyName.Equals("XYZ")
				&& Status[processPropChangeUpdate]
				)
			{
				UpdateItem(idx + 1, idx);
			}

			if (e.PropertyName.Equals("XYZ"))
			{
				OnPropertyChanged("StartPointIndex");
				OnPropertyChanged("StartPointX");
				OnPropertyChanged("StartPointY");
				OnPropertyChanged("StartPointZ");
			}
			else
			{
				OnPropertyChanged("StartPoint" + e.PropertyName);
			}
		}

		// only endpoint events
		// this will, depending on the defer flag, 
		// update the end point
		private void TopoPoints_PropertyChangedEndPt(object sender, PropertyChangedEventArgs e)
		{
			int idx = ((TopoPoint) sender).Index;

			DisplayPropChangeInfo(idx, sender, e, "endPt  ");

			if (e.PropertyName.Equals("X") ||
				e.PropertyName.Equals("Y") ||
				e.PropertyName.Equals("Z") ||
				e.PropertyName.Equals("XYZ")
				&& Status[processPropChangeUpdate]
				)
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


		private void DisplayPropChangeInfo(int idx, 
			object sender, PropertyChangedEventArgs e, string who)
		{

			message += who + " received| "
				+ "  idx| " + idx
				+ "  property name| " + e.PropertyName
				+ "  value| " + ((TopoPoint) sender).ToString()
				+ "\n"
				;

			Debug.Write("received| "
				+ "  idx| " + idx
				+ "  property name| " + e.PropertyName
				+ "  value| " + ((TopoPoint) sender).ToString()
				+ "\n"
				);
		}



	#region > superseding methods

		public new void Add(TopoPoint t) => throw new NotImplementedException();
		public new bool Contains(TopoPoint item) => throw new NotImplementedException();
		public new void Insert(int idx, TopoPoint item) => throw new NotImplementedException();

	#endregion

	#region >> Methods - Done

		public int EndPointIdx => Items.Count - 1;

		public void Initialize(TopoStartPoint start)
		{
			Items[0] = start;
			Items[0].PropertyChanged += TopoPoints_PropertyChangedStartPt;
			
			Status[processPropChangeUpdate] = false;
			Status[gotStartPoint] = true;
		}


		public void Finalize(TopoEndPoint end)
		{
			if (!CheckStatus(gotEndPoint)) { throw new InvalidOperationException("TopoPoints Not Ready to Finalize"); }

			end.Index = EndPointIdx;

			Items[EndPointIdx] = end;
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

			if (idx <= 0 || idx > EndPointIdx) { throw new ArgumentException("Invalid Insert Index"); }

			base.Insert(idx, new TopoPoint(xyz));

			Items[idx].PropertyChanged += TopoPoints_PropertyChanged;

			Status[gotPoints] = true;

			if (Status[complete])
			{
				Status[processPropChangeUpdate] = false;
				ReIndex(idx, false);
				Status[processPropChangeUpdate] = true;
			}
		}

		// determine if the point exists in the collection
		public bool Contains(XYZ xyz)
		{
			bool result = false;

			foreach (TopoPoint tp in Items)
			{
				result = tp.XYZ.Equals(xyz);
				if (result) break;
			}

			return result;
		}

		private void ReIndex(int start = 0, bool all = true)
		{
			int i;
			int j = Items.Count;

			if (!all)
			{
				j = start + 2;

				j = j > Items.Count ? Items.Count : j;
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


//		private void ReIndex2(int start = 1)
//		{
//			for (int j = start; j < Items.Count; j++)
//			{
//				UpdateItem(j, j - 1);
//			}
//		}

		private void UpdateItem(int j, int i)
		{
			if (j > EndPointIdx || i < 0)
			{
				return;
			}

			Items[j].Update(j, Items[i]);
		}

		// everything upto statusLevel, exclusive, must be true
		private bool CheckStatus(int statusLevel)
		{
			Status[complete] = false;

			for (int i = 0; i < statusLevel; i++)
			{
				if (!Status[i]) return false;
			}

			return true;
		}

		public new void Clear()
		{
			foreach (TopoPoint tp in Items)
			{
				tp.PropertyChanged -= TopoPoints_PropertyChanged;
			}

			// clear status
			Status = new bool[length];

			Items.Insert(0, new TopoPoint(XYZ.Empty));
			Items.Insert(1, new TopoPoint(XYZ.Empty));

			Status[created] = true;
		}

	#endregion

		public new bool Remove(TopoPoint tp)
		{
			return Items.Remove(tp);
		}

		public new void RemoveAt(int index)
		{
			Items.RemoveAt(index);
		}

		public new void CopyTo(TopoPoint[] array, int arrayIndex)
		{
			Items.CopyTo(array, arrayIndex);
		}


		public bool IsReadOnly => Items.IsReadOnly;

		public new int IndexOf(TopoPoint item)
		{
			return Items.IndexOf(item);
		}

		private void OnPropertyChanged([CallerMemberName] string memberName = "")
		{
			base.OnPropertyChanged(new PropertyChangedEventArgs(memberName));
		}

	#region > start and end point properties

		// start point
		public int StartPointIndex => Items[0].Index;

		public double StartPointX => Items[0].X;
		public double StartPointY => Items[0].Y;
		public double StartPointZ => Items[0].Z;

		public bool StartPointCp => Items[0].ControlPoint;

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

	#endregion
	}
}


// SAVED - this update the index in reverse order
//
//// updates the index number for all items
//// updates the distances for all items
//private void ReIndex()
//{
//	// internal method - no checks
//	int i = Items.Count;
//	int j = -1;
//
//	_startPoint.Update(i--, Items[j++ + 1]);
//
//	for (; j < Items.Count - 1; j++)
//	{
//		Items[j].Index = i;
//		Items[j].Update(i--, Items[j + 1]);
//	}
//
//	Items[j].Update(i--, _endPoint);
//
//	_endPoint.Index = i;
//
//	OnCollectionChanged(
//		new NotifyCollectionChangedEventArgs(
//			NotifyCollectionChangedAction.Reset));
//}

/*
 * CANCEL THE BELOW - change in system:
 * just add the info as needed and then always ReIndex
 *
 *
 * check 0: note: all xyz points must be unique
 * additional checks to the below:
 *  at other than start, xyz != start
 *  at other than end, xyz != end
 *  otherwise, xyz cannot exist in the collection
 *
 ** add start (xyz) -> add the start point with no prior point
 *  checks: xyz.IsValid, collection is new / empty / undefined,
 * initialized = false, check 0 == true
 *  
 *  set startpoint as topopoint / no prior point
 *  set initialized = got start point (remainder false)
 *  set endpoint as NaN
 *
 ** add endpoint (xyz) -> add the end point but does not complete / close the collection
 *
 *  no prior point & maybe, no points
 *     checks: xyz.IsValid, IsInitialized == got startpoint
 *       check 0 == true;
 *     1. make endpoint with no end point
 *     2. initialized == got endpoint
 *
 ** Complete(xyz)
 *
 *  adds the point as the endpoint and *** completes the collection,
 *       cannot 'add(xyz)' after this
 *     checks:xyz.IsValid, IsInitialized == got points
 *      check 0 == true;
 *     
 *    1. make endpoint with [[].count -1] as prior point
 *    2. initialized == complete
 *
 *
 ** add(xyz) -> add a new point to the begging of the current list
 *  checks: start point provided, xyz.IsValid, check 0 == true
 *  uses: [].count -1 as prior point
 *  sets IsInitialized = got points
 *
 *
 ** insert(xyz, idx) -> add a new point at the indexed location
 *  checks: start point provided, index < [].count && > = 0,
 *		xyz.IsValid, check 0 == true;
 *
 *  A. index = x = 0
 *   1. insert @ [0] using start as prior point
 *   2. update [x+1] using [x] as the prior point
 *
 *  B. index = x == [].count - 1 (last item)
 *   1. insert @ [x] using [x-1] as prior point
 *   2. update end point using [x] as the prior point
 *
 *  C. index = x
 *   process as: insert(xyz, x)
 *   1. insert @ [x] using x-1 as prior point - [x] becomes the new point
 *   2. update [x+1] using [x] as the prior point
 *
 *
 ** remove(xyz) -> remove an existing point
 *  checks: xyz.IsValid, initialized >= got points,
 *   xyz exists == true or xyz == start or xyz == end
 *
 *  A. xyz == start
 *   1. this would mean reset to the whole collection so this is not
 *     allowed.
 *   2.  throw error
 *
 *  B. xyz == end
 *   1. this is only OK if initialized == got endpoint
 *   2. end point is reset
 *   3. initialized = got startpoint
 *
 *  C. find index = x, x == [].count - 1 (last item)
 *   1. update endpoint using [x-1]
 *   2. remove [x]
 *
 *  D. find index = x
 *    process as: private remove(x)
 *
 *
 ** removeat(x) -> remove an existing point
 *   checks: x exists == true
 *
 *   A. x == [].count - 1 (last item) && [].count > 1
 *    1. update endpoint using [x-1]
 *    2. remove[x]
 *
 *   B. x == !last item && [].count > 1
 *    1. process as remove(x)
 *
 *   C. x != last item && [].count = 1
 *    1. update endpoint using startpoint
 *    2. remove [x]
 *
 *
 *
 ** move(new idx, xyz) -> moves xyz to idx position
 *  checks: xyz.IsValid, initialized >= got points, xyz exists == true
 *    idx < [].count, xyz != start, xyz != end
 *
 *  ** find index = x
 *  process as: private move(x, new idx)
 *
 * move(x, new idx)
 *  checks: initialized >= got points, x != idx, x >= 0, x < [].count,
 *    idx >= 0, idx < [].count
 *
 *  process as: private move (x, new idx)
 *
 *
 * clear() == reset

 *
 *
 * private insert(x, xyz)
 *   1. insert @ [x] using x-1 as prior point - [x] becomes the new point
 *   2. update [x+1] using [x] as the prior point
 *
 * private remove(x) 
 *   1. update [x+1] using prior point [x-1]
 *   2. remove[x]
 *
 * private move(new idx, x)
 *  1. insert([x].xyz, new idx)
 *  2. private remove([x])
 *
 *
 *
 *
 *
 *
 */