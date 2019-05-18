using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Microsoft.Expression.Interactivity.Core;
using WpfApp1_ListControlTest.ControlPtsData;
using static WpfApp1_ListControlTest.MainWindow;

namespace WpfApp1_ListControlTest.ControlPtsWin

{
	/// <summary>
	/// Interaction logic for ControlPoints.xaml
	/// </summary>
	public partial class ControlPoints : Window, INotifyPropertyChanged
	{
		public string WinTitle { get; set; } = "Control Points";

		public string MessageText { get; set; } = "Message Window\n";

		private int itemIndex = -1;
		private string _errorMsg = "error message 1";
		private string _endSlope = "10%";
		private string _endDeltaZ = "330'-0\"";
		private string _endDeltaXYZ = "320'-0\"";
		private string _endDeltaXY = "310'-0\"";
		private string _endZ = "230'-0\"";
		private string _endY = "220'-0\"";
		private string _endX = "210'-0\"";
		private string _startZ = "130'-0\"";
		private string _startY = "120'-0\"";
		private string _startX = "110'-0000\"";

		public ControlPointsConsts cpConsts { get; private set; } 
			= new ControlPointsConsts();

		public ObservableCollection<ControlPts> Sx { get; set; } 
			= new ObservableCollection<ControlPts>();

		public ControlPointsResources cpr { get; set; }

		private bool _canExecute;

		public ControlPoints()
		{
			InitializeComponent();

			_canExecute = true;

			MainWindow.Cps = this;

			if (MainWindow.Cpr != null)
			{
				cpr = MainWindow.Cpr;

				cpr.BtnRestoreXClick += CommandClickHandler2;
				cpr.XYZ_OnError += ValidationOnErrorHandler2;

			}

			this.AddHandler(Button.ClickEvent, new RoutedEventHandler(CommandClickHandler1));

			System.Windows.Controls.Validation.AddErrorHandler(this, new EventHandler<ValidationErrorEventArgs>(ValidationOnErrorHandler1));
#if DEBUG
			LoadData();
#endif
		}

		private ICommand _clickCommand;

		public ICommand ClickCommand => _clickCommand ?? (_clickCommand = new CommandHandler(MyAction, _canExecute));

		public void MyAction()
		{
			MessageBox.Show("my action");
		}

		public class CommandHandler : ICommand
		{
			private Action _action;
			private bool _canExecute;
			public CommandHandler(Action action, bool canExecute)
			{
				_action = action;
				_canExecute = canExecute;
			}

			public bool CanExecute(object parameter)
			{
				return _canExecute;
			}

			public event EventHandler CanExecuteChanged;

			public void Execute(object parameter)
			{
				_action();
			}
		}


		private void ValidationOnErrorHandler1(object sender, ValidationErrorEventArgs e)
		{
			MessageBox.Show("bubble validation message");
		}
		
		private void ValidationOnErrorHandler2(object sender, ValidationErrorEventArgs e)
		{
			MessageBox.Show("indirect validation message");
		}

		private void CommandClickHandler1(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("bubble button click event message");
		}

		private void CommandClickHandler2(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("indirect button click event message");
		}


		private void BtnDone_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void BtnTest_Click(object sender, RoutedEventArgs e)
		{
			LoadData();
		}

		private void BtnDebugUndo_Click(object sender, RoutedEventArgs e)
		{
			ControlPts cps = Sx[itemIndex];
			cps.Undo(((Button)sender).Tag as string);

			AppendMessage("@ BtnDebugUndo_Click| "
				+ " :: revised?| " + cps.IsRevised
				);
			list(itemIndex);
		}

		private void BtnDebugSl_Click(object sender, RoutedEventArgs e)
		{
			//			Sx[itemIndex]._Scp.IsRevisedValid = true;
			Sx[itemIndex].Slope = -10000.0;
			AppendMessage("@ BtnDebugSl_Click");
			list(itemIndex);
		}

		private void BtnDebugX_Click(object sender, RoutedEventArgs e)
		{
			Sx[itemIndex].X = -20000.0;
			AppendMessage("@ BtnDebugX_Click");
			list(itemIndex);
		}

