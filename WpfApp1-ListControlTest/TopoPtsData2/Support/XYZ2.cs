// Solution:     WpfApp1-ListControlTest
// Project:       WpfApp1-ListControlTest
// File:             XYZ2.cs
// Created:      -- ()

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using WpfApp1_ListControlTest.TopoPts.Support;

namespace WpfApp1_ListControlTest.TopoPtsData2
{
	public class XYZ2 : IEquatable<XYZ2>, INotifyPropertyChanged, ICloneable
	{
		private Coordinate x = new Coordinate();
		private Coordinate y = new Coordinate();
		private Coordinate z = new Coordinate();

		private const string XYZChange = "xyz";
		private const string UndoChange = "undo";

		public XYZ2(double x = Double.NaN, double y = Double.NaN, double z = Double.NaN)
		{
			this.x.PropertyChanged += CoordinateChangedX;
			this.y.PropertyChanged += CoordinateChangedY;
			this.z.PropertyChanged += CoordinateChangedZ;


			this.x.Value = x;
			this.y.Value = y;
			this.z.Value = z;
		}

	#region > public properties

		public double X
		{
			get => x.Value;
			set
			{
				if (Double.IsNaN(value)
					|| value.Equals(x.Value)) return;

				x.Value = value;
			}
		}

		public double Y
		{
			get => y.Value;
			set
			{
				if (Double.IsNaN(value)
					|| value.Equals(y.Value)) return;

				y.Value = value;
			}
		}

		public double Z
		{
			get => z.Value;
			set
			{
				if (Double.IsNaN(value)
					|| value.Equals(z.Value)) return;

				z.Value = value;
			}
		}

		public double UndoValueX => x.UndoValue;
		public double UndoValueY => y.UndoValue;
		public double UndoValueZ => z.UndoValue;

		public bool IsRevisedX => x.IsRevised;
		public bool IsRevisedY => y.IsRevised;
		public bool IsRevisedZ => z.IsRevised;

		public bool IsValid => !Double.IsNaN(X) &&
			!Double.IsNaN(Y) && !Double.IsNaN(Z);

		public bool IsRevised => IsRevisedX || IsRevisedY || IsRevisedZ;

	#endregion

	#region > methods

		public void Undo(string which)
		{
			if (which.Equals(TopoPtsConsts.aTag))
			{
				x.Undo();
				y.Undo();
				z.Undo();
			}
			else
			{
//				Coordinate c = null;

				switch (which)
				{
				case TopoPtsConsts.xTag:
					{
						x.Undo();
						break;
					}
				case TopoPtsConsts.yTag:
					{
						y.Undo();
						break;
					}
				case TopoPtsConsts.zTag:
					{
						z.Undo();
						break;
					}
				}
			}
		}

	#endregion

	#region > utility

		public static XYZ2 Empty => new XYZ2();

		public bool Equals(XYZ2 other)
		{
			return
				X.Equals(other.X) &&
				Y.Equals(other.Y) &&
				Z.Equals(other.Z);
		}

		public override string ToString()
		{
			return (
				X.ToString("F4") + ", " +
				Y.ToString("F4") + ", " +
				Z.ToString("F4"));
		}

		public object Clone()
		{
			XYZ2 clone = new XYZ2();

			clone.x = x.Clone() as Coordinate;
			clone.y = y.Clone() as Coordinate;
			clone.z = z.Clone() as Coordinate;

			return clone;
		}

	#endregion

	#region > event handlers

	#if DEBUG
		private void CoordinateMessage(string who, string propname)
		{
			Debug.WriteLine("(get ) @ XYZ2.CoordinateChanged| (" + who + " [" + propname + "] )");
		}
	#endif

		private void CoordinateChangedX(object sender, PropertyChangedEventArgs e)
		{
		#if DEBUG
			CoordinateMessage("X", e.PropertyName);
		#endif

			if (e.PropertyName.Equals(XYZChange))
			{
				OnPropertyChange("IsRevisedX");
				OnPropertyChange("HasRevision");
			}
			else
			{
				OnPropertyChange("UndoValueX");
			}
		}

		private void CoordinateChangedY(object sender, PropertyChangedEventArgs e)
		{
		#if DEBUG
			CoordinateMessage("Y", e.PropertyName);
		#endif
			if (e.PropertyName.Equals(XYZChange))
			{
				OnPropertyChange("IsRevisedY");
				OnPropertyChange("HasRevision");
			}
			else
			{
				OnPropertyChange("UndoValueY");
			}
		}

		private void CoordinateChangedZ(object sender, PropertyChangedEventArgs e)
		{
		#if DEBUG
			CoordinateMessage("Z", e.PropertyName);
		#endif
			if (e.PropertyName.Equals(XYZChange))
			{
				OnPropertyChange("IsRevisedZ");
				OnPropertyChange("HasRevision");
			}
			else
			{
				OnPropertyChange("UndoValueZ");
			}
		}

	#endregion

	#region > event providers

		private void PropChangedMessage(string who, string membername)
		{
			Debug.WriteLine("(send) @ XYZ2.PropChanged| (" + who + " [" + membername + "] )");
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChange([CallerMemberName] string memberName = "")
		{
			PropChangedMessage("general", memberName);

			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}

	#endregion

	#region > coordinate

		// an X or Y or Z value 
		// along with a backup value
		// to allow undo
		public class Coordinate : INotifyPropertyChanged, ICloneable
		{
			// the value of this number
			private double value;

			// the number used to show in the UI
			// this will typically match value
			// however, when being edited, this
			// holds the proposed number waiting
			// to be applied
			private double displayValue;

			// this is the original number saved
			// so that we can preform an undo
			private double undoValue;

			// indicates that this Coordinate has 
			// an undo value
			private bool isRevised;

			public Coordinate()
			{
				value = Double.NaN;
				UndoValue = Double.NaN;
				isRevised = false;
			}

			public double UndoValue
			{
				get => undoValue;
				private set
				{
					if (value.Equals(undoValue)) return;

					undoValue = value;

					OnPropertyChange(UndoChange);

					IsRevised = true;


				}
			}

			// update the value of this coordinate
			// note that this only has (1) undo level
			// a subsequent change over writes the 
			// prior undo value
			public double Value
			{
				get => value;
				set
				{
					// do nothing if no change
					if (this.value.Equals(value)) return;

					// do we currently have a value
					// if true, save current value as
					// the undo value and set
					// both flags true
					if (!double.IsNaN(value))
					{
						UndoValue = this.value;

						this.value = value;

					}
					// no value previously set (first time set)
					else
					{
						this.value = value;

						// use internal value to prevent 
						// an extra event
						isRevised = false;
					}

					OnPropertyChange(XYZChange);
				}
			}

			public void Undo()
			{
				if (IsRevised)
				{
					Value = UndoValue;
					IsRevised = false;
				}
			}

			public bool IsRevised
			{
				get => isRevised;
				private set
				{
					// don't flag no change events
					if (isRevised == value) return;

					isRevised = value;

					OnPropertyChange();
				}
			}

			public override string ToString()
			{
				return value.ToString("F4");
			}

			public object Clone()
			{
				Coordinate clone = new Coordinate();

				clone.value = value;
				clone.displayValue = displayValue;
				clone.undoValue = undoValue;
				clone.isRevised = isRevised;
				clone.value = value;

				return clone;
			}

			public event PropertyChangedEventHandler PropertyChanged;

			private void OnPropertyChange(string memberName = "")
			{
				Debug.WriteLine("(send) @ Coordinate.OnPropertyChange| (" + memberName + ")\n");

				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
			}

		}

	#endregion
	}
}