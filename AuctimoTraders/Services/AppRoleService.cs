using System;
using System.Threading.Tasks;
using AuctimoTraders.Helpers;
using AuctimoTraders.Interfaces;
using AuctimoTraders.Models;
using Microsoft.AspNetCore.Identity;

namespace AuctimoTraders.Services
{
    /// <summary>
    ///     An implementation of the IAppRoleService
    /// </summary>
    public class AppRoleService : IAppRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="roleManager"></param>
        public AppRoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        /// <inheritdoc />
        public async Task CreateRolesAsync()
        {
            foreach (var roleName in Enum.GetNames(typeof(UserRole)))
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                    await _roleManager.CreateAsync(new AppRole { Name = roleName });
            }
        }
    }
}