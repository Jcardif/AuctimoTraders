
using System;
using AuctimoTraders.Shared.Helpers;

namespace AuctimoTraders.Shared.Models
{
    public class SeedSale 
    {
        public string Country { get; set; }
        public string Region { get; set; }
        public string ItemType { get; set; }
        public string SalesChannel { get; set; }
        public string OrderPriority { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderId { get; set; }
        public DateTime ShipDate { get; set; }
        public int UnitsSold { get; set; }
        public float UnitPrice { get; set; }
        public float UnitCost { get; set; }
        public int Serial { get; set; }
    }
}
