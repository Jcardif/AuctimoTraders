using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctimoTraders.Helpers;
using AuctimoTraders.Shared.Helpers;
using AuctimoTraders.Shared.Models;

namespace AuctimoTraders.Interfaces
{
    /// <summary>
    ///     Interface contains tasks to handle different AppUser operations
    /// </summary>
    public interface IAppUserService
    {
        /// <summary>
        ///     Register a new user to the system
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<ApiResponseMessage> RegisterUserAsync(UserDTO user, UserRole role);
    }
}
