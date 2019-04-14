using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfExample.SfPoints
{
    /// <summary>
    /// Interaction logic for SurfacePoints.xaml
    /// </summary>
    public partial class SurfacePoints : Window
	{
		public string WinTitle { get; set; } = "Surface Points";

        public SurfacePoints()
        {
            InitializeComponent();
        }
    }
}
