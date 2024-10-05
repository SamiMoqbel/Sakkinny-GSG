namespace Sakkinny.Models.Dtos
{
    public class getApartmentDetailsDto
    {
        public string? Title { get; set; }
        public string? subTitle { get; set; }
        public string? location { get; set; }
        public int? roomsNumber { get; set; }
        public int? roomsAvailable { get; set; }
        public decimal? price { get; set; }

        public List<string>? Base64Images { get; set; }

    }
}