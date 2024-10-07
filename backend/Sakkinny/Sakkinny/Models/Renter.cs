using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sakkinny.Models
{
    public class Renter
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; } // Reference to ApplicationUser

        [ForeignKey("Apartment")]
        public int ApartmentId { get; set; } // Reference to Apartment

        public ApplicationUser User { get; set; } // Navigation property to User
        public Apartment Apartment { get; set; } // Navigation property to Apartment
    }
}
