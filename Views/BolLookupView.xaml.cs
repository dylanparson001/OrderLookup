using OrderLookup.ViewModels;

namespace OrderLookup.Views;

public partial class BolLookupView : ContentPage
{
	public BolLookupView(BolLookupViewModel viewModel)
	{
		InitializeComponent();

		BindingContext = viewModel;
	}
}