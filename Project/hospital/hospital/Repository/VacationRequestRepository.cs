using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class VacationRequestRepository
    {
        public ObservableCollection<VacationRequest> vacationRequests;
        public VacationRequestRepository()
        {
            vacationRequests = new ObservableCollection<VacationRequest>();
        }
        public bool Create(VacationRequest vacationRequest)
        {
            vacationRequests.Add(vacationRequest);
            return true;
        }
        public ObservableCollection<VacationRequest> FindAll()
        {
            return vacationRequests;
        }
        public VacationRequest FindById(int id)
        {
            foreach(VacationRequest vacationRequest in vacationRequests)
            {
                if(vacationRequest.Id == id)
                {
                    return vacationRequest;
                }
            }
            return null;
        }
        public VacationRequest FindByDoctorId(string doctorId)
        {
            foreach (VacationRequest vacationRequest in vacationRequests)
            {
                if (vacationRequest.DoctorId == doctorId)
                {
                    return vacationRequest;
                }
            }
            return null;
        }
        public bool DeleteById(int id)
        {
            vacationRequests.Remove(FindById(id));
            return true;
        }
    }
}
