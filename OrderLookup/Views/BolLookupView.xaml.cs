using OrderLookup.ViewModels;

namespace OrderLookup.Views;

public partial class BolLookupView : ContentPage
{
    private readonly BolLookupViewModel _bolLookupViewModel;

    public BolLookupView(BolLookupViewModel bolLookupViewModel)
	{
		InitializeComponent();
        _bolLookupViewModel = bolLookupViewModel;

        BindingContext = _bolLookupViewModel;
    }
}