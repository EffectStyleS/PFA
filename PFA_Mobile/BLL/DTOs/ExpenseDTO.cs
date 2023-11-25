using DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs
{
    public class ExpenseDTO
    {
        public ExpenseDTO() { }

        public ExpenseDTO(Expense expense)
        {
            Id            = expense.Id;
            Name          = expense.Name;
            Value         = expense.Value;
            Date          = expense.Date;
            ExpenseTypeId = expense.ExpenseTypeId;
            UserId        = expense.UserId;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
        public int ExpenseTypeId { get; set; }
        public int UserId { get; set; }
    }
}
