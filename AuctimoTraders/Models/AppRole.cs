using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctimoTraders.Helpers;
using AuctimoTraders.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AuctimoTraders.Models
{
    /// <inheritdoc cref="IdentityRole{TKey}" />
    public class AppRole : IdentityRole<Guid>, IBaseModel
    {
        /// <inheritdoc />
        public DateTime CreatedAt { get; set; }

        /// <inheritdoc />
        public DateTime UpdatedAt { get; set; }

        /// <inheritdoc />
        public DateTime? DeletedAt { get; set; }


        /// <summary>
        ///     Initialise the default values for
        ///     <see cref="CreatedAt"/> <see cref="UpdatedAt"/>
        ///     and <see cref="DeletedAt"/>
        /// </summary>
        public AppRole()
        {
            var date = DateTime.Now;
            CreatedAt = date.GetUtcDateTime();
            UpdatedAt = date.GetUtcDateTime();
            DeletedAt = null;
        }
    }
}
