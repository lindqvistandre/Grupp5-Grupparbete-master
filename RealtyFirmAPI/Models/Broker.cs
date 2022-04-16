using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RealtyFirmAPI.Models
{

    public class Broker
    {
        [Key]
        public Guid Broker_Id { get; set; }
        [Required]
        public string First_Name { get; set; }
        [Required]
        public string Last_Name { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone_Number { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Postal_Code { get; set; }
        [Required]
        public string Postal_Area { get; set; }

        [Required]
        public string Password { get; set; }
        [Required]
        public string Google_Id { get; set; }

        //NavProps

        public virtual ICollection<Listing> Listings { get; set; }

    }
}

