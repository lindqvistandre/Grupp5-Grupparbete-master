using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace RealtyFirmAPI.Models
{
    public class Image
    {
        [Key]
        public Guid Image_Id { get; set; }
        [Required]
        public string Image_url { get; set; }
        [Required]
        public Guid Listing_Id { get; set; }

        public virtual Listing Listing { get; set; } //Navprop
    }
}
