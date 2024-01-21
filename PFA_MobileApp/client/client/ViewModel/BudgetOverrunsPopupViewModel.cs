using ApiClient;
using client.Infrastructure;
using client.Model.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace client.ViewModel
{
    public partial class BudgetOverrunsPopupViewModel : BaseViewModel
    {
        private readonly Client _client;
        private readonly int _userId;

        public BudgetOverrunsPopupViewModel(Client client, int userId)
        {
            _client = client;
            _userId = userId;
            
            EventManager.OnUserExit += UserExitHandler;
            GetBudgetsDifferences();

            PageTitle = "Budget Overruns";
        }

        private async Task GetBudgetsDifferences()
        {
            ICollection<BudgetOverrunDTO> result = new List<BudgetOverrunDTO>();

            try
            {
                result = _client.DifferenceAsync(_userId).Result;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Fail", ex.Message, "OK");
                return;
            }

            BudgetOverruns = new ObservableCollection<BudgetOverrunsModel>();
            foreach (var item in result)
            {
                BudgetOverruns.Add(new BudgetOverrunsModel()
                {
                    BudgetName = item.BudgetName,
                    ExpenseType = item.ExpenseType,
                    Difference = (decimal)item.Difference!,
                });
            }
        }

        [ObservableProperty] private string _pageTitle;

        [ObservableProperty] private ObservableCollection<BudgetOverrunsModel> _budgetOverruns;
        
        private async Task UserExitHandler()
        {
            BudgetOverruns = new ObservableCollection<BudgetOverrunsModel>();
        }
    }
}
