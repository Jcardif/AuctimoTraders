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
using AuctimoTraders.Models;
using AuctimoTraders.Shared.Models;

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

        // GET: api/Sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSales()
        {
            return await _context.Sales.ToListAsync();
        }

        // GET: api/Sales/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sale>> GetSale(Guid id)
        {
            var sale = await _context.Sales.FindAsync(id);

            if (sale == null)
            {
                return NotFound();
            }

            return sale;
        }

        // PUT: api/Sales/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSale(Guid id, Sale sale)
        {
            if (id != sale.Id)
            {
                return BadRequest();
            }

            _context.Entry(sale).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SaleExists(id))
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

        // POST: api/Sales
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sale>> PostSale(Sale sale)
        {
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSale", new { id = sale.Id }, sale);
        }

        [HttpPost("seed")]
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

        // DELETE: api/Sales/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSale(Guid id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null)
            {
                return NotFound();
            }

            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SaleExists(Guid id)
        {
            return _context.Sales.Any(e => e.Id == id);
        }
    }
}
