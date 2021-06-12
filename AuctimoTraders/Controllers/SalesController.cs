using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AuctimoTraders.Data;
using AuctimoTraders.Helpers;
using AuctimoTraders.Models;
using AuctimoTraders.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace AuctimoTraders.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SalesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult> SeedSalesAsync([FromBody] SeedSale seedSale)
        {
            var sale = await _context.Sales.FirstOrDefaultAsync(s => s.OrderId == seedSale.OrderId);
            if (sale != null)
                return Conflict(new ApiResponseMessage("Another sale is registered with the provided orderId", null,
                    HttpStatusCode.Conflict));

            var region = await _context.Regions.FirstOrDefaultAsync(r => r.RegionName == seedSale.Region);
            if (region is null)
                return NotFound(new ApiResponseMessage("No region was found with the provided name", null,
                    HttpStatusCode.NotFound));

            var country = await _context.Countries.FirstOrDefaultAsync(c => c.CountryName == seedSale.Country);
            if (country is null)
                return NotFound(new ApiResponseMessage("No country was found with the provided name", null,
                    HttpStatusCode.NotFound));

            if (country.RegionId is null)
            {
                country.RegionId = region.Id;
                _context.Entry(country).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            var itemType = await _context.ItemTypes.FirstOrDefaultAsync(i => i.Name == seedSale.ItemType);
            if (itemType is null)
            {
                itemType = new ItemType
                {
                    Name = seedSale.ItemType,
                    UnitPrice = seedSale.UnitPrice
                };
                await _context.ItemTypes.AddAsync(itemType);
                await _context.SaveChangesAsync();
            }

            var salesPerson = await _context.Users.FirstOrDefaultAsync(u => u.Serial == seedSale.Serial);
            sale = new Sale
            {
                SalesPersonId = salesPerson.Id,
                ItemTypeId = itemType.Id,
                CountryId = country.Id,
                SalesChannel = seedSale.SalesChannel,
                OrderDate = seedSale.OrderDate,
                OrderId = seedSale.OrderId,
                ShipDate = seedSale.ShipDate,
                UnitsSold = seedSale.UnitsSold,
                UnitCost = seedSale.UnitCost,
                OrderPriority = seedSale.OrderPriority
            };

            await _context.Sales.AddAsync(sale);
            await _context.SaveChangesAsync();

            return StatusCode(201, sale);
        }
    }
}
