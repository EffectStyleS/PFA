using client.Model.Models;
using client.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace client.ViewModel
{
    public partial class BudgetsMenuViewModel : BaseViewModel
    {

        public BudgetsMenuViewModel()
        {
            // TODO: из сервиса получим потом
            Budgets = new ObservableCollection<BudgetModel>
            {
                new BudgetModel()
                {
                    Id = 1,
                    Name = "Budget 1",
                    StartDate = DateTime.Now,
                    TimePeriodId = 0,
                    TimePeriod = "Месяц",
                    PlannedExpenses = new List<PlannedExpensesModel>
                    {
                        new PlannedExpensesModel()
                        {
                            Sum = 12,
                            ExpenseTypeId = 0,
                            ExpenseType = "бубубу"
                        },

                        new PlannedExpensesModel()
                        {
                            Sum = 12,
                            ExpenseTypeId = 1,
                            ExpenseType = "бубубу"
                        },

                        new PlannedExpensesModel()
                        {
                            Sum = 12,
                            ExpenseTypeId = 2,
                            ExpenseType = "бубубу"
                        },

                        new PlannedExpensesModel()
                        {
                            Sum = 12,
                            ExpenseTypeId = 3,
                            ExpenseType = "бубубу"
                        },
                    },

                    PlannedIncomes = new List<PlannedIncomesModel>
                    {
                        new PlannedIncomesModel()
                        {
                            Sum = 64,
                            IncomeTypeId = 0,
                            IncomeType = "бубубу"
                        },

                        new PlannedIncomesModel()
                        {
                            Sum = 65,
                            IncomeTypeId = 1,
                            IncomeType = "бубубу"
                        },

                        new PlannedIncomesModel()
                        {
                            Sum = 63,
                            IncomeTypeId = 2,
                            IncomeType = "бубубу"
                        },

                        new PlannedIncomesModel()
                        {
                            Sum = 543,
                            IncomeTypeId = 3,
                            IncomeType = "бубубу"
                        },
                    },
                },

                new BudgetModel()
                {
                    Id = 1,
                    Name = "Budget 2",
                    StartDate = DateTime.Now,
                    TimePeriodId = 0,
                    TimePeriod = "Месяц",
                    PlannedExpenses = new List<PlannedExpensesModel>
                    {
                        new PlannedExpensesModel()
                        {
                            Sum = 12,
                            ExpenseTypeId = 0,
                            ExpenseType = "бубубу"
                        },

                        new PlannedExpensesModel()
                        {
                            Sum = 12,
                            ExpenseTypeId = 1,
                            ExpenseType = "бубубу"
                        },

                        new PlannedExpensesModel()
                        {
                            Sum = 12,
                            ExpenseTypeId = 2,
                            ExpenseType = "бубубу"
                        },

                        new PlannedExpensesModel()
                        {
                            Sum = 12,
                            ExpenseTypeId = 3,
                            ExpenseType = "бубубу"
                        },
                    },

                    PlannedIncomes = new List<PlannedIncomesModel>
                    {
                        new PlannedIncomesModel()
                        {
                            Sum = 64,
                            IncomeTypeId = 0,
                            IncomeType = "бубубу"
                        },

                        new PlannedIncomesModel()
                        {
                            Sum = 65,
                            IncomeTypeId = 1,
                            IncomeType = "бубубу"
                        },

                        new PlannedIncomesModel()
                        {
                            Sum = 63,
                            IncomeTypeId = 2,
                            IncomeType = "бубубу"
                        },

                        new PlannedIncomesModel()
                        {
                            Sum = 543,
                            IncomeTypeId = 3,
                            IncomeType = "бубубу"
                        },
                    },
                },

                new BudgetModel()
                {
                    Id = 1,
                    Name = "Budget 3",
                    StartDate = DateTime.Now,
                    TimePeriodId = 0,
                    TimePeriod = "Месяц",
                    PlannedExpenses = new List<PlannedExpensesModel>
                    {
                        new PlannedExpensesModel()
                        {
                            Sum = 12,
                            ExpenseTypeId = 0,
                            ExpenseType = "бубубу"
                        },

                        new PlannedExpensesModel()
                        {
                            Sum = 12,
                            ExpenseTypeId = 1,
                            ExpenseType = "бубубу"
                        },

                        new PlannedExpensesModel()
                        {
                            Sum = 12,
                            ExpenseTypeId = 2,
                            ExpenseType = "бубубу"
                        },

                        new PlannedExpensesModel()
                        {
                            Sum = 12,
                            ExpenseTypeId = 3,
                            ExpenseType = "бубубу"
                        },
                    },

                    PlannedIncomes = new List<PlannedIncomesModel>
                    {
                        new PlannedIncomesModel()
                        {
                            Sum = 64,
                            IncomeTypeId = 0,
                            IncomeType = "бубубу"
                        },

                        new PlannedIncomesModel()
                        {
                            Sum = 65,
                            IncomeTypeId = 1,
                            IncomeType = "бубубу"
                        },

                        new PlannedIncomesModel()
                        {
                            Sum = 63,
                            IncomeTypeId = 2,
                            IncomeType = "бубубу"
                        },

                        new PlannedIncomesModel()
                        {
                            Sum = 543,
                            IncomeTypeId = 3,
                            IncomeType = "бубубу"
                        },
                    },
                },

                new BudgetModel()
                {
                    Id = 1,
                    Name = "Budget 4",
                    StartDate = DateTime.Now,
                    TimePeriodId = 0,
                    TimePeriod = "Месяц",
                    PlannedExpenses = new List<PlannedExpensesModel>
                    {
                        new PlannedExpensesModel()
                        {
                            Sum = 12,
                            ExpenseTypeId = 0,
                            ExpenseType = "бубубу"
                        },

                        new PlannedExpensesModel()
                        {
                            Sum = 12,
                            ExpenseTypeId = 1,
                            ExpenseType = "бубубу"
                        },

                        new PlannedExpensesModel()
                        {
                            Sum = 12,
                            ExpenseTypeId = 2,
                            ExpenseType = "бубубу"
                        },

                        new PlannedExpensesModel()
                        {
                            Sum = 12,
                            ExpenseTypeId = 3,
                            ExpenseType = "бубубу"
                        },
                    },

                    PlannedIncomes = new List<PlannedIncomesModel>
                    {
                        new PlannedIncomesModel()
                        {
                            Sum = 64,
                            IncomeTypeId = 0,
                            IncomeType = "бубубу"
                        },

                        new PlannedIncomesModel()
                        {
                            Sum = 65,
                            IncomeTypeId = 1,
                            IncomeType = "бубубу"
                        },

                        new PlannedIncomesModel()
                        {
                            Sum = 63,
                            IncomeTypeId = 2,
                            IncomeType = "бубубу"
                        },

                        new PlannedIncomesModel()
                        {
                            Sum = 543,
                            IncomeTypeId = 3,
                            IncomeType = "бубубу"
                        },
                    },
                }
            };

            PageTitle = "Budgets";
        }

        [ObservableProperty]
        string _pageTitle;

        [ObservableProperty]
        ObservableCollection<BudgetModel> _budgets;

        [RelayCommand]
        void DeleteBudget()
        {
            // TODO: удаление сервисом
        }

        [RelayCommand]
        async Task AddBudget()
        {
            // TODO: добавление сервисом
            // переход через shell с query параметром - бюджет
            var navigationParameter = new Dictionary<string, object>
            {
                {
                    "Budget",
                    new BudgetModel()
                    {
                        Id = -1,
                        UserId = -1,
                        PlannedExpenses = new List<PlannedExpensesModel>
                        {
                            new PlannedExpensesModel()
                            {
                                ExpenseTypeId = 0,
                                ExpenseType = "бубубу"
                            },

                            new PlannedExpensesModel()
                            {
                                ExpenseTypeId = 1,
                                ExpenseType = "бубубу"
                            },

                            new PlannedExpensesModel()
                            {
                                ExpenseTypeId = 2,
                                ExpenseType = "бубубу"
                            },

                            new PlannedExpensesModel()
                            {
                                ExpenseTypeId = 3,
                                ExpenseType = "бубубу"
                            },
                        },
                        PlannedIncomes = new List<PlannedIncomesModel>
                        {
                            new PlannedIncomesModel()
                            {
                                IncomeTypeId = 0,
                                IncomeType = "бубубу"
                            },

                            new PlannedIncomesModel()
                            {
                                IncomeTypeId = 1,
                                IncomeType = "бубубу"
                            },

                            new PlannedIncomesModel()
                            {
                                IncomeTypeId = 2,
                                IncomeType = "бубубу"
                            },

                            new PlannedIncomesModel()
                            {
                                IncomeTypeId = 3,
                                IncomeType = "бубубу"
                            },
                        },
                    }
                }
            };

            await Shell.Current.GoToAsync($"{nameof(BudgetAddEdit)}", navigationParameter);
            //await _popupNavigation.PushAsync(new BudgetsPopup(new BudgetsPopupViewModel()));
        }

        [RelayCommand]
        async Task EditBudget(BudgetModel budget)
        {
            // TODO: изменение сервисом
            var navigationParameter = new Dictionary<string, object>
            {
                { "Budget", budget }
            };
            await Shell.Current.GoToAsync($"{nameof(BudgetAddEdit)}", navigationParameter);
            //await _popupNavigation.PushAsync(new BudgetsPopup(new BudgetsPopupViewModel(budget)));
        }

    }
}
