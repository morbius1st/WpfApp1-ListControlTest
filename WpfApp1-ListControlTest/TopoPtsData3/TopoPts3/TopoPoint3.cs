#region + Using Directives

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UtilityLibrary;
//using WpfApp1_ListControlTest.TopoPts.Support;
using WpfApp1_ListControlTest.TopoPtsData3.Support;
using WpfApp1_ListControlTest.TopoPtsData3.TopoPts3.Support;

#endregion


// projname: WpfApp1_ListControlTest.TopoPts
// itemname: TopoPoint
// username: jeffs
// created:  5/21/2019 7:41:18 PM


namespace WpfApp1_ListControlTest.TopoPtsData3.TopoPts3
{
#region	> special Topopoints

	public class TopoStartPoint : TopoPoint3
	{
		public TopoStartPoint(XYZ3 xyz)
		{
			if (!xyz.IsValid) { throw new ArgumentException("Invalid Start Point"); }

			base.index = 0;
			base.point = xyz;
			base.controlPoint = true;
		}
	}

	public class TopoEndPoint : TopoPoint3
	{
		public TopoEndPoint(XYZ3 xyz)
		{
			if (!xyz.IsValid) { throw new ArgumentException("Invalid End Point"); }

			base.index = -1;
			base.point = xyz;
			base.controlPoint = true;
		}
	}
#endregion

	public class TopoPoint3 : IEquatable<TopoPoint3>, ICloneable, INotifyPropertyChanged
	{
	#region > internal fields

		// the index number for this item
		// and the list index number
		protected int index = -1;

		// flag - this TopoPoint is being edited
		private bool isBeingEdited = false;

		// management: is this a control point (start or end points)
		protected bool controlPoint = false;

		// the point's XYZ coordinates
		protected XYZ3 point = XYZ3.Empty;

		// the X or Y or Z distance change 
		// for the current point
		// from the prior point
		private double _xΔ = Double.NaN;
		private double _yΔ = Double.NaN;
		private double _zΔ = Double.NaN;

		// the XY distance change
		// for the current point
		// from the prior point
		private double _xyΔ = Double.NaN;

		// the XYZ distance change
		// for the current point
		// from the prior point
		private double _xyzΔ = Double.NaN;

		// the Slope
		// for the current point
		// from the prior point
		private double _slope = Double.NaN;

	#endregion

	#region > constructors

		public TopoPoint3()
		{
			point.PropertyChanged += PointPropertyChanged;
		}

		public TopoPoint3(XYZ3 xyz)
		{
			point = xyz;

			point.PropertyChanged += PointPropertyChanged;
		}

	#endregion

	#region > public properties

		// update XY and / or Z all at one time
		// this reduces the number of events 
		// will make the ui a bit faster
		public XYZ3 XYZ
		{
			get { return point; }

			// not used now but can be used
			set
			{
				if (!value.Equals(point))
				{
					OnPropertyChange("XYZstart");

					if (!double.IsNaN(value.X))
					{
						point.X = value.X;
						OnPropertyChange("X");
					}

					if (!double.IsNaN(value.Y))
					{
						point.Y = value.Y;
						OnPropertyChange("Y");
					}

					if (!double.IsNaN(value.Z))
					{
						point.Z = value.Z;
						OnPropertyChange("Z");
					}

					OnPropertyChange("XYZend");
				}
			}
		}

		// XYZ3 values can be set individually
		// however, since there is no prior point
		// cannot re-calculate values
		// just cause an event when changed
		public double X
		{
			get => point.X;
			set
			{
				if (!value.Equals(point.X))
				{
					Debug.WriteLine("     | @ TopoPoint3| @ pre-change X" + "\n");
					point.X = value;
					Debug.WriteLine("     | @ TopoPoint3| @ post-change X" + "\n");

					OnPropertyChange();
				}
			}
		}

		public double Y
		{
			get => point.Y;
			set
			{
				if (!value.Equals(point.Y))
				{
					point.Y = value;

					OnPropertyChange();
				}
			}
		}

		public double Z
		{
			get => point.Z;
			set
			{
				if (!value.Equals(point.Z))
				{
					point.Z = value;

					OnPropertyChange();
				}
			}
		}

		// these are management values
		// setting one of them just causes
		// a property change event
		public int Index
		{
			get => index;
			set
			{
				if (value == index) return;
				index = value;
				OnPropertyChange();
			}
		}

		public bool IsBeingEdited
		{
			get => isBeingEdited;
			set
			{
				if (value == isBeingEdited) return;
				isBeingEdited = value;
				OnPropertyChange();
			}
		}

		public bool ControlPoint
		{
			get => controlPoint;
			private set { controlPoint = value; }
		}

