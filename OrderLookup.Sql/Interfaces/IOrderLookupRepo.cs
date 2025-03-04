using OrderLookup.Sql.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderLookup.Sql.Interfaces
{
    public interface IOrderLookupRepo
    {
        Task<List<Order>> GetOrders(string concatenatedList);
    }
}
