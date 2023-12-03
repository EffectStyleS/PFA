using ApiClient;
using client.Model.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;
using System.Collections.ObjectModel;

namespace client.ViewModel
{
    public partial class IncomesPopupViewModel : BaseViewModel
    {
        private readonly Client _client;
        private readonly IPopupNavigation _popupNavigation;
        private readonly ObservableCollection<IncomeModel> _incomes;
        private readonly IncomeModel _income;
        private readonly bool _isEdited; 

        public IncomesPopupViewModel(IPopupNavigation popupNavigation, Client client, IncomeModel income, ObservableCollection<IncomeModel> incomes, List<IncomeTypeModel> incomeTypes, bool isEdited) 
        {
            _client = client;
            _popupNavigation = popupNavigation;

            _income = income;
            _incomes = incomes;
            IncomeTypes = new ObservableCollection<IncomeTypeModel>(incomeTypes);
            _isEdited = isEdited;

            if (_isEdited)
            {
                PageTitle = "Edit Income";

                Name = income.Name;
                Date = income.Date;
                Value = income.Value;
                IncomeType = IncomeTypes.Where(x => x.Id == income.IncomeTypeId).FirstOrDefault();
                return;
            }

            PageTitle = "Add Income";

            Date = DateTime.Today;
            Value = 0;
            IncomeType = IncomeTypes[0];
        }

        [ObservableProperty]
        string _pageTitle;

        [ObservableProperty]
        string _name;

        [ObservableProperty]
        decimal _value;

        [ObservableProperty]
        DateTime _date;

        [ObservableProperty]
        ObservableCollection<IncomeTypeModel> _incomeTypes;

        [ObservableProperty]
        IncomeTypeModel _incomeType;


        [RelayCommand]
        async Task Save()
        {
            _income.Name = Name;
            _income.Value = Value;
            _income.Date = Date;
            _income.IncomeTypeId = IncomeType.Id;
            _income.IncomeType = IncomeType.Name;

            IncomeDTO postResult = new();

            try
            {
                if (_isEdited)
                {
                    var incomeRequest = new IncomeDTO()
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
                        new IncomeDTO()
                        {
                            Name = _income.Name,
                            Value = (double)_income.Value,
                            Date = _income.Date,
                            IncomeTypeId = _income.IncomeTypeId,
                            UserId = _income.UserId,
                        });
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Fail", ex.Message, "OK");
                return;
            }
            
            // обновление списка доходов
            if (_isEdited) 
            {
                var found = _incomes.FirstOrDefault(x => x.Id == _income.Id);
                int i = _incomes.IndexOf(found);
                _incomes[i] = _income;
            }
            else
            {
                _incomes.Add(new IncomeModel()
                {
                    Id = postResult.Id,
                    Name = postResult.Name,
                    Value = (decimal)postResult.Value,
                    Date = postResult.Date.DateTime,
                    IncomeTypeId = postResult.IncomeTypeId,
                    IncomeType = IncomeTypes.Where(x => x.Id == postResult.IncomeTypeId).FirstOrDefault().Name,
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
