using DAL.Interfaces;

namespace BLL.Services.Abstract
{
    public abstract class BaseService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected BaseService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        protected virtual async Task<bool> SaveAsync() => await _unitOfWork.Save() > 0;
    }
}
