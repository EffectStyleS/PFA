using DAL.Entities;

namespace BLL.DTOs
{
    public class IncomeTypeDTO
    {
        public IncomeTypeDTO() {}

        public IncomeTypeDTO(IncomeType incomeType)
        {
            Id   = incomeType.Id;
            Name = incomeType.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
