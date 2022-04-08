using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;

namespace hospital.Controller
{
    class DoctorController
    {
        private readonly DoctorService doctorService;

        public DoctorController()
        {
            doctorService = new DoctorService();
        }

        public List<Doctor> GetDoctors()
        {
            return doctorService.GetDoctors();
        }
        
        public List<Doctor> GetGeneralPractitioners()
        {
            throw new NotImplementedException();
        }
    }
}