		// values below are calculated and 
		// cannot be set from outside
		// event only insures UI gets the
		// updated value
		public double XΔ
		{
			get => _xΔ;
			set
			{
				if (value.Equals(_xΔ)) return;
				_xΔ = value;
				OnPropertyChange();
			}
		}

		public double YΔ
		{
			get => _yΔ;
			set
			{
				if (value.Equals(_yΔ)) return;
				_yΔ = value;
				OnPropertyChange();
			}
		}

		public double ZΔ
		{
			get => _zΔ;
			set
			{
				if (value.Equals(_zΔ)) return;
				_zΔ = value;
				OnPropertyChange();
			}
		}

		public double XYΔ
		{
			get => _xyΔ;
			set
			{
				if (value.Equals(_xyΔ)) return;
				_xyΔ = value;
				OnPropertyChange();
			}
		}

		public double XYZΔ
		{
			get => _xyzΔ;
			set
			{
				if (value.Equals(_xyzΔ)) return;
				_xyzΔ = value;
				OnPropertyChange();
			}
		}

		public double Slope
		{
			get => _slope;
			set
			{
				if (value.Equals(_slope)) return;
				_slope = value;
				OnPropertyChange();
			}
		}

		// indexer using a string to select
		public double this[string which]
		{
			get
			{
				switch (which)
				{
				case TopoPtsConsts.XaxisName:
					{
						return X;
					}
				case TopoPtsConsts.YaxisName:
					{
						return Y;
					}
				case TopoPtsConsts.ZaxisName:
					{
						return Z;
					}
				}

				return double.NaN;
			}

			set
			{
				switch (which)
				{
				case TopoPtsConsts.XaxisName:
					{
						X = value;
						break;
					}
				case TopoPtsConsts.YaxisName:
					{
						Y = value;
						break;
					}
				case TopoPtsConsts.ZaxisName:
					{
						Z = value;
						break;
					}
				}
			}
		}

	#endregion

	#region > public methods

		public void Undo(TopoPtsData3.Support.TopoPtsTags which)
		{
			point.Undo(which);

			OnPropertyChange(which.AxisName);
		}
		
		public void Redo(TopoPtsData3.Support.TopoPtsTags which)
		{
			point.Redo(which);

			OnPropertyChange(which.AxisName);
		}

	#endregion

	#region > private methods

		public void Clear()
		{
			Index = -1;
			point = new XYZ3();
			XΔ = Double.NaN;
			YΔ = Double.NaN;
			ZΔ = Double.NaN;
			XYΔ = Double.NaN;
			XYZΔ = Double.NaN;
			Slope = Double.NaN;

			isBeingEdited = false;
			controlPoint = false;
		}

	#endregion

	#region > utility methods

	#endregion

	#region > overriding members

		public override string ToString()
		{
			return (
				X.ToString("F4") + "," +
				Y.ToString("F4") + "," +
				Z.ToString("F4"));
		}

		public object Clone()
		{
			return new TopoPoint3
			{
				X = point.X,
				Y = point.Y,
				Z = point.Z,
				Index = index,
				ControlPoint = controlPoint,
				XΔ = _xΔ,
				YΔ = _yΔ,
				ZΔ = _zΔ,
				XYΔ = _xyΔ,
				XYZΔ = _xyzΔ,
				Slope = _slope
			};
		}

		public bool Equals(TopoPoint3 other)
		{
			return
				point.X.Equals(other.point.X) &&
				point.Y.Equals(other.point.Y) &&
				point.Z.Equals(other.point.Z);
		}

		public bool FullyEquals(TopoPoint3 other)
		{
			return
				point.X.Equals(other.point.X) &&
				point.Y.Equals(other.point.Y) &&
				point.Z.Equals(other.point.Z) &&
				XΔ.Equals(other.XΔ) &&
				YΔ.Equals(other.YΔ) &&
				ZΔ.Equals(other.ZΔ) &&
				XYΔ.Equals(other.XYΔ) &&
				Slope.Equals(other.Slope);
		}

	#endregion

	#region > event handler

	#if DEBUG
		private void PropChangedReceivedMessage(string who, string propname)
		{
			Debug.WriteLine("(get ) @ TopoPoint3.PropChanged| (" + who + " [" + propname + "] )");
		}
	#endif

		private void PointPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
	#if DEBUG
			PropChangedReceivedMessage("general", e.PropertyName);
	#endif

			OnPropertyChange(e.PropertyName);
		}

	#endregion

	#region > event providers

	#if DEBUG
		private void PropChangedSendMessage(string who, string membername)
		{
			Debug.WriteLine("(send) @ TopoPoint3.OnPropertyChange| (" + who + " [" + membername + "] )");
		}
	#endif

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChange([CallerMemberName] string memberName = "")
		{
	#if DEBUG
			PropChangedSendMessage("general", memberName);
	#endif
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}

	#endregion
	}
}