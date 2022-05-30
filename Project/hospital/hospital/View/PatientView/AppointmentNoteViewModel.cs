using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace hospital.View.PatientView
{
    public class AppointmentNoteViewModel
    {
        public string DoctorNote { get; set;}
        public string PatientNote { get; set; }
        public MyICommand ConfirmCommand { get; set; }
        private AppointmentController ac;
        private Appointment oldAppointment;

        public AppointmentNoteViewModel(Appointment appointment)
        {
            App app = Application.Current as App;
            ac = app.appointmentController;
            ConfirmCommand = new MyICommand(Confirm);
            DoctorNote = appointment.DoctorNote;
            PatientNote = appointment.PatientNote;
            oldAppointment = appointment;
        }

        private void Confirm()
        {
            Appointment newAppointment = oldAppointment;
            newAppointment.PatientNote = PatientNote;
            ac.UpdateAppointment(oldAppointment, newAppointment);
        }

    }
}
