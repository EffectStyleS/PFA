using client.Model.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using System.Collections.ObjectModel;

namespace client.ViewModel
{
    public partial class ExpensesPopupViewModel : BaseViewModel
    {
        public ExpensesPopupViewModel(ExpenseModel expense = null)
        {
            PageTitle = expense == null ? "Add Expense" : "Edit Expense";

            Id = expense?.Id;
            Name = expense?.Name;
            Value = expense?.Value;
            Date = expense?.Date;
            ExpenseTypeId = expense == null ? 0 : expense.ExpenseTypeId;

            // TODO: из сервиса подгружать
            ExpenseTypes = new ObservableCollection<ExpenseTypeModel>()
            {
                new ExpenseTypeModel() { Name = "Type 1" },
                new ExpenseTypeModel() { Name = "Type 2" },
                new ExpenseTypeModel() { Name = "Type 3" },
                new ExpenseTypeModel() { Name = "Type 4" },
            };
        }

        [ObservableProperty]
        string _pageTitle;

        [ObservableProperty]
        int? _id;

        [ObservableProperty]
        string _name;

        [ObservableProperty]
        decimal? _value;

        [ObservableProperty]
        DateTime? _date;

        [ObservableProperty]
        int _expenseTypeId;

        [ObservableProperty]
        ObservableCollection<ExpenseTypeModel> _expenseTypes;

        [RelayCommand]
        async Task Save()
        {
            // TODO: реализовать сохранение
            await MopupService.Instance.PopAsync();

        }

        [RelayCommand]
        async Task Cancel()
        {
            // TODO: реализовать отмену
            await MopupService.Instance.PopAsync();
        }
    }
}
