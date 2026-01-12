using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderLookup.Data.Sql.DbConstants
{
    public static class OrderLookupConstants
    {
        public static string GetOrderInfo(string orderIds) { 

            return $"SELECT BolId, OrderId, FacilityId_Source, FacilityId_Pickup, FacilityId_Delivery, Processing_Status, STATUS_DATE" +
                $" FROM [epcdocmandb_igps].[dbo].[OrderRequestNew_Header] WHERE BolId IN ({orderIds})";
    }
    }
}
