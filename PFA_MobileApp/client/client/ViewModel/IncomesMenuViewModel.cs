using client.Model.Models;
using client.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;
using System.Collections.ObjectModel;

namespace client.ViewModel
{
    public partial class IncomesMenuViewModel : BaseViewModel
    {
        IPopupNavigation _popupNavigation;

        public IncomesMenuViewModel(IPopupNavigation popupNavigation)
        {
            _popupNavigation = popupNavigation;
            // TODO: из сервиса получим потом
            Incomes = new ObservableCollection<IncomeModel>
            {
                new IncomeModel()
                {
                    Name = "Доход 1",
                    Date = DateTime.Now,
                    Value = 12523,
                    IncomeTypeId = 0,
                    IncomeType = "бубубу"
                },

                new IncomeModel()
                {
                    Name = "Доход 2",
                    Date = DateTime.Now,
                    Value = 12523,
                    IncomeTypeId = 0,
                    IncomeType = "бубубу"
                },

                new IncomeModel()
                {
                    Name = "Доход 3",
                    Date = DateTime.Now,
                    Value = 12523,
                    IncomeTypeId = 0,
                    IncomeType = "бубубу"
                },

                new IncomeModel()
                {
                    Name = "Доход 4",
                    Date = DateTime.Now,
                    Value = 12523,
                    IncomeTypeId = 0,
                    IncomeType = "бубубу"
                },
            };

            PageTitle = "Incomes";
        }

        [ObservableProperty]
        string _pageTitle;

        [ObservableProperty]
        ObservableCollection<IncomeModel> _incomes;

        [RelayCommand]
        void DeleteIncome()
        {
            // TODO: удаление сервисом
        }

        [RelayCommand]
        async Task AddIncome()
        {
            // TODO: добавление сервисом
            // переделать вызов, передавая новый доход без id, но с userId
            await _popupNavigation.PushAsync(new IncomesPopup(new IncomesPopupViewModel()));
        }

        [RelayCommand]
        async Task EditIncome(IncomeModel income)
        {
            // TODO: изменение сервисом
            await _popupNavigation.PushAsync(new IncomesPopup(new IncomesPopupViewModel(income)));
        }
    }
}
