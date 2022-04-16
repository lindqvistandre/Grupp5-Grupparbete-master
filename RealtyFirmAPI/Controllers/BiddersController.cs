using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealtyFirmAPI.Data;
using RealtyFirmAPI.Models;
using RealtyFirmAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealtyFirmAPI.Controllers
{
    //Här ska vi lägga in en auktorisering och autentisering! Gör ett eget attribut!
    [BrokerAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BiddersController : ControllerBase
    {
        private readonly RealtyDbContext _context;

        public BiddersController(RealtyDbContext context)
        {
            _context = context;
        }

        // GET: api/Bidders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bidder>>> GetBidders()
        {
            List<Bidder> bidders = await _context.Bidders //Detta är tydligen hur man förväntas göra om man vill använda sig av eager
                    .Include(bidder => bidder.ListingBidders)//loading: https://www.tektutorialshub.com/entity-framework-core/eager-loading-using-include-theninclude-in-ef-core/
                        .ThenInclude(listingBidder => listingBidder.Listing)
                            .ThenInclude(listing => listing.Images)
                    .Include(bidder => bidder.ListingBidders)
                        .ThenInclude(listingBidder => listingBidder.Listing)
                            .ThenInclude(listing => listing.Broker)
                                .ToListAsync();
            foreach (var bidder in bidders)
            {
                foreach (var listingBidder in bidder.ListingBidders)
                {

                    listingBidder.Listing.ListingBidders = null;
                }
            }
            return bidders;
        }

        // GET: api/Bidders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bidder>> GetBidder(Guid id)
        {
            var bidder = await _context.Bidders.FindAsync(id);
            if (bidder == null)
            {
                return NotFound();
            }
            bidder.ListingBidders = await _context.ListingBidders.Where(listingBidder => listingBidder.Bidder_Id == bidder.Bidder_Id)
                                        .Include(listingBidder => listingBidder.Listing)
                                            .ThenInclude(listing => listing.Images)
                                        .Include(listingBidder => listingBidder.Listing)
                                            .ThenInclude(listing => listing.Broker)
                                                .ToListAsync();
            foreach (var listingBidder in bidder.ListingBidders)
            {
                listingBidder.Bidder.ListingBidders = null;
                listingBidder.Listing.Broker.Listings = null;
            }
            return bidder;
        }

        // PUT: api/Bidders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBidder(Guid id, Bidder bidder)
        {
            if (id != bidder.Bidder_Id)
            {
                return BadRequest();
            }

            _context.Entry(bidder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BidderExists(id))
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

        // POST: api/Bidders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bidder>> PostBidder(Bidder bidder)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.SelectMany(x => x.Value.Errors));
            }

            _context.Bidders.Add(bidder);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (BidderExists(bidder.Bidder_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetBidder", new { id = bidder.Bidder_Id }, bidder);
        }

        // DELETE: api/Bidders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBidder(Guid id)
        {
            var bidder = await _context.Bidders.FindAsync(id);
            if (bidder == null)
            {
                return NotFound();
            }

            _context.Bidders.Remove(bidder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BidderExists(Guid id)
        {
            return _context.Bidders.Any(e => e.Bidder_Id == id);
        }
    }
}