		//		private void BtnDebugReset_Click(object sender, RoutedEventArgs e)
		//		{
		//			Sx[1] = new ControlPts()  { XOrig = " 1081.0", YOrig = " 2081.0", ZOrig = " 3081.0", SlopeOrig = " 1.0", XyValue = " 4081.0", XyzValue = " 5081.0", ZDelta = " 6081.0"};
		//			AppendMessage("@ BtnDebugReset_Click");
		//			list(1);
		//		}

		private void BtnDebug_Click(object sender, RoutedEventArgs e)
		{
			Sx[itemIndex].X = -20000.0;

			StartX = "asdfZ";
			EndX = "asdf";


			AppendMessage("BtnDebug_Click");
			list(itemIndex);

			//			(listBox2.Items[1] as ControlPts).Slope = "asdf";
			//			Sx[1]._Scp.IsRevisedValid = true;
			//			Sx[1].Slope = "asdf";

		}

		private void list(int idx)
		{
			string a;
			a = "x| " + Sx[idx].X + "  ";
			a += "y| " + Sx[idx].Y + "  ";
			a += "z| " + Sx[idx].Z + "  ";
			a += "s| " + Sx[idx].Slope;

			AppendMessage(a);
		}

		private void LoadData()
		{
			Sx.Insert(0, new ControlPts() { XOrig = " 1011.0", YOrig = " 2011.0", ZOrig = " 3011.0", SlopeOrig = " 1.0", XyOrig = " 4011.0", XyzOrig = " 5011.0", ZDeltaOrig = " 6011.0" });
			Sx.Insert(0, new ControlPts() { XOrig = " 1021.0", YOrig = " 2021.0", ZOrig = " 3021.0", SlopeOrig = " 1.0", XyOrig = " 4021.0", XyzOrig = " 5021.0", ZDeltaOrig = " 6021.0" });
			Sx.Insert(0, new ControlPts() { XOrig = " 1031.0", YOrig = " 2031.0", ZOrig = " 3031.0", SlopeOrig = " 1.0", XyOrig = " 4031.0", XyzOrig = " 5031.0", ZDeltaOrig = " 6031.0" });
			Sx.Insert(0, new ControlPts() { XOrig = " 1041.0", YOrig = " 2041.0", ZOrig = " 3041.0", SlopeOrig = " 1.0", XyOrig = " 4041.0", XyzOrig = " 5041.0", ZDeltaOrig = " 6041.0" });
//			Sx.Insert(0, new ControlPts() { XOrig = " 1051.0", YOrig = " 2051.0", ZOrig = " 3051.0", SlopeOrig = " 1.0", XyOrig = " 4051.0", XyzOrig = " 5051.0", ZDeltaOrig = " 6051.0" });
//			Sx.Insert(0, new ControlPts() { XOrig = " 1061.0", YOrig = " 2061.0", ZOrig = " 3061.0", SlopeOrig = " 1.0", XyOrig = " 4061.0", XyzOrig = " 5061.0", ZDeltaOrig = " 6061.0" });
//			Sx.Insert(0, new ControlPts() { XOrig = " 1071.0", YOrig = " 2071.0", ZOrig = " 3071.0", SlopeOrig = " 1.0", XyOrig = " 4071.0", XyzOrig = " 5071.0", ZDeltaOrig = " 6071.0" });
//			Sx.Insert(0, new ControlPts() { XOrig = " 1081.0", YOrig = " 2081.0", ZOrig = " 3081.0", SlopeOrig = " 1.0", XyOrig = " 4081.0", XyzOrig = " 5081.0", ZDeltaOrig = " 6081.0" });
//			Sx.Insert(0, new ControlPts() { XOrig = " 1091.0", YOrig = " 2091.0", ZOrig = " 3091.0", SlopeOrig = " 1.0", XyOrig = " 4091.0", XyzOrig = " 5091.0", ZDeltaOrig = " 6091.0" });

		}

