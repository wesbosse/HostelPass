using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hostL.API.Models
{
    public class Reservation
    {
        // Primary Key 
        public int ReservationId { get; set; }
        public string UserId { get; set; }
        public int HostelId { get; set; }
        public int PaymentId { get; set; }

        // Fields relevant to Reservation 
        public DateTime CreatedDate { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int GuestNum { get; set; }
        public bool ConfirmedStatus { get; set; }
        public decimal TotalPaid { get; set; }
        public decimal TotalCost { get; set; }

        // Relationship fields 
        public virtual HostLUser Traveller { get; set; }
        public virtual Hostel Hostel { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }


    }
}