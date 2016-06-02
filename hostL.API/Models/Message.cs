using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hostL.API.Models
{
    public class Message
    {
        // Primary Key
        public int MessageId { get; set; }
        public string UserId { get; set; }
        public int ReservationId { get; set; }

        // Fields relevant to Message
        public string Subject { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }

        // Relationship fields
        public virtual HostLUser HostLUser { get; set; }
        public virtual Reservation Reservation { get; set; }
      
    }
}