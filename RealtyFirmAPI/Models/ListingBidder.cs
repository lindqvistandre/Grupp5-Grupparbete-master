using System;

namespace RealtyFirmAPI.Models
{
    public class ListingBidder
    {

        public Guid Listing_Id { get; set; }
        public Guid Bidder_Id { get; set; }
        public int Bid { get; set; }

        //Navprops

        public virtual Listing Listing { get; set; }

        public virtual Bidder Bidder { get; set; }
    }
}
