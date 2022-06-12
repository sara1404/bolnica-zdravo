using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Drawing;
using Syncfusion.Pdf.Grid;
using System.Data;
using Controller;
using Model;
using ViewModel;

namespace hospital.View.UserControls
{
    public partial class GenerateReportUserControl : UserControl
    {
        public GenerateReportUserControl()
        {
            this.DataContext = new GenerateReportViewModel();
            InitializeComponent();
        }
    }
}
