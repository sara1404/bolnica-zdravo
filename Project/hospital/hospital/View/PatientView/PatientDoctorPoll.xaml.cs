﻿using Controller;
using hospital.Controller;
using hospital.Model;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace hospital.View.PatientView
{
    /// <summary>
    /// Interaction logic for PatientDoctorPoll.xaml
    /// </summary>
    public partial class PatientDoctorPoll : Page
    {
        private App app;
        private AppointmentManagementController ac;
        private PollController pbc;
        private UserController uc;
        public List<PollQuestion> Poll { get; set; }
        public ObservableCollection<Appointment> Appointments { get; set; }
        public PatientDoctorPoll()
        {
            InitializeComponent();
            app = Application.Current as App;
            pbc = app.pollBlueprintController;
            uc = app.userController;
            ac = app.appointmentController;
            List<Appointment> appointments = ac.GetPastAppointmentsByPatient(uc.CurentLoggedUser.Username).ToList();
            appointments.RemoveAll(x => pbc.AppointmentPollAlreadyFilled(x.Id));
            Appointments = new ObservableCollection<Appointment>(appointments);
            DataContext = this;
            Poll = pbc.GetDoctorPollQuestions();
        }

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

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidated())
            {
                pbc.SavePoll(FillPoll());
                foreach (Window window in Application.Current.Windows)
                {
                    if (window.GetType() == typeof(PatientHomeWindow))
                    {
                        (window as PatientHomeWindow).Main.Content = new PatientCalendar();
                    }
                }
                Dispatcher.Invoke(() =>
                {
                    notifier.ShowInformation("Poll answers successfully saved!");
                });
            }
        }

        private bool IsValidated()
        {
            if (CheckIfFilled())
            {
                if (cbAppointments.SelectedItem != null)
                {
                    Appointment appointment = (Appointment)cbAppointments.SelectedItem;
                    if (!pbc.AppointmentPollAlreadyFilled(appointment.Id))
                    {
                        return true;
                    }
                    else
                    {
                        lbWarning.Content = "You have already answered the poll for this appointment!";
                        return false;
                    }
                }
                else
                {
                    lbWarning.Content = "Please choose an appointment!";
                    return false;
                }
            }
            else
            {
                lbWarning.Content = "Please answer all of the questions!";
                return false;
            }
        }

        private PollBlueprint FillPoll()
        {
            PollBlueprint poll = new PollBlueprint();
            poll.Type = PollType.DOCTOR_POLL;
            poll.PollName = "Doctor poll";
            poll.Categories = new List<PollCategory>()
            {
                new PollCategory(0, "Doctor", new List<PollQuestion>()
                {
                    new PollQuestion(0, "How polite was your doctor?"),
                    new PollQuestion(1, "How kind was your doctor?"),
                    new PollQuestion(2, "How would you rate doctors expertise?"),
                }),
                new PollCategory(1, "Appointment", new List<PollQuestion>()
                {
                    new PollQuestion(3, "How appropriate were the medicines the doctor gave you?"),
                    new PollQuestion(4, "Did you have to wait for a long time?"),
                    new PollQuestion(5, "Did you spend a lot of time on appointment?"),
                }),
                new PollCategory(2, "Ordination", new List<PollQuestion>()
                {
                    new PollQuestion(6, "Rate the cleanliness of the ordination?"),
                    new PollQuestion(7, "How equiped was the ordination?"),
                    new PollQuestion(8, "How are you satasfied with our waiting room?"),
                }),
            };

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
            Appointment appointment = (Appointment)cbAppointments.SelectedItem;
            poll.Username = uc.CurentLoggedUser.Username;
            poll.AppointmentId = appointment.Id;
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

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 10,
                offsetY: 10);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });
    }
}
