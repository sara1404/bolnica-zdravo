using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using System.Collections.ObjectModel;

namespace Controller
{
    public class DoctorController
    {
        private readonly DoctorService doctorService;

        public DoctorController(DoctorService _service)
        {
            doctorService = _service;
        }
        public DoctorController() { }

        public ObservableCollection<Doctor> GetDoctors()
        {
            return doctorService.GetDoctors();
        }
        
        public ObservableCollection<Doctor> GetGeneralPractitioners()
        {
            throw new NotImplementedException();
        }

        public Doctor getByName(string name)
        {
            return doctorService.getByName(name);
        }


        public Doctor GetByUsername(string username)
        {
            return doctorService.GetByUsername(username);
        }
    }
}
