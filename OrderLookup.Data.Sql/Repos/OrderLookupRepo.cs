using Microsoft.Data.SqlClient;
using OrderLookup.Data.Sql.DbConstants;
using OrderLookup.Data.Sql.Interfaces;
using OrderLookup.Data.Sql.Managers;
using OrderLookup.Data.Sql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderLookup.Data.Sql.Repos
{
    public class OrderLookupRepo : BaseRepo, IOrderLookupRepo
    {
        private SqlSettingsManager _sqlSettingsManager;

        public OrderLookupRepo(SqlSettingsManager sqlSettingsManager) : base(sqlSettingsManager)
        {
            _sqlSettingsManager = sqlSettingsManager;
        }

        public async Task<List<Order>> GetOrders(string concatenatedList)
        {
            var listOfOrders = new List<Order>();

            string query = OrderLookupConstants.GetOrderInfo(concatenatedList);
            if (string.IsNullOrEmpty(ConnectionString))
            {
                throw new Exception("Connection to server could not be made");
            }

            var connection = new SqlConnection(ConnectionString);

            using (var conn = connection)
            {
                try
                {
                    await conn.OpenAsync();

                    SqlCommand command = new SqlCommand(query, conn);

                    var reader = await command.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string bolId = reader["BolId"].ToString();
                            string orderId = reader["OrderId"].ToString();
                            string sourceId = reader["FacilityId_Source"].ToString();
                            string pickupId = reader["FacilityId_Pickup"].ToString();
                            string deliveryId = reader["FacilityId_Delivery"].ToString();
                            string processingStatus = reader["Processing_Status"].ToString();
                            DateTime statusDate = (DateTime)reader["STATUS_DATE"];

                            var newOrder = new Order()
                            {
                                BolId = bolId,
                                OrderId = orderId.Trim(),
                                SourceId = sourceId,
                                PickupId = pickupId,
                                DestinationId = deliveryId,
                                Processing_Status = processingStatus,
                                Status_Date = statusDate
                            };

                            listOfOrders.Add(newOrder);

                        }
                    }


                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    await conn.CloseAsync();
                }

                return listOfOrders;
            }

        }
    }
}
