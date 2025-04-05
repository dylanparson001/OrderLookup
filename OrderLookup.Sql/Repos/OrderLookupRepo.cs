using Microsoft.Data.SqlClient;
using OrderLookup.Sql.Interfaces;
using OrderLookup.Sql.Managers;
using OrderLookup.Sql.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderLookup.Sql.Repos
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

            string query = "SELECT BolId, OrderId, FacilityId_Source, FacilityId_Pickup, FacilityId_Delivery, Processing_Status, STATUS_DATE " +
                "FROM [epcdocmandb_igps].[dbo].[OrderRequestNew_Header]" +
                $"WHERE BolId IN ({concatenatedList})";

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
                                OrderId = orderId,
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
