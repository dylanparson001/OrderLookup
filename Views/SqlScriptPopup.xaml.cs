using System.Collections.Generic;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using OrderLookup.ViewModels;

namespace OrderLookup.Views;

public partial class SqlScriptPopup : Popup, IQueryAttributable
{
    public SqlScriptPopup(SqlScriptPopupViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }

    // Forward query attributes to the ViewModel (the popup service delivers parameters to the Popup)
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (BindingContext is IQueryAttributable attributable)
        {
            attributable.ApplyQueryAttributes(query);
        }
    }
}