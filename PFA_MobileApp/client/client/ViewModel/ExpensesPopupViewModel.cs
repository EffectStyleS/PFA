using ApiClient;
using client.Model.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;
using System.Collections.ObjectModel;

namespace client.ViewModel
{
    public partial class ExpensesPopupViewModel : BaseViewModel
    {
        private readonly Client _client;
        private readonly IPopupNavigation _popupNavigation;
        private readonly ObservableCollection<ExpenseModel> _expenses;
        private readonly ExpenseModel _expense;
        private readonly bool _isEdited;

        public ExpensesPopupViewModel(IPopupNavigation popupNavigation, Client client, ExpenseModel expense, ObservableCollection<ExpenseModel> expenses, List<ExpenseTypeModel> expenseTypes, bool isEdited)
        {
            _client = client;
            _popupNavigation = popupNavigation;

            _expense = expense;
            _expenses = expenses;
            ExpenseTypes = new ObservableCollection<ExpenseTypeModel>(expenseTypes);
            _isEdited = isEdited;

            if (_isEdited)
            {
                PageTitle = "Edit Expense";

                Name = expense.Name;
                Date = expense.Date;
                Value = expense.Value;
                ExpenseType = ExpenseTypes.Where(x => x.Id == expense.ExpenseTypeId).FirstOrDefault();

                return;
            }

            PageTitle = "Add Expense";

            Date = DateTime.Today;
            Value = 0;
            ExpenseType = ExpenseTypes[0];
        }

        [ObservableProperty]
        string _pageTitle;

        [ObservableProperty]
        string _name;

        [ObservableProperty]
        decimal? _value;

        [ObservableProperty]
        DateTime _date;

        [ObservableProperty]
        ObservableCollection<ExpenseTypeModel> _expenseTypes;

        [ObservableProperty]
        ExpenseTypeModel _expenseType;

        [RelayCommand]
        async Task Save()
        {
            _expense.Name = Name ?? "New Expense";
            _expense.Value = Value ?? 0;
            _expense.Date = Date;
            _expense.ExpenseTypeId = ExpenseType.Id;
            _expense.ExpenseType = ExpenseType.Name;

            ExpenseDTO postResult = new();

            try
            {
                if (_isEdited)
                {
                    var expenseRequest = new ExpenseDTO()
                    {
                        Id = _expense.Id,
                        Name = _expense.Name,
                        Value = (double)_expense.Value,
                        Date = _expense.Date,
                        ExpenseTypeId = _expense.ExpenseTypeId,
                        UserId = _expense.UserId,
                    };

                    await _client.ExpensePUTAsync(_expense.Id, expenseRequest);
                }
                else
                {
                    postResult = await _client.ExpensePOSTAsync(
                        new ExpenseDTO()
                        {
                            Name = _expense.Name,
                            Value = (double)_expense.Value,
                            Date = _expense.Date,
                            ExpenseTypeId = _expense.ExpenseTypeId,
                            UserId = _expense.UserId,
                        });
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Fail", ex.Message, "OK");
                return;
            }

            // обновление списка расходов
            if (_isEdited)
            {
                var found = _expenses.FirstOrDefault(x => x.Id == _expense.Id);
                int i = _expenses.IndexOf(found);
                _expenses[i] = _expense;
            }
            else
            {
                _expenses.Add(new ExpenseModel()
                {
                    Id = postResult.Id,
                    Name = postResult.Name,
                    Value = (decimal)postResult.Value,
                    Date = postResult.Date.DateTime,
                    ExpenseTypeId = postResult.ExpenseTypeId,
                    ExpenseType = ExpenseTypes.Where(x => x.Id == postResult.ExpenseTypeId).FirstOrDefault().Name,
                    UserId = postResult.UserId
                });
            }

            await _popupNavigation.PopAsync();
        }

        [RelayCommand]
        Task Cancel()
        {
            return _popupNavigation.PopAsync();
        }
    }
}
