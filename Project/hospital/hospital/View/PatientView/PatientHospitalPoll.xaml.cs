using hospital.Controller;
using hospital.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public List<PollQuestion> Poll { get; set; }
        public PatientHospitalPoll()
        {
            InitializeComponent();
            app = Application.Current as App;
            PollBlueprintController pbc = app.pollBlueprintController;
            //lbPoll.ItemsSource = pbc.GetHospitalPoll().PollQuestions;
            DataContext = this;
            Poll = pbc.GetHospitalPoll().PollQuestions;
            //AddComboboxColumn();
            //this.Resources.Add("poll", pbc.GetHospitalPoll().PollQuestions);
        }
        private void AddComboboxColumn()
        {
            DataGridComboBoxColumn dgcc = new DataGridComboBoxColumn();
            IEnumerable<int> grades = new List<int>() { 1, 2, 3, 4, 5 };
            dgcc.ItemsSource = grades;
            dgcc.Width = 50;
            dgcc.DisplayIndex = 3;
            //pollTable.Columns.Add(dgcc);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            app.PatientBackToMainMenu();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckIfFilled())
            {
                MessageBox.Show("Please answer all of the questions!");
            }
            else
            {

            }
            

        }

        private bool CheckIfFilled()
        {
            foreach (var listIterator in lbPoll.Items)
            {
                ListBoxItem item = (ListBoxItem)lbPoll.ItemContainerGenerator.ContainerFromItem(listIterator);
                ContentPresenter presenter = FindVisualChild<ContentPresenter>(item);
                DataTemplate dataTemplate = presenter.ContentTemplate;
                if (dataTemplate != null)
                {
                    bool oneChecked = (bool)((RadioButton)dataTemplate.FindName("rbOne", presenter)).IsChecked;
                    bool twoChecked = (bool)((RadioButton)dataTemplate.FindName("rbTwo", presenter)).IsChecked;
                    bool threeChecked = (bool)((RadioButton)dataTemplate.FindName("rbThree", presenter)).IsChecked;
                    bool fourChecked = (bool)((RadioButton)dataTemplate.FindName("rbFour", presenter)).IsChecked;
                    bool fiveChecked = (bool)((RadioButton)dataTemplate.FindName("rbFive", presenter)).IsChecked;
                    if (!oneChecked && !twoChecked && !threeChecked && !fourChecked && !fiveChecked)
                    {
                        return false;
                    }
                    
                }
            }
            return true;
        }
        // this function has been found online so DON'T F**ING TOUCH IT 
        private childItem FindVisualChild<childItem>(DependencyObject obj)
        where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                {
                    return (childItem)child;
                }
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
    }
}
