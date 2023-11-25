using BLL.DTOs;
using BLL.Interfaces;
using BLL.Services.Abstract;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class TimePeriodService : BaseService, ITimePeriodService
    {
        public TimePeriodService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        #region ICrud Implementation
        public async Task<bool> Create(TimePeriodDTO itemDTO)
        {
            var timePeriod = new TimePeriod()
            {
                Id   = itemDTO.Id,
                Name = itemDTO.Name,
            };

            await _unitOfWork.TimePeriod.Create(timePeriod);
            return await SaveAsync();
        }

        public async Task<bool> Delete(int id)
        {
            if (!await _unitOfWork.TimePeriod.Exists(id))
                return false;

            // Период "Месяц" - неудаляемый,
            // TODO: сделать на клиенте неудаляемым, возможно задать имя неудаляемого таймпериода где-нибудь в другом месте?
            var timePeriod = await _unitOfWork.TimePeriod.GetItem(id);
            if (timePeriod.Name == "Месяц")
            {
                //if (result == false) // добавить лог недудачного удаления "Месяц"
                return false;
            }

            // Заменяем TimePeriodId у бюджетов на id периода "Месяц"
            var allTimePeriods = await _unitOfWork.TimePeriod.GetAll();
            var monthId = allTimePeriods.FirstOrDefault(x => x.Name == "Месяц").Id; // период "Месяц" всегда будет найден

            var thisTimePeriodBudgets = await _unitOfWork.Budget.GetAll();
            foreach (var budget in thisTimePeriodBudgets.Where(x => x.TimePeriodId == id).ToList())
            {
                budget.TimePeriodId = monthId;
            }

            await _unitOfWork.TimePeriod.Delete(id);
            //if (result == false) // добавить лог недудачного удаления с id 
            return await SaveAsync();
        }

        public async Task<bool> Exists(int id) => await _unitOfWork.TimePeriod.Exists(id);

        public async Task<List<TimePeriodDTO>> GetAll()
        {
            var items = await _unitOfWork.TimePeriod.GetAll();

            var result = items
                .Select(x => new TimePeriodDTO(x))
                .ToList();

            return result;
        }

        public async Task<TimePeriodDTO> GetById(int id)
        {
            var item = await _unitOfWork.TimePeriod.GetItem(id);

            return item == null ? null : new TimePeriodDTO(item);
        }

        public async Task<bool> Update(TimePeriodDTO itemDTO)
        {
            if (!await _unitOfWork.TimePeriod.Exists(itemDTO.Id))
                return false;

            TimePeriod x = await _unitOfWork.TimePeriod.GetItem(itemDTO.Id);

            x.Id   = itemDTO.Id;
            x.Name = itemDTO.Name;

            await _unitOfWork.TimePeriod.Update(x);
            return await SaveAsync();
        }
        #endregion
    }
}
