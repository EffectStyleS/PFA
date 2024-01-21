using ApiClient;
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
        private readonly IPopupNavigation _popupNavigation;
        private readonly Client _client;

        public IncomesMenuViewModel(IPopupNavigation popupNavigation, Client client)
        {
            _popupNavigation = popupNavigation;
            _client = client;

            PageTitle = "Incomes";
        }

        private async Task GetAllIncomeTypes()
        {
            if (IncomeTypes != null)
                return;

            List<IncomeTypeModel> result = new();

            var incomeTypesDto = await _client.IncomeTypeAllAsync();

            foreach (var incomeType in incomeTypesDto)
            {
                result.Add(new IncomeTypeModel() 
                { 
                    Id = incomeType.Id,
                    Name = incomeType.Name
                });
            }

            if (result.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Fail", "Null Income Types", "OK");
                return;
            }

            IncomeTypes = result;
        }

        public async Task CompleteDataAfterNavigation()
        {
            var userLogin = _client.GetCurrentUserLogin();
            var userDto = await _client.UserAsync(userLogin);
            User = new UserModel
            {
                Id = userDto.Id,
                Login = userDto.Login,
                RefreshToken = userDto.RefreshToken,
                RefreshTokenExpireTime = userDto.RefreshTokenExpiryTime.DateTime
            };

            await GetAllIncomeTypes().ConfigureAwait(false);
            Incomes = new ObservableCollection<IncomeModel>();

            ICollection<IncomeDTO> result = new List<IncomeDTO>();

            try
            {
                result = _client.UserAll4Async(User.Id).Result;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Fail", ex.Message, "OK");
                return;
            }

            foreach (var income in result)
            {
                Incomes.Add(new IncomeModel()
                {
                    Id = income.Id,
                    Name = income.Name,
                    Value = (decimal)income.Value,
                    Date = income.Date.DateTime,
                    IncomeTypeId = income.IncomeTypeId,
                    IncomeType = IncomeTypes.Where(x => x.Id == income.IncomeTypeId).FirstOrDefault().Name,
                    UserId = income.UserId
                });
            }
        }

        [ObservableProperty] private string _pageTitle;

        [ObservableProperty] private ObservableCollection<IncomeModel> _incomes;

        [ObservableProperty] private List<IncomeTypeModel> _incomeTypes;

        [ObservableProperty] private UserModel _user;

        [RelayCommand]
        private async Task DeleteIncome(IncomeModel income)
        {
            try
            {
                await _client.IncomeDELETEAsync(income.Id);
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Fail", ex.Message, "OK");
                return;
            }

            Incomes.Remove(income);
        }

        [RelayCommand]
        private async Task AddIncome()
        {
            IncomeModel income = new()
            {
                UserId = User.Id,
            };
            await _popupNavigation.PushAsync(new IncomesPopup(new IncomesPopupViewModel(_popupNavigation, _client, income, Incomes, IncomeTypes, false)))
                .ConfigureAwait(false);
        }

        [RelayCommand]
        private async Task EditIncome(IncomeModel income)
        {
            await _popupNavigation.PushAsync(new IncomesPopup(new IncomesPopupViewModel(_popupNavigation, _client, income, Incomes, IncomeTypes, true)))
                .ConfigureAwait(false);
        }
    }
}
