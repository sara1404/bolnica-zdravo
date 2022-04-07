using System;

namespace Model
{
    public class Patient : User
    {
        private String firstName;
        private String lastName;
        private String id;
        private String dateOfBirth;
        private string phoneNumber;
        private bool isGuest;

        private MedicalRecord medicalRecord;

        public Appointment[] appointment;

    }
}