using ApiClient;
using client.Model.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;
using System.Collections.ObjectModel;


namespace client.ViewModel;

public partial class IncomesPopupViewModel : BaseViewModel
{
    private readonly Client _client;
    private readonly IPopupNavigation _popupNavigation;
    private readonly ObservableCollection<IncomeModel> _incomes;
    private readonly IncomeModel _income;
    private readonly bool _isEdit;

    public IncomesPopupViewModel(IPopupNavigation popupNavigation, Client client, IncomeModel income,
        ObservableCollection<IncomeModel> incomes, List<IncomeTypeModel> incomeTypes, bool isEdit)
    {
        _client = client;
        _popupNavigation = popupNavigation;

        _income = income;
        _incomes = incomes;
        IncomeTypes = new ObservableCollection<IncomeTypeModel>(incomeTypes);
        _isEdit = isEdit;

        if (_isEdit)
        {
            PageTitle = "Edit Income";

            Name = income.Name;
            Date = income.Date;
            Value = income.Value;
            IncomeType = IncomeTypes.FirstOrDefault(x => x.Id == income.IncomeTypeId)!;
            return;
        }

        PageTitle = "Add Income";

        Date = DateTime.Today;
        Value = 0;
        IncomeType = IncomeTypes[0];
    }

    [ObservableProperty] private string _pageTitle;

    [ObservableProperty] private string _name;

    [ObservableProperty] private decimal? _value;

    [ObservableProperty] private DateTime _date;

    [ObservableProperty] private ObservableCollection<IncomeTypeModel> _incomeTypes;

    [ObservableProperty] private IncomeTypeModel _incomeType;
        
    [RelayCommand]
    private async Task Save()
    {
        _income.Name = Name ?? "New Income";
        _income.Value = Value ?? 0;
        _income.Date = Date;
        _income.IncomeTypeId = IncomeType.Id;
        _income.IncomeType = IncomeType.Name;

        IncomeDto postResult = new();

        try
        {
            if (_isEdit)
            {
                var incomeRequest = new IncomeDto
                {
                    Id = _income.Id,
                    Name = _income.Name,
                    Value = (double)_income.Value,
                    Date = _income.Date,
                    IncomeTypeId = _income.IncomeTypeId,
                    UserId = _income.UserId,
                };

                await _client.IncomePUTAsync(_income.Id, incomeRequest);
            }
            else
            {
                postResult = await _client.IncomePOSTAsync(
                    new IncomeDto
                    {
                        Name = _income.Name,
                        Value = (double)_income.Value,
                        Date = _income.Date,
                        IncomeTypeId = _income.IncomeTypeId,
                        UserId = _income.UserId,
                    });
            }
        }
        catch
        {
            var mainPage = Application.Current!.MainPage;
            if (mainPage is not null)
            {
                await mainPage.DisplayAlert("Fail", Resources.AddEditIncomeFailed, "OK");
            }
                
            return;
        }
            
        // обновление списка доходов
        if (_isEdit) 
        {
            var found = _incomes.FirstOrDefault(x => x.Id == _income.Id);
            if (found is not null)
            {
                var i = _incomes.IndexOf(found);
                _incomes[i] = _income;
            }
        }
        else
        {
            _incomes.Add(new IncomeModel
            {
                Id = postResult.Id,
                Name = postResult.Name,
                Value = (decimal)postResult.Value,
                Date = postResult.Date.DateTime,
                IncomeTypeId = postResult.IncomeTypeId,
                IncomeType = IncomeTypes.FirstOrDefault(x => x.Id == postResult.IncomeTypeId)!.Name,
                UserId = postResult.UserId
            });
        }

        await _popupNavigation.PopAsync();
    }

    [RelayCommand]
    private Task Cancel()
    {
        return _popupNavigation.PopAsync();
    }
}