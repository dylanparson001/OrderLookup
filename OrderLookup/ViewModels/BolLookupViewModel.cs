using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OrderLookup.Helpers;
using OrderLookup.Sql.Helpers;
using OrderLookup.Sql.Interfaces;
using OrderLookup.Sql.Models;
using System.Collections.ObjectModel;

namespace OrderLookup.ViewModels
{
    public partial class BolLookupViewModel : ObservableObject
    {
        #region Repos
        private readonly IOrderLookupRepo _orderLookupRepo;
        #endregion

        #region Observable Properties

        [ObservableProperty]
        private string? _bolText;


        [ObservableProperty]
        private ObservableCollection<Order>? _bolResult = new ObservableCollection<Order>();

        [ObservableProperty]
        private string? _connectedText;
        #endregion

        #region Constructor
        public BolLookupViewModel(IOrderLookupRepo orderLookupRepo)
        {
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
        private async Task CheckConnection()
        {
            await AttemptConnection();
        }

        #endregion

        #region Private Methods
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
