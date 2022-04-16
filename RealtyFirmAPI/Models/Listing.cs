using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealtyFirmAPI.Models
{
    public class Listing
    {
        [Key]
        public Guid Listing_Id { get; set; }
        [Required]
        public string Listing_Type { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Postal_Code { get; set; }
        [Required]
        public string Postal_Area { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        [Required]
        public int Room_Count { get; set; }
        [Required]
        public int Listing_Price { get; set; }

        public string Description { get; set; }
        [Required]
        public int Year_Built { get; set; }

        public DateTime Tour_Date { get; set; }
        [Required]
        public int Floor_Area { get; set; }
        [Required]
        public int Nonusable_Floor_Area { get; set; }
        [Required]
        public int Lot_Area { get; set; }
        [Required]
        public string Form_Of_Lease { get; set; }
        public Guid Broker_Id { get; set; } //Broker_Id.

        //NavProps:

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<ListingBidder> ListingBidders { get; set; }

        public virtual Broker Broker { get; set; }
    }
}
