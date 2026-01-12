using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OrderLookup.Csv;
using OrderLookup.Data.Sql.DbConstants;
using OrderLookup.Data.Sql.Helpers;
using OrderLookup.Data.Sql.Interfaces;
using OrderLookup.Data.Sql.Models;
using OrderLookup.Helpers;
using OrderLookup.Views;
using System.Collections.ObjectModel;

namespace OrderLookup.ViewModels
{
    public partial class BolLookupViewModel : ObservableObject
    {
        #region Private Fields
        private readonly IOrderLookupRepo _orderLookupRepo;
        private readonly IPopupService _popupService;
        #endregion

        #region Observable Properties

        [ObservableProperty]
        private string? _bolText;

        private string? _sqlScript = string.Empty;


        [ObservableProperty]
        private ObservableCollection<Order>? _bolResult = new ObservableCollection<Order>();

        [ObservableProperty]
        private string? _connectedText;

        [ObservableProperty]
        private bool _isFileSaved = false;

        [ObservableProperty]
        private string _filePath = "";
        #endregion

        #region Constructor
        public BolLookupViewModel(IPopupService popupService, IOrderLookupRepo orderLookupRepo)
        {
            _popupService = popupService;
            _orderLookupRepo = orderLookupRepo;

            Task.Run(async () =>
            {
                await CheckConnection();
            });
        }
        #endregion

        #region Commands
        [RelayCommand]
        private async Task LookupOrders()
        {
            await RunOrderLookup();
        }
        [RelayCommand]
        private async Task ShowSqlScript()
        {
            await GetSqlScript();
        }
        [RelayCommand]
        private async Task CheckConnection()
        {
            await AttemptConnection();
        }

        [RelayCommand]
        private async Task SaveToCsv()
        {
            await SaveOrdersToCsv();
            IsFileSaved = true;

            await Task.Delay(5000);
            IsFileSaved = false;
        }

        #endregion

        #region Private Methods

        private async Task GetSqlScript()
        {
            try
            {
                //var vm = new SqlScriptPopupViewModel
                //{
                //    SqlScript = this.SqlScript ?? "(no SQL)"
                //};

                //Shell.Current.ShowPopup(new SqlScriptPopup(vm));
                var queryAttributes = new Dictionary<string, object>
                {
                    [nameof(SqlScriptPopupViewModel.SqlScript)] = _sqlScript
                };
                _popupService.ShowPopup<SqlScriptPopup>(Shell.Current, options: PopupOptions.Empty, shellParameters: queryAttributes);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"GetSqlScript failed: {ex}");
                throw;
            }
        }
        private async Task<List<Order>> GetOrders()
        {
            char[] delimiters = { '\n' };
            if (string.IsNullOrEmpty(BolText))
            {
                throw new Exception("No BOLs entered");
            }


            BolResult!.Clear();
            var listBols = BaseHelpers.ParseTextToList(BolText);
            var concatenatedList = BaseHelpers.ConcatStringFromList(listBols);

            _sqlScript = OrderLookupConstants.GetOrderInfo(concatenatedList);
            
            var result = await _orderLookupRepo.GetOrders(concatenatedList);
            
            result.ForEach(x =>
            {
                if (string.IsNullOrEmpty(x.Processing_Status))
                {
                    x.Processing_Status = "NULL";
                }
            });
            return result;
        }

        private async Task AttemptConnection()
        {
            bool connectionResult = false;
            try
            {

                connectionResult = await SqlHelpers.CheckDbConnection();
            }
            catch (Exception ex)
            {
            }
            if (connectionResult)
            {
                ConnectedText = "Connected!";
            }
            else
            {
                ConnectedText = "Disconnected";
            }
        }

        private async Task SaveOrdersToCsv()
        {
            if (BolResult == null)
            {
                return;
            }

            var csvManager = new CsvManager();
            FilePath = csvManager.GetCurrentFolderPath();
            await csvManager.SaveCsvFile(BolResult.ToList());

        }

        #endregion

        #region Public Methods 
        public async Task RunOrderLookup()
        {
            try
            {
                var bolResult = await GetOrders();
                bolResult.ForEach(order => BolResult!.Add(order));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        #endregion

    }
}
