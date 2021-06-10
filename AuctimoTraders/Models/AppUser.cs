using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using AuctimoTraders.Helpers;
using AuctimoTraders.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace AuctimoTraders.Models
{
    /// <inheritdoc cref="IdentityUser" />
    public class AppUser : IdentityUser<Guid>, IBaseModel
    {
        /// <summary>
        ///     Gets or set the first name of a user
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        ///     Gets or sets the last name of the user
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        ///     Gets or Sets the Gender of the User
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        ///     Gets or Sets the date of birth of the User
        /// </summary>
        public DateTime DOB { get; set; }

        /// <summary>
        ///     Gets or sets the weight of the user in Kgs
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        ///     Gets or sets the current salary of the user
        /// </summary>
        public float Salary { get; set; }

        /// <summary>
        ///     Gets or sets the year the user joined the company
        /// </summary>
        public int JoiningYear { get; set; }

        /// <summary>
        ///     Gets or sets the month the user joined the company
        /// </summary>
        public int JoiningMonth { get; set; }

        /// <summary>
        ///     Gets or sets the day the user joined the company
        /// </summary>
        public int JoiningDay { get; set; }

        /// <inheritdoc />
        public DateTime CreatedAt { get; set; }

        /// <inheritdoc />
        public DateTime UpdatedAt { get; set; }

        /// <inheritdoc />
        public DateTime? DeletedAt { get; set; }

        [JsonIgnore]
        public virtual Country Country { get; set; }

        [JsonIgnore]
        public virtual Region Region { get; set; }

        [JsonIgnore]
        public virtual ICollection<Sale> Sales { get; set; }


        /// <summary>
        ///     Initialise the default values for
        ///     <see cref="CreatedAt"/> <see cref="UpdatedAt"/>
        ///     and <see cref="DeletedAt"/>
        /// </summary>
        public AppUser()
        {
            var date = DateTime.Now;
            CreatedAt = date.GetUtcDateTime();
            UpdatedAt = date.GetUtcDateTime();
            DeletedAt = null;
        }


    }
}
