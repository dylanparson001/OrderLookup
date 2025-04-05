using Microsoft.Extensions.Logging;
using OrderLookup.Sql.Models;
using OrderLookup.Views;
using System.IO.Compression;
using System.Text;

namespace OrderLookup.Csv
{
    internal class CsvManager
    {
        #region Private Properties

        private static List<string> sourceFilePaths = new List<string>();

        #endregion

        public CsvManager()
        {

        }
        public string GetCurrentFolderPath()
        {
            var filePath = Preferences.Default.Get("filePath", "");
            if (string.IsNullOrEmpty(filePath))
            {
                throw new Exception("File path is empty please set in settings");
            }
            return filePath;
        }

        public async Task SaveCsvFile(List<Order> bolList)
        {

            string path = GetCurrentFolderPath();

            // Generate csv
            var csv = new StringBuilder();

            DateTime currentDate = DateTime.Now;

            string currentDateString = currentDate.ToString("MM-dd-yyyy");

            string csvFileName = $"Order Lookup {bolList.First().BolId} {currentDateString}";

            string fullPath = Path.Combine(path, csvFileName);

            if (File.Exists($"{fullPath}.csv"))
            {
                Random rnd = new Random();
                string randomCsvFileName = $"{csvFileName} ({rnd.Next(1, 1000)})";
                fullPath = Path.Combine(path, randomCsvFileName);

            }
            sourceFilePaths.Add(fullPath);

            csv.AppendLine("BOL ID, ORDER ID, SOURCE ID, PICKUP ID, DESTINATION ID, PROCESSING STATUS, STATUS DATE");
            foreach (Order bol in bolList)
            {
                var first = bol.BolId;
                var second = bol.OrderId;
                var third = bol.SourceId;
                var fourth = bol.PickupId;
                var fifth = bol.DestinationId;
                var sixth = bol.Processing_Status;
                var seventh = bol.Status_Date;

                var newLine = string.Format($"{first},{second},{third},{fourth},{fifth},{sixth},{seventh}");
                csv.AppendLine(newLine);
            }

            try
            {

                // Using statement for StreamWriter to ensure proper disposal
                using (var streamWriter = new StreamWriter($"{fullPath}.csv"))
                {
                    streamWriter.Write(csv.ToString());
                    streamWriter.Flush();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }


            csv.Clear();
        }

    }
}