using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hostL.API.Models
{
    public class HostLUser : IdentityUser
    {
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Hostel> Hostels { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}