using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AuctimoTraders.Helpers;

namespace AuctimoTraders.Models
{
    public class Sale : BaseModel
    {
        /// <summary>
        ///     Gets or sets the primary key for the organization.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or set the ItemTypeId which is a foreign key
        /// </summary>
        [ForeignKey(nameof(ItemType))]
        public Guid ItemTypeId { get; set; }

        /// <summary>
        ///     Gets or set the CountryId which is a foreign key
        /// </summary>
        [ForeignKey(nameof(Country))]
        public Guid CountryId { get; set; }

        /// <summary>
        ///     Gets or sets the sales channel of the sale
        /// </summary>
        public SalesChannel SalesChannel { get; set; }

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

        /// <summary>
        ///     Gets or sets the Id of the salesman who executed the sale
        /// </summary>
        [ForeignKey(nameof(AppUser))]
        public Guid SalesPersonId { get; set; }

        [JsonIgnore]
        public virtual ItemType ItemType { get; set; }

        [JsonIgnore]
        public virtual Country Country { get; set; }

        [JsonIgnore]
        public virtual AppUser AppUser { get; set; }
    }
}
