using System.ComponentModel.DataAnnotations;

namespace RealtyFirmAPI.Models.DTO
{
    public class SubscriptionDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        public string Listing_Id { get; set; }
    }
}
