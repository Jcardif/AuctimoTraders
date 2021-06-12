using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using AuctimoTraders.Shared.Helpers;

namespace AuctimoTraders.Shared.Models
{
    public class SaleDTO
    {
        public string Id { get; set; }
        
        /// <summary>
        ///     Gets or set the ItemTypeId which is a foreign key
        /// </summary>
        public Guid ItemTypeId { get; set; }

        /// <summary>
        ///     Gets or set the CountryId which is a foreign key
        /// </summary>
        public Guid? CountryId { get; set; }

        /// <summary>
        ///     Gets or sets the sales channel of the sale
        /// </summary>
        public string SalesChannel { get; set; }

        /// <summary>
        ///     Gets or sets the date when the order was placed
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        ///     Unique Identifier of the Order
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        ///     Gets or sets the date when the order is shipped
        /// </summary>
        public DateTime ShipDate { get; set; }

        /// <summary>
        ///     Gets or Sets the number of units sold in this particular sale
        /// </summary>
        public int UnitsSold { get; set; }

        /// <summary>
        ///     Gets or Sets the cost at which each unit was sold
        /// </summary>
        public float UnitCost { get; set; }

        public string OrderPriority { get; set; }

        /// <summary>
        ///     Gets or sets the Id of the salesman who executed the sale
        /// </summary>
        public Guid SalesPersonId { get; set; }
    }
}
