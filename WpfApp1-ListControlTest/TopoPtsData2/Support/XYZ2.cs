// Solution:     WpfApp1-ListControlTest
// Project:       WpfApp1-ListControlTest
// File:             XYZ2.cs
// Created:      -- ()

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using WpfApp1_ListControlTest.TopoPts.Support;

namespace WpfApp1_ListControlTest.TopoPtsData2.Support
{
	public class XYZ2 : IEquatable<XYZ2>, INotifyPropertyChanged, ICloneable
	{
		private Coordinate x = new Coordinate();
		private Coordinate y = new Coordinate();
		private Coordinate z = new Coordinate();

		private const string ValueChange = "value";
		private const string UndoValueChange = "undo";
		private const string RedoValueChange = "redo";


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

		public double RedoValueX => x.RedoValue;
		public double RedoValueY => y.RedoValue;
		public double RedoValueZ => z.RedoValue;

		public bool IsRevisedX => x.IsRevised;
		public bool IsRevisedY => y.IsRevised;
		public bool IsRevisedZ => z.IsRevised;
		
		public bool HasRedoX => x.HasRedo;
		public bool HasRedoY => y.HasRedo;
		public bool HasRedoZ => z.HasRedo;

		public bool IsValid => !Double.IsNaN(X) &&
			!Double.IsNaN(Y) && !Double.IsNaN(Z);

		public bool IsRevised => IsRevisedX || IsRevisedY || IsRevisedZ;
		public bool HasRedo => HasRedoX || HasRedoY || HasRedoZ;

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

		public void Redo(string which)
		{
			switch (which)
			{
			case TopoPtsConsts.xTag:
				{
					x.Redo();
					break;
				}
			case TopoPtsConsts.yTag:
				{
					y.Redo();
					break;
				}
			case TopoPtsConsts.zTag:
				{
					z.Redo();
					break;
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

			if (e.PropertyName.Equals(ValueChange))
			{
				OnPropertyChange("IsRevisedX");
			}
			else if (e.PropertyName.Equals(UndoValueChange))
			{
				OnPropertyChange("UndoValueX");
			}
			else
			{
				OnPropertyChange("RedoValueX");
			}
		}

		private void CoordinateChangedY(object sender, PropertyChangedEventArgs e)
		{
		#if DEBUG
			CoordinateMessage("Y", e.PropertyName);
		#endif
			if (e.PropertyName.Equals(ValueChange))
			{
				OnPropertyChange("IsRevisedY");
			}
			else if (e.PropertyName.Equals(UndoValueChange))
			{
				OnPropertyChange("UndoValueY");
			}
			else
			{
				OnPropertyChange("RedoValueY");
			}
		}

		private void CoordinateChangedZ(object sender, PropertyChangedEventArgs e)
		{
		#if DEBUG
			CoordinateMessage("Z", e.PropertyName);
		#endif
			if (e.PropertyName.Equals(ValueChange))
			{
				OnPropertyChange("IsRevisedZ");
			}
			else if (e.PropertyName.Equals(UndoValueChange))
			{
				OnPropertyChange("UndoValueZ");
			}
			else
			{
				OnPropertyChange("RedoValueZ");
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
					if (!double.IsNaN(this.value))
					{
						UndoValue = this.value;

						RedoValue = double.NaN;

						this.value = value;

						IsRevised = true;

					}
					// clear / reset the current value
					else if (double.IsInfinity(value))
					{
						this.value = Double.NaN;

						undoValue = Double.NaN;
						redoValue = Double.NaN;

						OnPropertyChange(ValueChange);
						OnPropertyChange(UndoValueChange);
						OnPropertyChange(RedoValueChange);

						IsRevised = false;
						HasRedo = false;

					}
					// no value previously set (first time set)
					else
					{
						this.value = value;

//						undoValue = Double.NaN;
//						redoValue = Double.NaN;
//
//						// use internal value to prevent 
//						// an extra event
//						isRevised = false;
//						hasRedo = false;
					}

					OnPropertyChange(ValueChange);
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

					OnPropertyChange();
				}
			}



			public void Undo()
			{
				if (IsRevised)
				{
					RedoValue =  value;

					value = undoValue;

					IsRevised = false;

					OnPropertyChange(UndoValueChange);

					OnPropertyChange(ValueChange);
				}
			}

			public void Redo()
			{
				if (HasRedo)
				{

					Value = redoValue;

//					undoValue = value;
//
//					value = redoValue;
//
//					RedoValue = double.NaN;
//
//					isRevised = true;
//
//					OnPropertyChange(UndoValueChange);
//
//					OnPropertyChange(ValueChange);
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