using System;
using System.ComponentModel.DataAnnotations;

namespace AuctimoTraders.Interfaces
{
    /// <summary>
    ///     Base model inherited across all models
    /// </summary>
    public interface IBaseModel
    {
        /// <summary>
        ///     Time the recorded was created in the database
        /// </summary>
        [Required]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        ///     Time the record was updated in the database
        /// </summary>
        [Required]
        public DateTime UpdatedAt { get; set; }

        /// <summary>
        ///     To help in soft deletion, indicates time a record was deleted in the database
        /// </summary>
        [Required]
        public DateTime? DeletedAt { get; set; }
    }
}
