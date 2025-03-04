using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderLookup.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        #region Observable Properties
        [ObservableProperty]
        private string _filePath;
        #endregion

        #region Constructor
        public SettingsViewModel()
        {

            try
            {
                bool hasFilePath = Preferences.Default.ContainsKey("filePath");
                if (!hasFilePath)
                {
                    Preferences.Default.Set("filePath", "");
                }
            }
            catch (Exception ex)
            {
            }
            FilePath = Preferences.Default.Get("filePath", "");
        }
        #endregion


        #region Commands
        [RelayCommand]
        private void SaveSettings()
        {
            Preferences.Default.Set("filePath", FilePath);
        }
        #endregion
    }
}
