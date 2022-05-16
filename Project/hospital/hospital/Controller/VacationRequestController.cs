using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public class VacationRequestController
    {
        private readonly VacationRequestService vacationRequestService;
        public VacationRequestController(VacationRequestService _repo)
        {
            vacationRequestService = _repo;
        }
        public bool Create(VacationRequest vacationRequest)
        {
            return vacationRequestService.Create(vacationRequest);
        }
        public ObservableCollection<VacationRequest> FindAll()
        {
            return vacationRequestService.FindAll();
        }
        public VacationRequest FindById(int id)
        {
            return vacationRequestService.FindById(id);
        }
        public VacationRequest FindByDoctorId(string doctorId)
        {
            return vacationRequestService.FindByDoctorId(doctorId);
        }
        public bool DeleteById(int id)
        {
            return vacationRequestService.DeleteById(id);
        }
    }
}
