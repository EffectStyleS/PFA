using BLL.DTOs;
using BLL.Interfaces;
using BLL.Services.Abstract;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class IncomeTypeService : BaseService, IIncomeTypeService
    {
        public IncomeTypeService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        #region ICrud Implementation
        public async Task<bool> Create(IncomeTypeDTO itemDTO)
        {
            var incomeType = new IncomeType()
            {
                Id   = itemDTO.Id,
                Name = itemDTO.Name,
            };

            await _unitOfWork.IncomeType.Create(incomeType);
            return await SaveAsync();
        }

        public async Task<bool> Delete(int id)
        {
            if (!await _unitOfWork.IncomeType.Exists(id))
                return false;

            // Тип "Другое" - неудаляемый,
            // TODO: сделать на клиенте неудаляемым
            var incomeType = await _unitOfWork.IncomeType.GetItem(id);
            if (incomeType.Name == "Другое")
            {
                //if (result == false) // добавить лог недудачного удаления "Другое"
                return false;
            }

            // Заменяем IncomeTypeId у доходов и
            // запланированных доходов на id типа дохода "Другое"
            var allIncomesTypes = await _unitOfWork.IncomeType.GetAll();
            var typeOtherId = allIncomesTypes.FirstOrDefault(x => x.Name == "Другое").Id; // тип "Другое" всегда будет найден

            var thisIncomeTypeIncomes = await _unitOfWork.Income.GetAll();
            foreach (var income in thisIncomeTypeIncomes.Where(x => x.IncomeTypeId == id).ToList())
            {
                income.IncomeTypeId = typeOtherId;
            }

            var thisIncomeTypePlannedIncomes = await _unitOfWork.PlannedIncomes.GetAll();
            foreach (var plannedIncomes in thisIncomeTypePlannedIncomes.Where(x => x.IncomeTypeId == id).ToList())
            {
                plannedIncomes.IncomeTypeId = typeOtherId;
            }

            await _unitOfWork.IncomeType.Delete(id);
            //if (result == false) // добавить лог недудачного удаления с id 
            return await SaveAsync();
        }

        public async Task<bool> Exists(int id) => await _unitOfWork.IncomeType.Exists(id);

        public async Task<List<IncomeTypeDTO>> GetAll()
        {
            var items = await _unitOfWork.IncomeType.GetAll();

            var result = items
                .Select(x => new IncomeTypeDTO(x))
                .ToList();

            return result;
        }

        public async Task<IncomeTypeDTO> GetById(int id)
        {
            var item = await _unitOfWork.IncomeType.GetItem(id);

            return item == null ? null : new IncomeTypeDTO(item);
        }

        public async Task<bool> Update(IncomeTypeDTO itemDTO)
        {
            if (!await _unitOfWork.IncomeType.Exists(itemDTO.Id))
                return false;

            IncomeType x = await _unitOfWork.IncomeType.GetItem(itemDTO.Id);

            x.Id   = itemDTO.Id;
            x.Name = itemDTO.Name;

            await _unitOfWork.IncomeType.Update(x);
            return await SaveAsync();
        }
        #endregion
    }
}
