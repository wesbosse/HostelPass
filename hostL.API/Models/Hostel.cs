using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hostL.API.Models
{
    public class Hostel
    {
        // Primary Key
        public int HostelId { get; set; }
        public string UserId { get; set; }

        // Fields relevant to Hostel 
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        
        // Relationship fields
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}