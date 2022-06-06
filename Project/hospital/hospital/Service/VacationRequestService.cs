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
        private readonly VacationRequestRepository _vacationRequestRepository;
        private readonly NotificationRepository _notificationRepository;
        public VacationRequestService(VacationRequestRepository _repo, NotificationRepository notiRepo)
        {
            _vacationRequestRepository = _repo;
            _notificationRepository = notiRepo;
        }
        public bool Create(VacationRequest vacationRequest)
        {
            return _vacationRequestRepository.Create(vacationRequest);
        }
        public ObservableCollection<VacationRequest> FindAll()
        {
            return _vacationRequestRepository.FindAll();
        }
        public VacationRequest FindById(int id)
        {
            return _vacationRequestRepository.FindById(id);
        }
        public VacationRequest FindByDoctorId(string doctorId)
        {
            return _vacationRequestRepository.FindByDoctorId(doctorId);
        }
        public bool DeleteById(int id)
        {
            return _vacationRequestRepository.DeleteById(id);
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
            ChangeStatusOfRequest(requestId, Status.approved);
            MakeNotification(requestId, "Your vacation request was approved.");
        }
        private void RejectRequest(int requestId)
        {
            ChangeStatusOfRequest(requestId,Status.rejected);
            MakeNotification(requestId, "Your vacation request was rejected.");

        }
        private void ChangeStatusOfRequest(int requestId,Status status)
        {
            _vacationRequestRepository.UpdateStatus(requestId, status);
        }
        private void MakeNotification(int requestId,string notificationText)
        {
            Notification notification = new Notification(FindById(requestId).DoctorId, notificationText);
            _notificationRepository.Create(notification);
        }
    }
}
