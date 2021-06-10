using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuctimoTraders.Data;
using AuctimoTraders.Helpers;
using AuctimoTraders.Interfaces;
using AuctimoTraders.Models;
using AuctimoTraders.Services;
using AuctimoTraders.Shared.Helpers;
using AuctimoTraders.Shared.Models;

namespace AuctimoTraders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAppUserService _userService;

        public UsersController(AppDbContext context, IAppUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            return await _context.Users.Select(u => u.ToUserDTO()).ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetAppUser(Guid id)
        {
            var appUser = await _context.Users.FindAsync(id);

            if (appUser == null)
            {
                return NotFound();
            }

            return appUser.ToUserDTO();
        }


        // POST: api/Users
        [HttpPost("{role}")]
        public async Task<ActionResult<UserDTO>> PostAppUser(UserDTO user, UserRole role)
        {
            if (string.IsNullOrEmpty(user.ManagerialRoleAssignment) && role is UserRole.CountryManager)
            {
                return BadRequest(new APIResponseMessage("Provide the Managerial region assigned", null,
                    HttpStatusCode.BadRequest));
            }

            if (string.IsNullOrEmpty(user.ManagerialRoleAssignment) && role is UserRole.RegionalManager)
            {
                return BadRequest(new APIResponseMessage("Provide the Managerial Country assigned", null,
                    HttpStatusCode.BadRequest));
            }

            var res = await _userService.RegisterUserAsync(user, role);
            if(res.StatusCode!=HttpStatusCode.Created)
                return StatusCode((int)res.StatusCode, res);
            var userDTO = (UserDTO) res.Result;

            switch (role)
            {
                case UserRole.SalesPerson:
                    return CreatedAtAction("GetAppUser", new { id = userDTO.Id }, userDTO);

                case UserRole.CountryManager:
                    var country = new Country
                    {
                        CountryName = user.ManagerialRoleAssignment,
                        CountryManagerId = userDTO.Id
                    };
                    await _context.Countries.AddAsync(country);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("GetAppUser", new { id = userDTO.Id }, userDTO);


                case UserRole.RegionalManager:
                    var region = new Region()
                    {
                        RegionManagerId = userDTO.Id,
                        RegionName = user.ManagerialRoleAssignment
                    };
                    await _context.Regions.AddAsync(region);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("GetAppUser", new { id = userDTO.Id }, userDTO);

                default:
                    throw new ArgumentOutOfRangeException(nameof(role), role, null);
            }
        }


        private bool AppUserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
