using DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
    public class IncomeDTO
    {
        public IncomeDTO() {}

        public IncomeDTO(Income income)
        {
            Id           = income.Id;
            Name         = income.Name;
            Value        = income.Value;
            Date         = income.Date;
            UserId       = income.UserId;
            IncomeTypeId = income.IncomeTypeId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public int IncomeTypeId { get; set; }
    }
}
