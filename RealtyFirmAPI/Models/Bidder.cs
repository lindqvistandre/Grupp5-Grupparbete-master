using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealtyFirmAPI.Models
{
    public class Bidder
    {
        [Key]
        public Guid Bidder_Id { get; set; }
        [Required]
        public string First_Name { get; set; }
        [Required]
        public string Last_Name { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone_Number { get; set; }
        [Required]


        //NavProps

        public virtual ICollection<ListingBidder> ListingBidders { get; set; }


    }
}
