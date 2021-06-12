using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AuctimoTraders.Helpers;
using AuctimoTraders.Interfaces;
using AuctimoTraders.Models;
using AuctimoTraders.Shared.Helpers;
using AuctimoTraders.Shared.Models;
using Microsoft.AspNetCore.Identity;

namespace AuctimoTraders.Services
{
    /// <inheritdoc />
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IAppRoleService _roleService;

        /// <summary>
        ///     Constructor
        /// </summary>
        public AppUserService(UserManager<AppUser> userManager, IAppRoleService roleService)
        {
            _userManager = userManager;
            _roleService = roleService;
        }
        
        /// <inheritdoc />
        public async Task<ApiResponseMessage> RegisterUserAsync(UserDTO user, UserRole role)
        {
            var appUser = await _userManager.FindByEmailAsync(user.Email);
            if (appUser != null)
                return new ApiResponseMessage("Another User is registered with the provided email address", null,
                    HttpStatusCode.Conflict);

            appUser = new AppUser
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Gender = user.Gender,
                DOB = user.DOB,
                Weight = user.Weight,
                Salary = user.Salary,
                JoiningDay = user.JoiningDay,
                JoiningMonth = user.JoiningMonth,
                JoiningMonthName = user.JoiningMonthName,
                JoiningYear = user.JoiningYear,
                UserName = user.Email,
                Serial = user.Serial
            };

            var result = await _userManager.CreateAsync(appUser);
            if (!result.Succeeded)
                return new ApiResponseMessage("Unable to create user", result.Errors.ToErrorStrings(),
                    HttpStatusCode.BadRequest);

            await _roleService.CreateRolesAsync();
            result = await _userManager.AddToRoleAsync(appUser, Enum.GetName(typeof(UserRole), role));

            if (!result.Succeeded)
                return new ApiResponseMessage("Unable to add user to specified role", result.Errors.ToErrorStrings(),
                    HttpStatusCode.BadRequest);

            return new ApiResponseMessage("Successfully created user", null, HttpStatusCode.Created, appUser.ToUserDTO());
        }
    }
}