		public string StartX
		{
			get => _startX;
			private set
			{
				_startX = value;
				OnPropertyChange();
			}
		}
		public string StartY
		{
			get => _startY;
			private set
			{
				_startY = value;
				OnPropertyChange();
			}
		}
		public string StartZ
		{
			get => _startZ;
			private set
			{
				_startZ = value;
				OnPropertyChange();
			}
		}
		public string EndX
		{
			get => _endX;
			private set
			{
				_endX = value;
				OnPropertyChange();
			}
		}
		public string EndY
		{
			get => _endY;
			private set
			{
				_endY = value;
				OnPropertyChange();
			}
		}
		public string EndZ
		{
			get => _endZ;
			private set
			{
				_endZ = value;
				OnPropertyChange();
			}
		}
		public string EndDeltaXY
		{
			get => _endDeltaXY;
			private set
			{
				_endDeltaXY = value;
				OnPropertyChange();
			}
		}
		public string EndDeltaXYZ
		{
			get => _endDeltaXYZ;
			private set
			{
				_endDeltaXYZ = value;
				OnPropertyChange();
			}
		}
		public string EndDeltaZ
		{
			get => _endDeltaZ;
			private set
			{
				_endDeltaZ = value;
				OnPropertyChange();
			}
		}
		public string EndSlope
		{
			get => _endSlope;
			private set
			{
				_endSlope = value;
				OnPropertyChange();
			}
		}
		public string ErrorMsg
		{
			get => _errorMsg;

			set
			{
				_errorMsg = value;
				OnPropertyChange();
			}
		}

		internal void AppendMessage(string message)
		{
			MessageText += message + nl;

			OnPropertyChange("MessageText");
		}

		public void TellMeTbx(string which, TextBox tbx = null)
		{
			if (tbx != null)
			{
				AppendMessage(nl + which + "| " + tbx.Name
					+ " :: text| (" + tbx.Text + ")"
					+ " :: tag| (" + (string)tbx.Tag + ")"
					);
			}
			else
			{
				AppendMessage(nl + which);
			}
		}

		private void TellMeLbx(ListBox lbx, string which)
		{
			AppendMessage(which + "| name| " + lbx.Name
				+ " :: idx (sender)| " + lbx.SelectedIndex
				+ " :: idx (listBox2)| " + listBox2.SelectedIndex
				);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChange([CallerMemberName] string memberName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(memberName));
		}

		public void TbxXYZ_OnError(object sender, ValidationErrorEventArgs e)
		{
			TextBox tbx = (TextBox)sender;

			TellMeTbx("at TbxXYZ_OnError| ", tbx);
			AppendMessage("*** on error action| "
				+ e.Action.ToString()
				);

			//			OnErrorEx();
		}

		public void TbxSlope_OnError(object sender, ValidationErrorEventArgs e)
		{
			TextBox tbx = (TextBox)sender;

			TellMeTbx("at TbxSlope_OnError| ", tbx);
			AppendMessage("*** on error action| "
				+ e.Action.ToString()
				);

			//			OnErrorEx();
		}

		private void ListBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
//			TellMeTbx("at ListBox2_SelectionChanged");

			//			// uses the selectedindex property to get the correct index
			//			TellMeLbx(sender as ListBox, "SelectionChanged");

			if (itemIndex < 0 || !Sx[itemIndex].IsRevised)
			{
				if (itemIndex >= 0)
				{
					((ControlPts) listBox2.Items[itemIndex]).IsBeingEdited = false;
				}

				// begin editing a new item
				// save the index of this item
				itemIndex = listBox2.SelectedIndex;

				((ControlPts)listBox2.SelectedItem).CurrentIndex = itemIndex;
				((ControlPts)listBox2.SelectedItem).IsBeingEdited = true;

				return;
			}

			// the current item is has changes - prevent them from switching to another item
			// use this method to prevent a selection change.
			listBox2.SelectedIndex = itemIndex;

			//SelectionChangedEx();
		}

		private bool clickTest = false;

