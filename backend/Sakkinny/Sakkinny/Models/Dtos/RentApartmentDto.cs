namespace Sakkinny.Models.Dtos
{
    public class RentApartmentDto
    {
        public int ApartmentId { get; set; }
        public int CustomerId { get; set; }
        public DateTime RentalStartDate { get; set; }
        public DateTime RentalEndDate { get; set; }
    }

    
}
