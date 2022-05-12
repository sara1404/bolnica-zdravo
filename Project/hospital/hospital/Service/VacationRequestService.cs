using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class VacationRequestService
    {
        private readonly VacationRequestRepository vacationRequestRepository;
        public VacationRequestService(VacationRequestRepository _repo)
        {
            vacationRequestRepository = _repo;
        }
        public bool Create(VacationRequest vacationRequest)
        {
            return vacationRequestRepository.Create(vacationRequest);
        }
        public ObservableCollection<VacationRequest> FindAll()
        {
            return vacationRequestRepository.FindAll();
        }
        public VacationRequest FindById(int id)
        {
            return vacationRequestRepository.FindById(id);
        }
        public VacationRequest FindByDoctorId(string doctorId)
        {
            return vacationRequestRepository.FindByDoctorId(doctorId);
        }
        public bool DeleteById(int id)
        {
            return vacationRequestRepository.DeleteById(id);
        }
    }
}
