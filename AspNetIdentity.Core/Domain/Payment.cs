using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetIdentity.Core.Domain
{
    public class Payment
    {
        //Primary Key
        public int PaymentId { get; set; }
        public int ReservationId { get; set; }

        //Fields relevant to payment
        public DateTime CreatedDate { get; set; }
        public decimal TotalPaid { get; set; }

        // Relationship fields
        public virtual Reservation Reservation { get; set; }
    }
}