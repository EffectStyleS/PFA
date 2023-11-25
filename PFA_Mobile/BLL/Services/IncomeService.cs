using BLL.DTOs;
using BLL.Interfaces;
using BLL.Services.Abstract;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class IncomeService : BaseService, IIncomeService
    {
        public IncomeService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        #region ICrud Implementation
        public async Task<bool> Create(IncomeDTO itemDTO)
        {
            var income = new Income()
            {
                Id           = itemDTO.Id,
                Name         = itemDTO.Name,
                Value        = itemDTO.Value,
                Date         = itemDTO.Date,
                IncomeTypeId = itemDTO.IncomeTypeId,
                UserId       = itemDTO.UserId
            };

            await _unitOfWork.Income.Create(income);
            return await SaveAsync();
        }

        public async Task<bool> Delete(int id)
        {
            if (!await _unitOfWork.Income.Exists(id))
                return false;

            await _unitOfWork.Income.Delete(id);
            //if (result == false) // добавить лог недудачного удаления с id 
            return await SaveAsync();
        }

        public async Task<bool> Exists(int id) => await _unitOfWork.Income.Exists(id);

        public async Task<List<IncomeDTO>> GetAll()
        {
            var items = await _unitOfWork.Income.GetAll();

            var result = items
                .Select(x => new IncomeDTO(x))
                .ToList();

            return result;
        }

        public async Task<IncomeDTO> GetById(int id)
        {
            var item = await _unitOfWork.Income.GetItem(id);

            return item == null ? null : new IncomeDTO(item);
        }

        public async Task<bool> Update(IncomeDTO itemDTO)
        {
            if (!await _unitOfWork.Income.Exists(itemDTO.Id))
                return false;

            Income x = await _unitOfWork.Income.GetItem(itemDTO.Id);

            x.Id           = itemDTO.Id;
            x.Name         = itemDTO.Name;
            x.Value        = itemDTO.Value;
            x.Date         = itemDTO.Date;
            x.IncomeTypeId = itemDTO.IncomeTypeId;
            x.UserId       = itemDTO.UserId;

            await _unitOfWork.Income.Update(x);
            return await SaveAsync();
        }
        #endregion

        public async Task<List<IncomeDTO>> GetAllUserIncomes(int userId)
        {
            var items = await _unitOfWork.Income.GetAll();

            var result = items
                .Select(x => new IncomeDTO(x))
                .Where(x => x.UserId == userId) 
                .ToList();

            return result;
        }
    }
}
