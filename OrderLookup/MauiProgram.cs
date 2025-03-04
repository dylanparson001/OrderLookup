using Microsoft.Extensions.Logging;
using OrderLookup.Sql.Factories;
using OrderLookup.Sql.Interfaces;
using OrderLookup.Sql.Managers;
using OrderLookup.Sql.Repos;
using OrderLookup.ViewModels;
using OrderLookup.Views;
namespace OrderLookup
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Repos
            builder.Services.AddSingleton<IOrderLookupRepo, OrderLookupRepo>();
            
            // Managers
            builder.Services.AddSingleton<SqlSettingsManager>();

            // Factories
            builder.Services.AddScoped<RepoFactory>();

            // Views
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<BolLookupView>();
            builder.Services.AddTransient<SettingsView>();

            // ViewModels
            builder.Services.AddTransient<AppShellViewModel>();
            builder.Services.AddTransient<BolLookupViewModel>();
            builder.Services.AddTransient<SettingsViewModel>();
            
            
#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
