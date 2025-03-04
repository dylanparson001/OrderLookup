using OrderLookup.ViewModels;

namespace OrderLookup.Views;

public partial class SettingsView : ContentPage
{
	public SettingsView(SettingsViewModel settingsViewModel)
	{
		InitializeComponent();
		BindingContext = settingsViewModel;
    }
}