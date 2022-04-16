using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealtyFirmAPI.Data;
using RealtyFirmAPI.Models;
using RealtyFirmAPI.Models.DTO;
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
    public class ListingsController : ControllerBase
    {
        private readonly RealtyDbContext _context;

        public ListingsController(RealtyDbContext context)
        {
            _context = context;
        }

        // GET: api/Listings
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Listing>>> GetListings()
        {
            List<Listing> listings = await _context.Listings.Include(listing => listing.Images)
                                            .Include(listing => listing.Broker)
                                            .Include(listing => listing.ListingBidders)
                                                .ThenInclude(listingBidder => listingBidder.Bidder).ToListAsync();
            foreach (var listing in listings)
            {
                foreach (var listingBidder in listing.ListingBidders)
                {
                    listingBidder.Bidder.ListingBidders = null;
                }
                listing.Broker.Listings = null;
            }

            return listings;
        }

        // GET: api/Listings/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Listing>> GetListing(Guid id)
        {
            var listing = await _context.Listings.FindAsync(id);

            if (listing == null)
            {
                return NotFound();
            }
            listing.ListingBidders = await _context.ListingBidders.Where(lb => lb.Listing_Id == id).Include(listingBidder => listingBidder.Bidder).ToListAsync();
            listing.Broker = await _context.Brokers.FirstOrDefaultAsync(b => b.Broker_Id == listing.Broker_Id);
            listing.Images = await _context.Images.Where(i => i.Listing_Id == listing.Listing_Id).ToListAsync();

            return listing;
        }

        // PUT: api/Listings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutListing(Guid id, Listing listing)
        {
            if (id != listing.Listing_Id)
            {
                return BadRequest();
            }

            _context.Entry(listing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListingExists(id))
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

        // POST: api/Listings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Listing>> PostListing(Listing listing)
        {
            listing.Listing_Id = Guid.NewGuid();
            listing.Broker_Id = Guid.Parse(HttpContext.Items["extractId"].ToString());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.SelectMany(x => x.Value.Errors));
            }

            //Här skulle vi behöva se hur vi kan hantera bilder vid skapandet av en Listing.

            _context.Listings.Add(listing);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetListing", new { id = listing.Listing_Id }, listing);
        }

        // DELETE: api/Listings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteListing(Guid id)
        {
            var listing = await _context.Listings.FindAsync(id);
            if (listing == null)
            {
                return NotFound();
            }

            _context.Listings.Remove(listing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("forlulz")]
        public async Task<IActionResult> forlulz(string s = "fail")
        {
            s = HttpContext.Items["extractId"].ToString();
            HttpContext.Items["extractId"] = ""; //Kanske för att göra det säkert? Ingen aning...
            return Ok(s);
        }

        [AllowAnonymous]
        [HttpPost("subscribe")]
        public async Task<IActionResult> SubscribeToListing(SubscriptionDTO subDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState.SelectMany(x => x.Value.Errors));
            }
            bool newBidder = false;
            Bidder bidder;
            if ((bidder = await _context.Bidders.FirstOrDefaultAsync(b => b.Email == subDto.Email)) == null)
            {
                bidder = new Bidder { Bidder_Id = Guid.NewGuid(), Email = subDto.Email, First_Name = subDto.FirstName, Last_Name = subDto.LastName, Phone_Number = subDto.PhoneNumber };
                newBidder = true;
            }

            ListingBidder listingBidder = new ListingBidder { Bid = 0, Bidder_Id = bidder.Bidder_Id, Listing_Id = Guid.Parse(subDto.Listing_Id) };
            try
            {
                if (newBidder)
                {
                    _context.Bidders.Add(bidder);

                }

                _context.ListingBidders.Add(listingBidder);

                await _context.SaveChangesAsync();
            }
            catch (Exception epicFail)
            {
                return BadRequest(epicFail.Message);
            }
            return CreatedAtAction("GetListing", new { id = listingBidder.Listing_Id }, await _context.Listings.FindAsync(listingBidder.Listing_Id));
        }
        private bool ListingExists(Guid id)
        {
            return _context.Listings.Any(e => e.Listing_Id == id);
        }
    }
}
