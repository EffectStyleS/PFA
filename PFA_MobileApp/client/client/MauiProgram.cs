using ApiClient;
using client.Infrastructure.Cache;
using client.Model.Interfaces;
using client.Model.Services;
using client.View;
using client.ViewModel;
using Mopups.Hosting;
using Mopups.Services;

namespace client
{
    public static class MauiProgram
    {
        private const string BaseUrl = "https://a6c2-94-25-227-3.ngrok-free.app";

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureMopups()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Montserrat-Black.ttf", "MontserratBlack");
                    fonts.AddFont("Montserrat-BlackItalic.ttf", "MontserratBlackItalic");
                    fonts.AddFont("Montserrat-Bold.ttf", "MontserratBold");
                    fonts.AddFont("Montserrat-BoldItalic.ttf", "MontserratBoldItalic");
                    fonts.AddFont("Montserrat-ExtraBold.ttf", "MontserratExtraBold");
                    fonts.AddFont("Montserrat-ExtraBoldItalic.ttf", "MontserratExtraBoldItalic");
                    fonts.AddFont("Montserrat-ExtraLight.ttf", "MontserratExtraLight");
                    fonts.AddFont("Montserrat-ExtraLightItalic.ttf", "MontserratExtraLightItalic");
                    fonts.AddFont("Montserrat-Italic.ttf", "MontserratItalic");
                    fonts.AddFont("Montserrat-Light.ttf", "MontserratLight");
                    fonts.AddFont("Montserrat-LightItalic.ttf", "MontserratLightItalic");
                    fonts.AddFont("Montserrat-Medium.ttf", "MontserratMedium");
                    fonts.AddFont("Montserrat-MediumItalic.ttf", "MontserratMediumItalic");
                    fonts.AddFont("Montserrat-Regular.ttf", "MontserratRegular");
                    fonts.AddFont("Montserrat-SemiBold.ttf", "MontserratSemiBold");
                    fonts.AddFont("Montserrat-SemiBoldItalic.ttf", "MontserratSemiBoldItalic");
                    fonts.AddFont("Montserrat-Thin.ttf", "MontserratThin");
                    fonts.AddFont("Montserrat-ThinItalic.ttf", "MontserratThinItalic");
                });

            builder.Services.AddSingleton(MopupService.Instance);
            builder.Services.AddScoped<IBudgetService, BudgetService>();
            builder.Services.AddSingleton<ICacheService, CacheService>();

            #region Views and ViewModels injection

            builder.Services.AddSingleton<AppShellViewModel>();

            builder.Services.AddSingleton<StartMenu>();
            builder.Services.AddSingleton<StartMenuViewModel>();

            builder.Services.AddScoped<LoginMenu>();
            builder.Services.AddScoped<LoginMenuViewModel>();

            builder.Services.AddScoped<SignUpMenu>();
            builder.Services.AddScoped<SignUpMenuViewModel>();

            builder.Services.AddScoped<HistoryPage>();
            builder.Services.AddScoped<HistoryPageViewModel>();

            builder.Services.AddScoped<IncomesMenu>();
            builder.Services.AddScoped<IncomesMenuViewModel>();

            builder.Services.AddTransient<IncomesPopup>();
            builder.Services.AddTransient<IncomesPopupViewModel>();

            builder.Services.AddScoped<ExpensesMenu>();
            builder.Services.AddScoped<ExpensesMenuViewModel>();
            builder.Services.AddTransient<BudgetOverrunsPopup>();
            builder.Services.AddTransient<BudgetOverrunsPopupViewModel>();

            builder.Services.AddScoped<GoalsMenu>();
            builder.Services.AddScoped<GoalsMenuViewModel>();
            builder.Services.AddTransient<GoalsPopup>();
            builder.Services.AddTransient<GoalsPopupViewModel>();

            builder.Services.AddScoped<BudgetsMenu>();
            builder.Services.AddScoped<BudgetsMenuViewModel>();

            builder.Services.AddTransient<BudgetAddEdit>();
            builder.Services.AddTransient<BudgetAddEditViewModel>();
            builder.Services.AddTransient<PlannedExpensesPopup>();
            builder.Services.AddTransient<PlannedExpensesPopupViewModel>();
            builder.Services.AddTransient<PlannedIncomesPopup>();
            builder.Services.AddTransient<PlannedIncomesPopupViewModel>();
            #endregion

            builder.Services.AddHttpClient("myhttpclient")
                .ConfigurePrimaryHttpMessageHandler(() => new SocketsHttpHandler
                {
                    PooledConnectionLifetime = TimeSpan.FromMinutes(2)
                })
                .SetHandlerLifetime(Timeout.InfiniteTimeSpan);

            builder.Services.AddSingleton<Client>(provider => new Client(BaseUrl, provider.GetService<HttpClient>()));
            
            return builder.Build();
        }
    }
}