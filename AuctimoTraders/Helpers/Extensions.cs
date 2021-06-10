using System;
using System.Collections.Generic;
using System.Linq;
using AuctimoTraders.Models;
using AuctimoTraders.Shared.Models;
using Microsoft.AspNetCore.Identity;

namespace AuctimoTraders.Helpers
{
    /// <summary>
    ///     Class that holds extension methods for the Application
    /// </summary>
    public static class Extensions
    {

        /// <summary>
        ///     Convert the provided date to UTC <see cref="DateTime"/>
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DateTime GetUtcDateTime(this DateTime date) =>
            new DateTimeOffset(date.Year, date.Month, date.Day, date.Hour, date.Minute,
                date.Second, TimeZoneInfo.Local.BaseUtcOffset).UtcDateTime;

        /// <summary>
        ///     Convert a list of <see cref="IdentityError"/> to a string of the error code and error description
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string[] ToErrorStrings(this IEnumerable<IdentityError> source) => source
            .Select(identityError => $"{identityError.Code}: {identityError.Description}").ToArray();

        /// <summary>
        ///     Create a <see cref="UserDTO"/> object from an <see cref="AppUser"/> object
        /// </summary>
        /// <param name="appUser"></param>
        /// <returns></returns>
        public static UserDTO ToUserDTO(this AppUser appUser) => new UserDTO(appUser.Id, appUser.Email,
            appUser.FirstName, appUser.LastName, appUser.PhoneNumber,
            appUser.Gender,
            appUser.DOB, appUser.Weight, appUser.Salary, appUser.JoiningDay, appUser.JoiningMonth,
            appUser.JoiningYear, appUser.CreatedAt, appUser.DeletedAt, appUser.UpdatedAt, appUser.JoiningQuarter, 
            appUser.Serial, appUser.JoiningMonthName);
    }
}