		private void Testx_Click(object sender, RoutedEventArgs e)
		{
			clickTest = !clickTest;


			if (clickTest)
			{
				label.Content = "CLICKED";
			}
			else
			{
				label.Content = "UN-CLICKED";
			}
		}

#pragma warning disable CS0108 // 'ControlPoints.LostFocus(object, RoutedEventArgs)' hides inherited member 'UIElement.LostFocus'. Use the new keyword if hiding was intended.
		private void LostFocus(object sender, RoutedEventArgs e)
#pragma warning restore CS0108 // 'ControlPoints.LostFocus(object, RoutedEventArgs)' hides inherited member 'UIElement.LostFocus'. Use the new keyword if hiding was intended.
		{
			if (itemIndex >=0 && !Sx[itemIndex].IsRevised)
			{
				Sx[itemIndex].IsBeingEdited = false;

				listBox2.SelectedIndex = -1;
			}
		}
		//
		//		private void tbxXYZ_OnGotFocus(object sender, RoutedEventArgs e)
		//		{
		//			TextBox tbx = sender as TextBox;
		//
		//			AppendMessage("\n*** select " + tbx.Tag + " textbox ***");
		//
		//
		//			TellMeTbx( "at tbxXYZ_OnGotFocus| ", tbx);
		//
		////			FocusEx(TextBox tbx);
		//		}
		//		
		//		private void tbxXYZ_OnLostFocus(object sender, RoutedEventArgs e)
		//		{
		//			TextBox tbx = sender as TextBox;
		//
		//			AppendMessage("\n*** de-select " + tbx.Tag + " textbox ***");
		//
		//			TellMeTbx( "at tbxXYZ_OnLostFocus| ", tbx);
		//		}
		//
		//		private void TbxXYZ_OnSourceUpdated(object sender, DataTransferEventArgs e)
		//		{
		//			TextBox tbx = sender as TextBox;
		//
		//			TellMeTbx( "at TbxValue_OnSourceUpdated| ", tbx);
		//		}
		//
		//		private void TbxSlope_OnSourceUpdated(object sender, DataTransferEventArgs e)
		//		{
		//			TextBox tbx = sender as TextBox;
		//			
		//			TellMeTbx( "at TbxSlope_OnSourceUpdated| ", tbx);
		//
		////			Sx[itemIndex].SetRevisedValue(tbx.Tag as string, tbx.Text);
		//		}


		//		private void SelectionChangedEx()
		//		{
		//			// sender is listbox
		//			AppendMessage("SelectionChanged| type|" + sender.GetType());
		//			// orig source is listbox
		//			AppendMessage("SelectionChanged| orig source type| " + e.OriginalSource.GetType());
		//			// source type is listbox
		//			AppendMessage("SelectionChanged| source type| " +	e.Source.GetType());
		//			// gets the value of this items x property
		//			AppendMessage("SelectionChanged| ControlPts|"
		//				+ " XOrig| " + ((ControlPts) listBox2.SelectedItem).XOrig
		//				+ " X| " + ((ControlPts) listBox2.SelectedItem).X
		//				);
		//			// count is 1 (single selection mode set)
		//			AppendMessage("SelectionChanged| selected items count| " + e.AddedItems.Count);
		//			// type is the data class (controlpts)
		//			AppendMessage("SelectionChanged| selected items type| " + e.AddedItems[0].GetType()); // gets a controlpoints object
		//			// type is the data class (controlpts)
		//			AppendMessage("SelectionChanged| selected type| " + listBox2.SelectedItem.GetType()); // gets a controlpoints object
		//
		//			ItemContainerGenerator gen = listBox2.ItemContainerGenerator;
		//
		//			DependencyObject dObj = gen.ContainerFromItem(e.AddedItems[0]);
		//			ListBoxItem lbi = dObj as ListBoxItem;
		//			// container type is listboxitem
		//			AppendMessage("SelectionChanged| container type| " + dObj?.GetType() ?? "is null"); // listbox item
		//			// container type is listboxitem
		//			AppendMessage("SelectionChanged| container type| " + lbi?.GetType() ?? "is null"); // listbox item
		//			// gets the correct index
		//			AppendMessage("SelectionChanged| item index| " + gen.IndexFromContainer(dObj)); // gets the index


		//			if (savedInfo.Row >= 0 && savedInfo.IsRevised)
		//			{
		//				AppendMessage("SelectionChanged| don't change row| row| " 
		//					+ savedInfo.Row 
		//					+ " :: hasrevised| " + savedInfo.IsRevised.ToString()
		//					);
		//
		//				if (listBox2.SelectedIndex != savedInfo.Row)
		//				{
		//					listBox2.SelectedIndex = savedInfo.Row;
		//				}
		//
		//				return;
		//			}
		//
		//			savedInfo = new SavedInfo();
		//
		//			savedInfo.Row = listBox2.SelectedIndex;
		//
		//			AppendMessage("SelectionChanged| change row| row| "
		//				+ savedInfo.Row );
		//		}

