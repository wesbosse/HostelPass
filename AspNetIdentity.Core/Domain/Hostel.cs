using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetIdentity.Core.Domain
{
    public class Hostel
    {
        // Primary Key
        public int HostelId { get; set; }
        public int UserId { get; set; }

        // Fields relevant to Hostel 
        public string HostelName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Description { get; set; }

        // Relationship fields
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Amenity> Amenities { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual HostLUser HostelOwner { get; set; }
    }
}