using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AuctimoTraders.Helpers;

namespace AuctimoTraders.Models
{
    public class Region : BaseModel
    {
        /// <summary>
        ///     Gets or sets the primary key for the organization.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        ///     Gets or sets the name of the region
        /// </summary>
        public string RegionName { get; set; }

        /// <summary>
        ///     Gets or sets the Id of the Regional Manager which is a foreign key to the <see cref="AppUser"/> table
        /// </summary>
        [ForeignKey(nameof(AppUser))]
        public Guid RegionManagerId { get; set; }

        [JsonIgnore]
        public virtual AppUser AppUser { get; set; }

        [JsonIgnore]
        public virtual ICollection<Country> Countries { get; set; }
    }
}
