﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctimoTraders.Shared.Helpers;

namespace AuctimoTraders.Shared.Models
{
    /// <summary>
    ///     Represents User Data in the database
    /// </summary>
    public class UserDTO
    {
        public int Serial { get; set; }

        public string ManagerialRoleAssignment { get; set; }

        /// <summary>
        /// Gets or sets a telephone number for the user.
        /// </summary>
        public string PhoneNumber { get; set; }


        /// <summary>
        /// Gets or sets the email address for this user.
        /// </summary>
        public string Email { get; set; }

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
        public string Gender { get; set; }

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

        /// <summary>
        ///     Gets or sets the quarter the user joined the company
        /// </summary>
        public string JoiningQuarter { get; set; }

        /// <summary>
        ///     Gets or sets the months the users joined as a string
        /// </summary>
        public string JoiningMonthName { get; set; }


        public Guid? Id { get; set; }

        public UserDTO()
        {
           
        }

        public UserDTO(Guid id, string email, string firstName, string lastName, string phoneNumber, string gender,
            DateTime dob, double weight, float salary, int joiningDay, int joiningMonth, int joiningYear,
            DateTime createdAt, DateTime? deletedAt, DateTime updatedAt, string joiningQuarter, int serial, 
            string joiningMonthName)
        {
            Id = id;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Gender = gender;
            DOB = dob;
            Weight = weight;
            Salary = salary;
            JoiningDay = joiningDay;
            JoiningMonth = joiningMonth;
            JoiningYear = joiningYear;
            CreatedAt = createdAt;
            DeletedAt = deletedAt;
            UpdatedAt = updatedAt;
            JoiningQuarter = joiningQuarter;
            Serial = serial;
            JoiningMonthName = joiningMonthName;
        }

        public DateTime UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
