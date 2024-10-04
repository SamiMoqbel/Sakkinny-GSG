using System.ComponentModel.DataAnnotations;

namespace Sakkinny.Models.Dtos
{
    public class CreateApartmentDto
    {
        [Required]
        public string? title { get; set; }

        public string? subTitle { get; set; }

        [Required]
        public string? location { get; set; }

        [Range(1, 100)]
        public int? roomsNumber { get; set; }

        [Range(0, 100)]
        public int? roomsAvailable { get; set; }

        public decimal? price { get; set; }

        public List<IFormFile> Images { get; set; }
    }
}
