using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealtyFirmAPI.Data;
using RealtyFirmAPI.Helpers;
using RealtyFirmAPI.Models;
using RealtyFirmAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyFirmAPI.Controllers
{
    [BrokerAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BrokersController : ControllerBase
    {
        private readonly RealtyDbContext _context;

        public BrokersController(RealtyDbContext context)
        {
            _context = context;
        }

        // GET: api/Brokers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Broker>>> GetBrokers()
        {
            var brokers = await _context.Brokers.Include(broker => broker.Listings)
                .ThenInclude(listing => listing.Images).ToListAsync();
            return brokers;
        }

        // GET: api/Brokers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Broker>> GetBroker(Guid id)
        {
            var broker = await _context.Brokers.FindAsync(id);

            if (broker == null)
            {
                return NotFound();
            }
            broker.Listings = await _context.Listings.Where(listing => listing.Broker_Id == broker.Broker_Id)
                .Include(listing => listing.Images).ToListAsync();


            return broker;
        }

        // PUT: api/Brokers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBroker(Guid id, Broker broker)
        {
            if (id != broker.Broker_Id)
            {
                return BadRequest();
            }

            _context.Entry(broker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BrokerExists(id))
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

        // POST: api/Brokers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<Broker>> PostBroker(Broker broker)
        {
            broker.Broker_Id = Guid.NewGuid();
            broker.Password = Security.Encrypt(AppSettings.appSettings.JwtSecret, broker.Password);


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.SelectMany(x => x.Value.Errors));
            }

            _context.Brokers.Add(broker);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BrokerExists(broker.Broker_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBroker", new { id = broker.Broker_Id }, broker);
        }

        // DELETE: api/Brokers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBroker(Guid id)
        {
            var broker = await _context.Brokers.FindAsync(id);
            if (broker == null)
            {
                return NotFound();
            }

            _context.Brokers.Remove(broker);
            await _context.SaveChangesAsync();

            return NoContent();
        }



        private bool BrokerExists(Guid id)
        {
            return _context.Brokers.Any(e => e.Broker_Id == id);
        }
    }
}
