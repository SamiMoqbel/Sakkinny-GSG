namespace Sakkinny.Models.Dtos
{
    public class CreateApartmentDto
    {
        public int Id { get; set; }
        public string? title { get; set; }
        public string? subTitle { get; set; }
        public string? location { get; set; }
        public int? roomsNumber { get; set; }
        public int? roomsAvailable { get; set; }
        public decimal? price { get; set; }
        public List<string>? pictureUrls { get; set; } = new List<string>();
    }
}
