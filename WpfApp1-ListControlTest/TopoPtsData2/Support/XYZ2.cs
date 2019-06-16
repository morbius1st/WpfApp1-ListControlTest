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
	public class XYZ2 : IEquatable<XYZ2>, INotifyPropertyChanged
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

		public bool HasUndoX => x.HasUndo;
		public bool HasUndoY => y.HasUndo;
		public bool HasUndoZ => z.HasUndo;
		
		public bool HasValueX => x.HasValue;
		public bool HasValueY => y.HasValue;
		public bool HasValueZ => z.HasValue;

		public bool IsValid => !Double.IsNaN(X) &&
			!Double.IsNaN(Y) && !Double.IsNaN(Z);

		public bool IsRevised => HasUndoX || HasUndoY || HasUndoZ;

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
				OnPropertyChange("HasUndoX");
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
				OnPropertyChange("HasUndoY");
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
				OnPropertyChange("HasUndoZ");
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


		public event PropertyChangedEventHandler PropertyChangedX;

		private void OnPropertyChangeX([CallerMemberName] string memberName = "")
		{
			PropChangedMessage("X", memberName);

			PropertyChangedX?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}

		public event PropertyChangedEventHandler PropertyChangedY;

		private void OnPropertyChangeY([CallerMemberName] string memberName = "")
		{
			PropChangedMessage("Y", memberName);

			PropertyChangedY?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}

		public event PropertyChangedEventHandler PropertyChangedZ;

		private void OnPropertyChangeZ([CallerMemberName] string memberName = "")
		{
			PropChangedMessage("Z", memberName);

			PropertyChangedZ?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}

	#endregion

	#region > coordinate

		// an X or Y or Z value 
		// along with a backup value
		// to allow undo
		public class Coordinate : INotifyPropertyChanged
		{
			private double value;
			private double undoValue;
			private bool hasValue;
			private bool hasUndo;

			public Coordinate()
			{
				value = Double.NaN;
				UndoValue = Double.NaN;
				hasUndo = false;
				hasValue = false;
			}

			public double UndoValue
			{
				get => undoValue;
				set
				{
					if (value.Equals(undoValue)) return;
					undoValue = value;

					OnPropertyChange(UndoChange);
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
					if (HasValue)
					{
						UndoValue = this.value;
						this.value = value;
						HasValue = true;
						HasUndo = true;
					}
					// no value previously set (first time set)
					// copy values, setflags
					else
					{
						this.value = value;
						HasValue = true;
						HasUndo = false;
					}

					OnPropertyChange(XYZChange);
				}
			}

			public void Undo()
			{
				if (HasUndo)
				{
					Value = UndoValue;
					HasValue = true;
					HasUndo = false;
				}
			}

			public bool HasValue
			{
				get => hasValue;
				private set
				{
					// don't flag no status change events
					if (value == hasValue) return;

					hasValue = value;
				}
			}

			public bool HasUndo
			{
				get => hasUndo;
				private set
				{
					// don't flag no change events
//					if (hasUndo == value) return;

					hasUndo = value;

//					OnPropertyChange();
				}
			}

			public override string ToString()
			{
				return value.ToString("F4");
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