using HexaEmployee.Domain.Entities;
using HexaEmployee.Domain.Exceptions;
using HexaEmployee.Domain.Notifications;
using HexaEmployee.Domain.Repositories;
using HexaEmployee.Shared.Data.Contexts;
using System;

namespace HexaEmployee.Domain.Services
{
    public class SampleService : ISampleService
    {
        private readonly ISampleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotification _notifications;

        public SampleService(
            ISampleRepository repository,
            IUnitOfWork unitOfWork,
            INotification notifications)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _notifications = notifications;
        }

        public SampleEntity GetSampleBy(int id)
        {
            _unitOfWork.BeginTransaction();
            try
            {
                var sample = _repository.GetById(id);

                _unitOfWork.Commit();

                return sample;
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                _notifications.AddException(ex, ErrorCode.PersistingError);
                return null;
            }
        }
    }
}
