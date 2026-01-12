using CommunityToolkit.Maui;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderLookup.ViewModels
{
    public partial class SqlScriptPopupViewModel : ObservableObject, IQueryAttributable
    {
        private readonly IPopupService _popupService;
        [ObservableProperty] private string? _sqlScript;
        public SqlScriptPopupViewModel(IPopupService popupService) {
            _popupService = popupService;
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            SqlScript = (string)query[nameof(SqlScriptPopupViewModel.SqlScript)];
        }
        [RelayCommand]
        async Task OkButtonClicked()
        {
            await _popupService.ClosePopupAsync(Shell.Current);
        }
    }
}
