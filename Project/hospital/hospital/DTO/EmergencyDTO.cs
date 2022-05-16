using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace DTO
{
    public class EmergencyDTO
    {
        private string _patientUsername;
        private Specialization _requiredSpecialization;
        private bool _isOperation;


        public EmergencyDTO(string patientUsername, Specialization requiredSpecialization, bool isOperation)
        {
            this._patientUsername = patientUsername;
            this._requiredSpecialization = requiredSpecialization; 
            this._isOperation = isOperation;
        }

        public string PatientUsername { get { return _patientUsername; } set { _patientUsername = value; } }
        public Specialization RequiredSpecialization { get { return _requiredSpecialization; } set { _requiredSpecialization = value; } }
        public bool IsOperation { get { return _isOperation; } set { _isOperation = value; } }

    }
}
