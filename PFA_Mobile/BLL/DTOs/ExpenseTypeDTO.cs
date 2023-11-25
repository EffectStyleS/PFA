using DAL.Entities;

namespace BLL.DTOs
{
    public class ExpenseTypeDTO
    {
        public ExpenseTypeDTO() { }
        public ExpenseTypeDTO(ExpenseType expenseType)
        {
            Id   = expenseType.Id;
            Name = expenseType.Name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        // TODO: возможно, не нужно хранить и создавать расходы и запланированные расходы для каждого типа расхода, это же просто справочник
        // Если мы вдруг удалим тип расхода, то просто в expense поставим тип "Другое", а не удалим все расходы с таким типом
        // и пересчитаем сумму расходов и запланированных расходов для типа "Другое", тип "Другое" - неудаляемый
        // с временным периодом бюджета то же самое, просто в бюджет ставим временнной период Месяц, а месяц делаем неудаляемы
    }
}
