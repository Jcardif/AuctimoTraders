using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AuctimoTraders.Helpers;

namespace AuctimoTraders.Models
{
    public class ItemType : BaseModel
    {
        /// <summary>
        ///     Gets or sets the primary key for the organization.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the name of the Item Type
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the Unit price of the Item Type
        /// </summary>
        public float UnitPrice { get; set; }

        [JsonIgnore]
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
