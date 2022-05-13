using Controller;
using hospital.Controller;
using hospital.Model;
using Model;
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
    /// Interaction logic for PatientDoctorPoll.xaml
    /// </summary>
    public partial class PatientDoctorPoll : Page
    {
        private App app;
        private AppointmentController ac;
        private PollBlueprintController pbc;
        private UserController uc;
        public List<PollQuestion> Poll { get; set; }
        public PatientDoctorPoll()
        {
            InitializeComponent();
            app = Application.Current as App;
            pbc = app.pollBlueprintController;
            uc = app.userController;
            ac = app.appointmentController;
            cbAppointments.ItemsSource = ac.GetAppointmentByPatient(uc.CurentLoggedUser.Username);
            DataContext = this;
            Poll = pbc.GetDoctorPollQuestions();
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            app.PatientBackToMainMenu();
        }

        private void cbAppointments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cbAppointments.SelectedItem != null)
            {
                Appointment appointment = (Appointment)cbAppointments.SelectedItem;
                if (!pbc.AppointmentPollAlreadyFilled(appointment.Id))
                {
                    //lbPoll.Visibility = Visibility.Visible;
                }
            }
            
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (CheckIfFilled())
            {
                if (cbAppointments.SelectedItem != null)
                {
                    Appointment appointment = (Appointment)cbAppointments.SelectedItem;
                    if (!pbc.AppointmentPollAlreadyFilled(appointment.Id))
                    {
                        PollBlueprint poll = FillPoll();
                        poll.Username = uc.CurentLoggedUser.Username;
                        poll.AppointmentId = appointment.Id;
                        pbc.SavePoll(poll);
                        app.PatientBackToMainMenu();
                    }
                    else
                    {
                        MessageBox.Show("You have already answered the poll for this appointment!");
                    }
                }
                else
                {
                    MessageBox.Show("Please choose an appointment!");
                }
            }
            else
            {
                MessageBox.Show("Please answer all of the questions!");
            }
        }

        private PollBlueprint FillPoll()
        {
            PollBlueprint poll = pbc.GetDoctorPollBlueprint();
            foreach (var listIterator in lbPoll.Items)
            {
                PollQuestion question = (PollQuestion)listIterator;
                ListBoxItem item = (ListBoxItem)lbPoll.ItemContainerGenerator.ContainerFromItem(listIterator);
                ContentPresenter presenter = FindVisualChild<ContentPresenter>(item);
                DataTemplate dataTemplate = presenter.ContentTemplate;
                if (dataTemplate != null)
                {
                    int grade;
                    bool oneChecked = (bool)((RadioButton)dataTemplate.FindName("rbOne", presenter)).IsChecked;
                    bool twoChecked = (bool)((RadioButton)dataTemplate.FindName("rbTwo", presenter)).IsChecked;
                    bool threeChecked = (bool)((RadioButton)dataTemplate.FindName("rbThree", presenter)).IsChecked;
                    bool fourChecked = (bool)((RadioButton)dataTemplate.FindName("rbFour", presenter)).IsChecked;
                    bool fiveChecked = (bool)((RadioButton)dataTemplate.FindName("rbFive", presenter)).IsChecked;

                    if (oneChecked)
                        grade = 1;
                    else if (twoChecked)
                        grade = 2;
                    else if (threeChecked)
                        grade = 3;
                    else if (fourChecked)
                        grade = 4;
                    else
                        grade = 5;


                    foreach (PollCategory category in poll.Categories)
                    {
                        foreach (PollQuestion pollQuestion in category.PollQuestions)
                        {
                            if (pollQuestion.Id == question.Id)
                            {
                                pollQuestion.Grade = grade;
                            }
                        }
                    }
                }
            }
            return poll;
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
    }
}
