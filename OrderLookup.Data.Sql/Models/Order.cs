using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderLookup.Data.Sql.Models
{
    public class Order
    {
        public string? BolId { get; set; }
        public string? OrderId { get; set; }
        public string? SourceId { get; set; }
        public string? PickupId { get; set; }
        public string? DestinationId { get; set; }
        public string? Processing_Status { get; set; }
        public DateTime? Status_Date { get; set; }
    }
}
