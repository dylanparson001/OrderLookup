using OrderLookup.ViewModels;
using OrderLookup.Views;

namespace OrderLookup
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel appShellViewModel, IServiceProvider serviceProvider)
        {
            InitializeComponent();

            BindingContext = appShellViewModel;

            Routing.RegisterRoute(nameof(BolLookupView), typeof(BolLookupView));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(SettingsView), typeof(SettingsView));

        }
    }
}
