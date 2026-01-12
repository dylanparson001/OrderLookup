using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OrderLookup.Views;

namespace OrderLookup.ViewModels
{
    public partial class AppShellViewModel : ObservableObject
    {
        #region Obseravble Properties
        [ObservableProperty]
        private string _appTitle;
        #endregion

        #region Relay Commands
        [RelayCommand]
        private async Task ShowSettings()
        {
            await Shell.Current.GoToAsync($"SettingsView");

        }
        #endregion

        #region Private Variables
        private string _appVersion;
        #endregion
        public AppShellViewModel()
        {
            _appVersion = "1.0";

            _appTitle = $"Order Lookup v{_appVersion}";
        }
    }
}
