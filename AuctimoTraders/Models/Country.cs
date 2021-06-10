using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AuctimoTraders.Helpers;

namespace AuctimoTraders.Models
{
    public class Country : BaseModel
    {
        /// <summary>
        ///     Gets or sets the primary key for the organization.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the name of the country
        /// </summary>
        public string CountryName { get; set; }


        /// <summary>
        ///     Gets or sets the Id of the Country Manager which is a foreign key to the <see cref="AppUser"/> table
        /// </summary>
        [ForeignKey(nameof(AppUser))]
        public Guid CountryManagerId { get; set; }

        [ForeignKey(nameof(Region))]
        public Guid RegionId { get; set; }

        [JsonIgnore]
        public virtual AppUser AppUser { get; set; }

        [JsonIgnore]
        public virtual Region Region { get; set; }

        [JsonIgnore]
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