		//		private void OnErrorEx()
		//		{
		//			if (e.Action != ValidationErrorEventAction.Added)
		//			{
		//				AppendMessage("TbxXvalue error| "
		//					+ tbx.Text + " :: tag| " + tbx.Tag.ToString() 
		//					+ " :: Removed");
		//				return;
		//			}
		//
		//			AppendMessage("TbxXvalue error| "
		//				+ tbx.Text + " :: tag| " + tbx.Tag.ToString()
		//				+ " :: Added :: " + e.Error.ErrorContent);

		//			SavedInfo.Saved saved = GetSaved(tbx);
		//
		//			saved.RevisedValue = tbx.Text;
		//			saved.IsRevisedValid = false;
		//		}

		//		private void FocusEx(TextBox tbx)
		//		{
		//			TellMeTbx(tbx, "got focus");
		//
		//			AppendMessage("got focus| tbx parent 1| " + tbx.TemplatedParent.GetType()); // content presenter
		//
		//			parent = ((FrameworkElement) parent).Parent;
		//			AppendMessage("got focus| tbx parent 2| " + parent.GetType());
		//
		//			parent = ((FrameworkElement) parent).Parent;
		//			AppendMessage("got focus| tbx parent 3| " + parent.GetType());
		//
		//			parent = ((FrameworkElement) parent).Parent;
		//			AppendMessage("got focus| tbx parent 4| " + parent.GetType());
		//
		//			parent = ((FrameworkElement) parent).Parent;
		//			AppendMessage("got focus| tbx parent 5| " + parent.GetType());

		//			SavedInfo.Saved saved = GetSaved(tbx);
		//
		//			if (saved.HasOriginal) return;
		//
		//			saved.OriginalValue = tbx.Text;
		//		}


	}

	public class PointTemplateSelector : DataTemplateSelector
	{
		public override DataTemplate SelectTemplate(object item, DependencyObject container)
		{
			FrameworkElement elemnt = container as FrameworkElement;
			ControlPts       pt     = item as ControlPts;

			if (pt.IsBeingEdited)
			{
				return elemnt.FindResource("pointDataTemplateEditing") as DataTemplate;
			}
			else
			{
				return elemnt.FindResource("pointDataTemplate") as DataTemplate;
			}
		}

	}

}


		// these also did not work - never fired
//		private void ListBox2_SourceUpdated(object sender, DataTransferEventArgs e)
//		{
//			AppendMessage("ListBox2 SourceUpdated");
//		}
//
//		private void ListBox2_TargetUpdated(object sender, DataTransferEventArgs e)
//		{
//			AppendMessage("ListBox2 TargetUpdated");
//		}

		// using got focus / lost focus methods does not work - this
		// does not invervene between the list box and the collection
//		private void TbxFirst_OnGotFocus(object sender, RoutedEventArgs e)
//		{
//			savedValue = ((TextBox) sender).Text;
//		}
//
//		private void TbxFirst_LostFocus(object sender, RoutedEventArgs e)
//		{
//			AppendMessage("tbxFirst changed| " + ((TextBox) sender).Text);
//
//			((TextBox) sender).Text = savedValue;
//
//			e.Handled = true;
//		}
//
//
//		private void TbxSecond_OnGotFocus(object sender, RoutedEventArgs e)
//		{
//			savedValue = ((TextBox) sender).Text;
//		}
//
//
//		private void TbxSecond_LostFocus(object sender, RoutedEventArgs e)
//		{
//			AppendMessage("tbxSecond changed| " + ((TextBox) sender).Text);
//
//			((TextBox) sender).Text = savedValue;
//
//			e.Handled = true;
//		}
//
//
//
//		private void TbxThird_OnGotFocus(object sender, RoutedEventArgs e)
//		{
//			savedValue = ((TextBox) sender).Text;
//		}
//
//		private void TbxThird_LostFocus(object sender, RoutedEventArgs e)
//		{
//			AppendMessage("tbxThird changed| " + ((TextBox) sender).Text);
//
//			((TextBox) sender).Text = savedValue;
//
//			e.Handled = true;
//		}

