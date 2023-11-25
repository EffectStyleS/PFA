using client.Model.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace client.ViewModel
{
    public partial class BudgetOverrunsPopupViewModel : BaseViewModel
    {
        public BudgetOverrunsPopupViewModel(UserModel user)
        {
            // TODO: из сервиса подгружать
            BudgetOverruns = new ObservableCollection<BudgetOverrunsModel>()
            {
                new BudgetOverrunsModel() 
                {
                    BudgetName = "Budget 1",
                    ExpenseType = "бубубу",
                    Difference = 2174182 
                },

                new BudgetOverrunsModel()
                {
                    BudgetName = "Budget 2",
                    ExpenseType = "бубубу",
                    Difference = 2174182
                },

                new BudgetOverrunsModel()
                {
                    BudgetName = "Budget 3",
                    ExpenseType = "бубубу",
                    Difference = 2174182
                },

                new BudgetOverrunsModel()
                {
                    BudgetName = "Budget 4",
                    ExpenseType = "бубубу",
                    Difference = 2174182
                },
            };

            PageTitle = "Budget Overruns";
        }

        [ObservableProperty]
        string _pageTitle;

        [ObservableProperty]
        ObservableCollection<BudgetOverrunsModel> _budgetOverruns;
    }
}
