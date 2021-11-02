using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetAppUser(Guid id)
        {
            var appUser = await _context.Users.FindAsync(id);

            if (appUser == null)
            {
                return NotFound();
            }

            return appUser;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAppUser(Guid id, AppUser appUser)
        {
            if (id != appUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(appUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AppUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AppUser>> PostAppUser(AppUser appUser)
        {
            _context.Users.Add(appUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAppUser", new { id = appUser.Id }, appUser);
        }

        // POST: api/Users
        [HttpPost("{role}")]
        public async Task<ActionResult<UserDTO>> PostAppUser(UserDTO user, UserRole role)
        {
            if (string.IsNullOrEmpty(user.ManagerialRoleAssignment) && role is UserRole.CountryManager)
            {
                return BadRequest(new ApiResponseMessage("Provide the Managerial region assigned", null,
                    HttpStatusCode.BadRequest));
            }

            if (string.IsNullOrEmpty(user.ManagerialRoleAssignment) && role is UserRole.RegionalManager)
            {
                return BadRequest(new ApiResponseMessage("Provide the Managerial Country assigned", null,
                    HttpStatusCode.BadRequest));
            }

            var res = await _userService.RegisterUserAsync(user, role);
            if (res.StatusCode != HttpStatusCode.Created)
                return StatusCode((int)res.StatusCode, res);
            var userDTO = (UserDTO)res.Result;

            switch (role)
            {
                case UserRole.SalesPerson:
                    return CreatedAtAction("GetAppUser", new { id = userDTO.Id }, userDTO);

                case UserRole.CountryManager:
                    Debug.Assert(userDTO.Id != null, "userDTO.Id != null");
                    var country = new Country
                    {
                        CountryName = user.ManagerialRoleAssignment,
                        CountryManagerId = userDTO.Id.Value
                    };
                    await _context.Countries.AddAsync(country);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("GetAppUser", new { id = userDTO.Id }, userDTO);


                case UserRole.RegionalManager:
                    Debug.Assert(userDTO.Id != null, "userDTO.Id != null");
                    var region = new Region()
                    {
                        RegionManagerId = userDTO.Id.Value,
                        RegionName = user.ManagerialRoleAssignment
                    };
                    await _context.Regions.AddAsync(region);
                    await _context.SaveChangesAsync();
                    return CreatedAtAction("GetAppUser", new { id = userDTO.Id }, userDTO);

                default:
                    throw new ArgumentOutOfRangeException(nameof(role), role, null);
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppUser(Guid id)
        {
            var appUser = await _context.Users.FindAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }

            _context.Users.Remove(appUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AppUserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
