using System;
using AuctimoTraders.Interfaces;

namespace AuctimoTraders.Helpers
{
    /// <inheritdoc />
    public class BaseModel : IBaseModel
    {

        /// <summary>
        ///     Initialise the default values for
        ///     <see cref="CreatedAt"/> <see cref="UpdatedAt"/>
        ///     and <see cref="DeletedAt"/>
        /// </summary>
        public BaseModel()
        {
            var date = DateTime.Now;
            CreatedAt = date.GetUtcDateTime();
            UpdatedAt = date.GetUtcDateTime();
            DeletedAt = null;
        }


        /// <inheritdoc />
        public DateTime CreatedAt { get; set; }

        /// <inheritdoc />
        public DateTime UpdatedAt { get; set; }

        /// <inheritdoc />
        public DateTime? DeletedAt { get; set; }
    }
}
