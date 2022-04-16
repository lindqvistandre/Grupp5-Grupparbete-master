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
    [BrokerAuthorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ListingBiddersController : ControllerBase
    {
        private readonly RealtyDbContext _context;

        public ListingBiddersController(RealtyDbContext context)
        {
            _context = context;
        }

        // GET: api/ListingBidders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ListingBidder>>> GetListingBidders()
        {
            var listingBidders = await _context.ListingBidders.ToListAsync();
            foreach (ListingBidder lb in listingBidders)
            {
                lb.Listing = await _context.Listings.FindAsync(lb.Listing_Id);
                lb.Bidder = await _context.Bidders.FindAsync(lb.Bidder_Id);
            }
            return listingBidders;
        }

        // GET: api/ListingBidders/5/4
        [HttpGet("{bidder_id}/{listing_id}")]
        public async Task<ActionResult<ListingBidder>> GetListingBidder(Guid bidder_id, Guid listing_id)
        {
            var listingBidder = await _context.ListingBidders.FindAsync(bidder_id, listing_id);

            if (listingBidder == null)
            {
                return NotFound();
            }
            listingBidder.Bidder = await _context.Bidders.FindAsync(listingBidder.Bidder_Id);
            listingBidder.Listing = await _context.Listings.FindAsync(listingBidder.Listing_Id);
            return listingBidder;
        }

        // PUT: api/ListingBidders/5/4
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{bidder_id}/{listing_id}")]
        public async Task<IActionResult> PutListingBidder(Guid bidder_Id, Guid listing_Id, ListingBidder listingBidder)
        {
            if (bidder_Id != listingBidder.Bidder_Id || listing_Id != listingBidder.Listing_Id)
            {
                return BadRequest();
            }

            _context.Entry(listingBidder).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ListingBidderExistsAsync(bidder_Id, listing_Id))
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

        // POST: api/ListingBidders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ListingBidder>> PostListingBidder(ListingBidder listingBidder)
        {



            _context.ListingBidders.Add(listingBidder);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (await ListingBidderExistsAsync(listingBidder.Bidder_Id, listingBidder.Listing_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetListingBidder", new { bidder_Id = listingBidder.Bidder_Id, listing_Id = listingBidder.Listing_Id }, listingBidder);
        }

        // DELETE: api/ListingBidders/5
        [HttpDelete("{bidder_id}/{listing_id}")]
        public async Task<IActionResult> DeleteListingBidder(Guid bidder_Id, Guid listing_Id)
        {
            var listingBidder = await _context.ListingBidders.FindAsync(bidder_Id, listing_Id);
            if (listingBidder == null)
            {
                return NotFound();
            }

            _context.ListingBidders.Remove(listingBidder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private async Task<bool> ListingBidderExistsAsync(Guid bidder_Id, Guid listing_Id)
        {
            bool exists = false;
            List<ListingBidder> listingBidders;

            if ((listingBidders = await _context.ListingBidders.Where(lb => lb.Bidder_Id == bidder_Id).ToListAsync()) != null)
            {
                if (listingBidders.Any(lb => lb.Listing_Id == listing_Id))
                    exists = true;
            }


            return exists;

        }
    }
}
