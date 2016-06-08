using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace AspNetIdentity.Core.Domain
{
    public class HostLUser : IUser<int>
    {
        public HostLUser()
        {
            Reservations = new Collection<Reservation>();
            Hostels = new Collection<Hostel>();
            Messages = new Collection<Message>();
            Ratings = new Collection<Rating>();
            Roles = new Collection<UserRole>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string SecurityStamp { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public DateTime JoinDate { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Hostel> Hostels { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual ICollection<UserRole> Roles { get; set; }
        public bool HostelOwner { get; set; }
    }
}