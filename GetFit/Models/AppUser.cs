using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GetFit.Models
{
    public class AppUser : IdentityUser
    {
        public int? Height { get; set; }
        public int? Weight { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string? City { get; set; }
        public string? State { get; set; } 
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public ICollection<Gym>? Gyms { get; set; }
        public ICollection<Home>? Homes { get; set; }
    }
}
