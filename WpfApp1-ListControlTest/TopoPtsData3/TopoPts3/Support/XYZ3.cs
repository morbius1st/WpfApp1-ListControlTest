// Solution:     WpfApp1-ListControlTest
// Project:       WpfApp1-ListControlTest
// File:             XYZ3.cs
// Created:      -- ()

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using static WpfApp1_ListControlTest.TopoPtsData3.Support.TopoPtsConsts;

namespace WpfApp1_ListControlTest.TopoPtsData3.TopoPts3.Support
{
	public class XYZ3 : IEquatable<XYZ3>, INotifyPropertyChanged, ICloneable
	{
		private Coordinate x = new Coordinate();
		private Coordinate y = new Coordinate();
		private Coordinate z = new Coordinate();

		private const string IsRevisedChange = "IsRevised";
		private const string UndoValueChange = "UndoValue";
		private const string RedoValueChange = "RedoValue";
		private const string HasRedoValueChange = "HasRedo";

		public XYZ3(double x = Double.NaN, double y = Double.NaN, double z = Double.NaN)
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

		public double RedoValueX => x.RedoValue;
		public double RedoValueY => y.RedoValue;
		public double RedoValueZ => z.RedoValue;

		// is revised means the same as hasUndo
		public bool IsRevisedX => x.IsRevised;
		public bool IsRevisedY => y.IsRevised;
		public bool IsRevisedZ => z.IsRevised;

		public bool NeedsUpdatingX
		{
			get => x.NeedsUpdating;
			set => x.NeedsUpdating = value;
		}

		public bool NeedsUpdatingY
		{
			get => y.NeedsUpdating;
			set => y.NeedsUpdating = value;
		}

		public bool NeedsUpdatingZ
		{
			get => z.NeedsUpdating;
			set => z.NeedsUpdating = value;
		}

		public bool HasRedoX => x.HasRedo;
		public bool HasRedoY => y.HasRedo;
		public bool HasRedoZ => z.HasRedo;

		public bool IsValid => !Double.IsNaN(X) &&
			!Double.IsNaN(Y) && !Double.IsNaN(Z);

	#endregion

	#region > methods

		public void Undo(TopoPtsData3.Support.TopoPtsTags tag)
		{
			switch (tag.Value)
			{
			case XaxisIndex:
				{
					x.Undo();
					break;
				}
			case YaxisIndex:
				{
					y.Undo();
					break;
				}
			case ZaxisIndex:
				{
					z.Undo();
					break;
				}
			}
		}

		public void Redo(TopoPtsData3.Support.TopoPtsTags tag)
		{
			switch (tag.Value)
			{
			case XaxisIndex:
				{
					x.Redo();
					break;
				}
			case YaxisIndex:
				{
					y.Redo();
					break;
				}
			case ZaxisIndex:
				{
					z.Redo();
					break;
				}
			}
		}

	#endregion

	#region > utility

		public static XYZ3 Empty => new XYZ3();

		public bool Equals(XYZ3 other)
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
			XYZ3 clone = new XYZ3();

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
			Debug.WriteLine("(get ) @ XYZ3.CoordinateChanged| (" + who + " [" + propname + "] )");
		}
	#endif

		private void CoordinateChangedX(object sender, PropertyChangedEventArgs e)
		{
		#if DEBUG
			CoordinateMessage("X", e.PropertyName);
		#endif

			OnPropertyChange(e.PropertyName + "X");
		}

		private void CoordinateChangedY(object sender, PropertyChangedEventArgs e)
		{
		#if DEBUG
			CoordinateMessage("Y", e.PropertyName);
		#endif

			OnPropertyChange(e.PropertyName + "Y");
		}

		private void CoordinateChangedZ(object sender, PropertyChangedEventArgs e)
		{
		#if DEBUG
			CoordinateMessage("Z", e.PropertyName);
		#endif

			OnPropertyChange(e.PropertyName + "Z");
		}

	#endregion

	#region > event providers

	#if DEBUG
		private void PropChangedMessage(string who, string membername)
		{
			Debug.WriteLine("(send) @ XYZ3.PropChanged| (" + who + " [" + membername + "] )");
		}
	#endif

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChange([CallerMemberName] string memberName = "")
		{
		#if DEBUG
			PropChangedMessage("general", memberName);
		#endif

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

			// this is the original number saved
			// so that we can preform an undo
			private double undoValue;

			// this is the value after an
			// undo operation and allows
			// for a redo operation
			private double redoValue;

			// indicates that this Coordinate has 
			// an undo value
			private bool isRevised;


			// indicates that this Coordinate 
			// has a redo value
			private bool hasRedo;

			public Coordinate()
			{
				value = Double.NaN;
				undoValue = Double.NaN;
				redoValue = Double.NaN;
				isRevised = false;
				hasRedo = false;
				NeedsUpdating = false;
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

					// do we currently have a value?

					// true if current is NaN.
					// no value previously set (first time set)
					if (double.IsNaN(this.value))
					{
						this.value = value;
						NeedsUpdating = true;
					}

					// has a prior value
					// current is not NaN.
					// save current value as
					// the undo value and set
					// flag to true
					else
					{
						UndoValue = this.value;

						RedoValue = double.NaN;

						this.value = value;

						IsRevised = true;

						NeedsUpdating = true;
					}
				}
			}

			public double UndoValue
			{
				get => undoValue;
				private set
				{
					if (value.Equals(undoValue)) return;

					undoValue = value;

					OnPropertyChange(UndoValueChange);
				}
			}

			public double RedoValue
			{
				get => redoValue;
				private set
				{
					if (value.Equals(redoValue)) return;

					redoValue = value;

					OnPropertyChange(RedoValueChange);

					HasRedo = !double.IsNaN(redoValue);
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

					OnPropertyChange(IsRevisedChange);
				}
			}

			public bool HasRedo
			{
				get => hasRedo;
				private set
				{
					// don't flag no change events
					if (value == hasRedo) return;

					hasRedo = value;

					OnPropertyChange(HasRedoValueChange);
				}
			}

			// indicates that the additional values
			// associated with this coordinate
			// needs to be updated

			// no pre-check on setter for duplicate value
			// because there is no event 
			// associated with this property
			public bool NeedsUpdating { get; set; }

			public void Undo()
			{
				if (IsRevised)
				{
					RedoValue = value;

					value = undoValue;

					UndoValue = Double.NaN;

					IsRevised = false;

					NeedsUpdating = true;
				}
			}

			public void Redo()
			{
				if (HasRedo)
				{
					Value = redoValue;
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