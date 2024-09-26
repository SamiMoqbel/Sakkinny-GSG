using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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