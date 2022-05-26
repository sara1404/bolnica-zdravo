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
        private readonly NotificationRepository notificationRepository;
        public VacationRequestService(VacationRequestRepository _repo, NotificationRepository notiRepo)
        {
            vacationRequestRepository = _repo;
            notificationRepository = notiRepo;
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

        public void FinishRequest(string resultRequest, int requestId)
        {
            if (resultRequest.Equals("reject"))
                RejectRequest(requestId);
            else
                ApproveRequest(requestId);
        }
        private void ApproveRequest(int requestId)
        {
            MakeNotification(requestId, "Your vacation request was approved.");
        }
        private void RejectRequest(int requestId)
        {
            MakeNotification(requestId, "Your vacation request was rejected.");

        }
        private void MakeNotification(int requestId,string notificationText)
        {
            Notification notification = new Notification(FindById(requestId).DoctorId, notificationText);
            notificationRepository.Create(notification);
            DeleteById(requestId);
        }
    }
}
