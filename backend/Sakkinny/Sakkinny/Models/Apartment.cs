using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sakkinny.Models
{
    public class Apartment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        [Required]
        public string Title { get; set; } 

        public string? SubTitle { get; set; } 

        [Required]
        public string Location { get; set; } 

        public int? RoomsNumber { get; set; } 

        public int RoomsAvailable { get; set; } 

        public decimal? Price { get; set; }

        public ICollection<ApartmentImage> Images { get; set; } = new List<ApartmentImage>();

        // Rental start and end dates
        public DateTime? RentalStartDate { get; set; }
        public DateTime? RentalEndDate { get; set; }

        public bool IsDeleted { get; set; } = false; 

        public DateTime CreationTime { get; set; } = DateTime.Now; 

        public DateTime? DeletionTime { get; set; } 
         // Doubly linked list for managing renters
        [NotMapped]
        public RenterList RenterList { get; set; } = new RenterList();

        // Helper to check if apartment is full
        public bool IsApartmentFull => RoomsAvailable <= 0;
    }
}
