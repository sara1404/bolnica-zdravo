using Controller;
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
        private PollController pbc;
        private UserController uc;
        public List<PollQuestion> Poll { get; set; }
        public PatientHospitalPoll()
        {
            InitializeComponent();
            app = Application.Current as App;
            pbc = app.pollBlueprintController;
            uc = app.userController;
            DataContext = this;
            Poll = pbc.GetHospitalPollQuestions();

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            app.PatientBackToMainMenu();
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckIfFilled())
            {
                lbWarning.Content = "Please answer all of the questions!";
            }
            else
            {
                if (!pbc.HospitalPollAlreadyFilled(uc.CurentLoggedUser.Username))
                {
                    pbc.SavePoll(FillPoll());
                    app.PatientBackToMainMenu();
                }
                else
                {
                    lbWarning.Content = "You have already filled this poll!";
                }
            }
        }

        private PollBlueprint FillPoll()
        {
            PollBlueprint poll = pbc.GetHospitalPollBlueprint();
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
                            if(pollQuestion.Id == question.Id)
                            {
                                pollQuestion.Grade = grade;
                            }
                        }
                    }
                }
            }
            poll.Username = uc.CurentLoggedUser.Username;
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
