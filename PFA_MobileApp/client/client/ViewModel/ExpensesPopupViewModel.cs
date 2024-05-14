using ApiClient;
using client.Model.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;
using System.Collections.ObjectModel;

namespace client.ViewModel;

/// <summary>
/// Модель представления попапа расходов
/// </summary>
public partial class ExpensesPopupViewModel : BaseViewModel
{
    private readonly Client _client;
    private readonly IPopupNavigation _popupNavigation;
    private readonly ObservableCollection<ExpenseModel> _expenses;
    private readonly ExpenseModel _expense;
    private readonly bool _isEdit;

    /// <summary>
    /// Модель представления попапа расходов
    /// </summary>
    /// <param name="popupNavigation">Навигация попапов</param>
    /// <param name="client">Клиент</param>
    /// <param name="expense">Расход</param>
    /// <param name="expenses">Расход</param>
    /// <param name="expenseTypes">Типы расходов</param>
    /// <param name="isEdit">Признак редактирования</param>
    public ExpensesPopupViewModel(IPopupNavigation popupNavigation, Client client, ExpenseModel expense,
        ObservableCollection<ExpenseModel> expenses, List<ExpenseTypeModel> expenseTypes, bool isEdit)
    {
        _client = client;
        _popupNavigation = popupNavigation;

        _expense = expense;
        _expenses = expenses;
        ExpenseTypes = new ObservableCollection<ExpenseTypeModel>(expenseTypes);
        _isEdit = isEdit;

        if (_isEdit)
        {
            PageTitle = "Edit Expense";

            Name = expense.Name;
            Date = expense.Date;
            Value = expense.Value;
            ExpenseType = ExpenseTypes.FirstOrDefault(x => x.Id == expense.ExpenseTypeId)!;

            return;
        }

        PageTitle = "Add Expense";

        Date = DateTime.Today;
        Value = 0;
        ExpenseType = ExpenseTypes[0];
    }

    /// <summary>
    /// Название страницы
    /// </summary>
    [ObservableProperty] private string _pageTitle;

    /// <summary>
    /// Название
    /// </summary>
    [ObservableProperty] private string _name;

    /// <summary>
    /// Значение
    /// </summary>
    [ObservableProperty] private decimal? _value;

    /// <summary>
    /// Дата
    /// </summary>
    [ObservableProperty] private DateTime _date;

    /// <summary>
    /// Типы расходов
    /// </summary>
    [ObservableProperty] private ObservableCollection<ExpenseTypeModel> _expenseTypes;

    /// <summary>
    /// Тип расхода
    /// </summary>
    [ObservableProperty] private ExpenseTypeModel _expenseType;

    /// <summary>
    /// Команда сохранения
    /// </summary>
    [RelayCommand]
    private async Task Save()
    {
        _expense.Name = Name ?? "New Expense";
        _expense.Value = Value ?? 0;
        _expense.Date = Date;
        _expense.ExpenseTypeId = ExpenseType.Id;
        _expense.ExpenseType = ExpenseType.Name;

        ExpenseDto postResult = new();

        try
        {
            if (_isEdit)
            {
                var expenseRequest = new ExpenseDto
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
                    new ExpenseDto
                    {
                        Name = _expense.Name,
                        Value = (double)_expense.Value,
                        Date = _expense.Date,
                        ExpenseTypeId = _expense.ExpenseTypeId,
                        UserId = _expense.UserId,
                    });
            }
        }
        catch
        {
            var mainPage = Application.Current!.MainPage;
            if (mainPage is not null)
            {
                await mainPage.DisplayAlert("Fail", Resources.AddEditExpenseFailed, "OK");
            }
                
            return;
        }

        // обновление списка расходов
        if (_isEdit)
        {
            var found = _expenses.FirstOrDefault(x => x.Id == _expense.Id);
            if (found is not null)
            {
                var i = _expenses.IndexOf(found);
                _expenses[i] = _expense;
            }
        }
        else
        {
            _expenses.Add(new ExpenseModel
            {
                Id = postResult.Id,
                Name = postResult.Name,
                Value = (decimal)postResult.Value,
                Date = postResult.Date.DateTime,
                ExpenseTypeId = postResult.ExpenseTypeId,
                ExpenseType = ExpenseTypes.FirstOrDefault(x => x.Id == postResult.ExpenseTypeId)!.Name,
                UserId = postResult.UserId
            });
        }

        var orderedExpenses = _expenses.OrderByDescending(x => x.Date).ToList();

        _expenses.Clear();

        foreach (var expense in orderedExpenses)
        {
            _expenses.Add(expense);
        }

        await _popupNavigation.PopAsync();
    }

    /// <summary>
    /// Команда отмены
    /// </summary>
    /// <returns></returns>
    [RelayCommand]
    Task Cancel()
    {
        return _popupNavigation.PopAsync();
    }
}