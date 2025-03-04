using Microsoft.Maui.Handlers;
using OrderLookup.Handlers;
using OrderLookup.Sql.Interfaces;
using OrderLookup.Sql.Repos;
using OrderLookup.ViewModels;
using OrderLookup.Views;

namespace OrderLookup
{
    public partial class App : Application
    {
        public App(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            MainPage = serviceProvider.GetRequiredService<AppShell>();

        }




    }
}
