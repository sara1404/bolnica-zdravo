using hospital.Controller;
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

namespace hospital.View.PatientView
{
    /// <summary>
    /// Interaction logic for PatientHospitalPoll.xaml
    /// </summary>
    public partial class PatientHospitalPoll : Page
    {
        private App app;
        public PatientHospitalPoll()
        {
            InitializeComponent();
            app = Application.Current as App;
            PollBlueprintController pbc = app.pollBlueprintController;
            pollTable.ItemsSource = pbc.GetHospitalPoll().PollQuestions;
            AddComboboxColumn();
        }
        private void AddComboboxColumn()
        {
            DataGridComboBoxColumn dgcc = new DataGridComboBoxColumn();
            IEnumerable<int> grades = new List<int>() { 1, 2, 3, 4, 5 };
            dgcc.ItemsSource = grades;
            dgcc.Width = 50;
            dgcc.DisplayIndex = 3;
            pollTable.Columns.Add(dgcc);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            app.PatientBackToMainMenu();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            //foreach()
            Console.WriteLine();
        }
    }
}
