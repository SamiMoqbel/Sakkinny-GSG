namespace Sakkinny.Models.Dtos
{
    public class ApartmentDto
    {   public int Id { get; set; }
        public string? title { get; set; }
        public string? subTitle { get; set; }
        public string? location { get; set; }
        public int? roomsNumber { get; set; }
        public int? roomsAvailable { get; set; }
        public decimal? price { get; set; }
<<<<<<< HEAD
<<<<<<< HEAD
        public string OwnerId { get; set; } 
=======
        public string? OwnerId { get; set; } // Ensure this is mapped from the model
>>>>>>> eb6893e822c95b25edcf0bdf26ad0b515e121398
=======
        public string? OwnerId { get; set; } // Ensure this is mapped from the model
>>>>>>> eb6893e822c95b25edcf0bdf26ad0b515e121398

        public List<IFormFile>? Images { get; set; }
    }
}
