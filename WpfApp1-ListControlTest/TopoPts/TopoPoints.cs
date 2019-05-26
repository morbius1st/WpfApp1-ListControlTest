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

#endregion


// projname: WpfApp1_ListControlTest.TopoPts
// tpname: TopoPoints
// username: jeffs
// created:  5/21/2019 7:40:37 PM


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
		private const int created       = 0;
		private const int gotStartPoint = 1;
		private const int gotPoints     = 2;
		private const int gotEndPoint   = 3;
		private const int complete      = 4;


		private TopoPoint _startPoint = new TopoPoint(new XYZ(0, 0, 0));
		private TopoPoint _endPoint = new TopoPoint(new XYZ(0, 0, 0));

		private bool[] Status = new bool[complete + 1];

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
			Status[created] = true;
		}

	#endregion

		private void TopoPoints_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (Status[complete])
			{
				int idx = ((TopoPoint) sender).Index;

				if (idx > 0)
				{
					UpdateItem(idx, Items[idx + 1]);
					UpdateItem(idx - 1, Items[idx]);
				}
				else
				{
					UpdateItem(idx, Items[idx + 1]);
					UpdateItem(idx - 1, _endPoint);
				}


				message = "received| "
					+ "  idx| " + idx
					+ "  value| " + ((TopoPoint) sender).ToString()
					+ "  property name| " + e.PropertyName;
			}
		}


	#region > superseding methods

		public new void Add(TopoPoint t) => throw new NotImplementedException();
		public new bool Contains(TopoPoint item) => throw new NotImplementedException();
		public new void Insert(int idx, TopoPoint item) => throw new NotImplementedException();

	#endregion

	#region >> Methods - Done

		public int EndPointIdx => Items.Count - 1;

		public TopoPoint StartPoint => _startPoint;

		public TopoPoint EndPoint => _endPoint;

		// adds the startpoint
		// will supersede a previously provided startpoint
		// distances, etc. are never configured for a startpoint
		public void SetStartPoint(XYZ startPoint)
		{
			if (!startPoint.IsValid) { throw new ArgumentException("Invalid Start Point"); }

			_startPoint = new TopoPoint(startPoint);

			Status[gotStartPoint] = true;

			if (Items.Count > 0)
			{
				ReIndexStart();
				OnPropertyChanged("StartPoint");
			}
		}

		// adds as the endpoint but does not complete the collection
		// may be provided before the startpoint
		// will supersede a previously provided endpoint
		// does not configure the endpoint for distances, etc.
		// may be provided before any points
		public void SetEndPoint(XYZ endPoint)
		{
			if (!endPoint.IsValid) { throw new ArgumentException("Invalid End Point"); }

			_endPoint = new TopoPoint(endPoint);

			if (Items.Count > 0) { Status[gotEndPoint] = true; }

			if (Items.Count > 0)
			{
				ReIndexEnd();
				OnPropertyChanged("EndPoint");
			}
		}

		public void AddDefered(XYZ xyz)
		{
			// validate first
			if (!xyz.IsValid) { throw new InvalidDataException("Add failed - Invalid point"); }

			TopoPoint tp = new TopoPoint(xyz);

			base.Insert(0, tp);

			Status[gotPoints] = true;
		}

		public void Add(XYZ xyz)
		{
			// validate first
			if (!xyz.IsValid) { throw new InvalidDataException("Add failed - Invalid point"); }

			TopoPoint tp = new TopoPoint(xyz);

			base.Insert(0, tp);

			ReIndex();
		}

		public void Insert(int index, XYZ xyz)
		{
			if (!xyz.IsValid) { throw new ArgumentException("Invalid Start Point"); }

			Items.Insert(index, new TopoPoint(xyz));

			ReIndex();
		}

		// completes - endpoint previously set
		public void Complete()
		{
			if (!checkStatus()) { throw new InvalidOperationException("TopoPoints Incomplete"); }

			ReIndex();

			Status[complete] = true;
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

//		// updates the index number for all items
//		// updates the distances for all items
//		private void ReIndex()
//		{
//			// internal method - no checks
//			int j = -1;
//
//			_startPoint.Update(j, Items[j++ + 1]);
//
//			for (; j < Items.Count - 1; j++)
//			{
//				Items[j].Update(j, Items[j + 1]);
//			}
//			Items[j].Update(j, _endPoint);
//
//			_endPoint.Index = j;
//
//			OnCollectionChanged(
//				new NotifyCollectionChangedEventArgs(
//					NotifyCollectionChangedAction.Reset));
//		}


		// updates the index number for all items
		// updates the distances for all items
		private void ReIndex()
		{
			// internal method - no checks
			int j = 0;

			ReIndexEnd();

			for (; j < Items.Count - 1; j++)
			{
				UpdateItem(j, Items[j + 1]);
//				Items[j].PropertyChanged -= TopoPoints_PropertyChanged;
//
//				Items[j].Update(j, Items[j + 1]);
//
//				Items[j].PropertyChanged += TopoPoints_PropertyChanged;
			}

			ReIndexStart();

			OnCollectionChanged(
				new NotifyCollectionChangedEventArgs(
					NotifyCollectionChangedAction.Reset));
		}
		
		private void UpdateItem(int j, TopoPoint tp)
		{
			Items[j].PropertyChanged -= TopoPoints_PropertyChanged;

			Items[j].Update(j, tp);

			Items[j].PropertyChanged += TopoPoints_PropertyChanged;
		}

		private void ReIndexEnd()
		{
			_endPoint.Update(Items.Count, Items[0]);
		}

		private void ReIndexStart()
		{
			UpdateItem(Items.Count -1, _startPoint);

//			int j = Items.Count - 1;
//
//			Items[j].PropertyChanged -= TopoPoints_PropertyChanged;
//
//			Items[j].Update(j, _startPoint);
//
//			Items[j].PropertyChanged += TopoPoints_PropertyChanged;

			_startPoint.Index = -1;
		}

		private bool checkStatus()
		{
			Status[complete] = false;

			for (int i = 0; i < Status.Length - 1; i++)
			{
				if (!Status[i]) return false;
			}

			return true;
		}

		public new void Clear()
		{
			_startPoint.Clear();
			_endPoint.Clear();
			Status[created] = true;

			Items.Clear();
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

//		public new event PropertyChangedEventHandler PropertyChanged;

//		private void OnPropertyChange([CallerMemberName] string memberName = "")
//		{
//			base.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
//		}

//		private void OnPropChange([CallerMemberName] string memberName = "")
//		{
//			OnPropertyChanged(new PropertyChangedEventArgs(memberName));
//		}

		private void OnPropertyChanged([CallerMemberName] string memberName = "")
		{
			base.OnPropertyChanged(new PropertyChangedEventArgs(memberName));
		}


//		protected override void OnPropertyChanged(PropertyChangedEventArgs e)
//		{
//			base.OnPropertyChanged(e);
//		}

//		protected override event PropertyChangedEventHandler PropertyChanged
//		{
//			add => base.PropertyChanged    += value;
//			remove => base.PropertyChanged -= value;
//		}
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