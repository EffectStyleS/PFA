using client.View;
using client.ViewModel;

namespace client
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel vm)
        {
            Routing.RegisterRoute(nameof(StartMenu), typeof(StartMenu));
            Routing.RegisterRoute(nameof(LoginMenu), typeof(LoginMenu));
            Routing.RegisterRoute(nameof(SignUpMenu), typeof(SignUpMenu));
            Routing.RegisterRoute(nameof(HistoryPage), typeof(HistoryPage));
            Routing.RegisterRoute(nameof(IncomesMenu), typeof(IncomesMenu));
            Routing.RegisterRoute(nameof(ExpensesMenu), typeof(ExpensesMenu));
            Routing.RegisterRoute(nameof(GoalsMenu), typeof(GoalsMenu));
            Routing.RegisterRoute(nameof(BudgetsMenu), typeof(BudgetsMenu));
            Routing.RegisterRoute(nameof(BudgetAddEdit), typeof(BudgetAddEdit));
            Routing.RegisterRoute(nameof(IncomesStatisticsPage), typeof(IncomesStatisticsPage));
            Routing.RegisterRoute(nameof(ExpensesStatisticsPage), typeof(ExpensesStatisticsPage));
            Routing.RegisterRoute(nameof(GoalsStatisticsPage), typeof(GoalsStatisticsPage));

            BindingContext = vm;

            InitializeComponent();
        }
    }
}