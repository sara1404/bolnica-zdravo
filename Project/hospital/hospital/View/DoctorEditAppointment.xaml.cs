using Controller;
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
using System.Windows.Shapes;

namespace hospital.View
{
    /// <summary>
    /// Interaction logic for DoctorEditAppointment.xaml
    /// </summary>
    public partial class DoctorEditAppointment : Window
    {
        private AppointmentController ac;
        private RoomController rc;
        private Appointment selectedAppointment;
        public DoctorEditAppointment()
        {
            InitializeComponent();
            App app = Application.Current as App;
            ac = app.appointmentController;
            rc = app.roomController;
            this.DataContext = this;
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(DoctorAppointmentsWindow))
                {
                    selectedAppointment = (window as DoctorAppointmentsWindow).Table.SelectedItem as Appointment;
                    tbPatient.Text = selectedAppointment.patient.ToString(); //moze i a.PatientName()
                    date.SelectedDate = selectedAppointment.StartTime;
                    tbDescription.Text = selectedAppointment.Description;
                    if (selectedAppointment.operationRoom != null)
                    {
                        cmbOpRoom.ItemsSource = rc.FindAll();
                        //cmbOpRoom.SelectedItem = (Room)selectedAppointment.operationRoom;
                        cmbOpRoom.SelectedIndex = 0; //za sad je zakucano
                        cbOperation.IsChecked = true;
                        cmbOpRoom.IsEnabled = true;
                    }
                }
            }
            
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            if (date.SelectedDate != null)
            {
                appointmentTable.ItemsSource = ac.GetFreeAppointmentsByDateAndDoctor((DateTime)date.SelectedDate, selectedAppointment.doctor.Username);
            }
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (appointmentTable.SelectedItem != null)
            {

                Appointment updatedAppointment = (Appointment)appointmentTable.SelectedItem;
                updatedAppointment.Description = tbDescription.Text;
                if (cmbOpRoom.SelectedIndex != -1)
                    updatedAppointment.operationRoom = (Room)cmbOpRoom.SelectedItem;
                ac.UpdateAppointment(selectedAppointment, updatedAppointment);
                this.Close();
            }
        }

        private void cbOperation_Checked(object sender, RoutedEventArgs e)
        {
            if(cbOperation.IsChecked == true)
            {
                cmbOpRoom.IsEnabled = true;
            }
        }

        private void cbOperation_Unchecked(object sender, RoutedEventArgs e)
        {
            if (cbOperation.IsChecked == false)
            {
                cmbOpRoom.IsEnabled = false;
            }
        }
    }
}
