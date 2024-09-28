using System;
using System.Collections.Generic;

namespace Sakkinny.Models
{
    public class Renter
    {
        public int CustomerId { get; set; }
        public DateTime RentalEndDate { get; set; }
        public Renter Prev { get; set; }
        public Renter Next { get; set; }
    }

    public class RenterList
    {
        public Renter Head { get; private set; }
        public Renter Tail { get; private set; }

        // Add renter to the list in O(1) time
        public void AddToApartment(int apartmentId, int customerId, DateTime rentalEndDate)
        {
            var newRenter = new Renter
            {
                CustomerId = customerId,
                RentalEndDate = rentalEndDate,
                Prev = null,
                Next = null
            };

            if (Head == null)
            {
                Head = newRenter;
                Tail = newRenter;
            }
            else
            {
                Tail.Next = newRenter;
                newRenter.Prev = Tail;
                Tail = newRenter;
            }
        }

        // Remove renter from the list in O(1) time
        public void RemoveRenter(Renter renter)
        {
            if (renter.Prev != null)
            {
                renter.Prev.Next = renter.Next;
            }
            else
            {
                Head = renter.Next;
            }

            if (renter.Next != null)
            {
                renter.Next.Prev = renter.Prev;
            }
            else
            {
                Tail = renter.Prev;
            }
        }
    }
}
