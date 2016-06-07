using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace AspNetIdentity.WebApi.Models
{
    public class HostLUser : IdentityUser
    {
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
        public bool HostelOwner { get; set; }


        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<HostLUser> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here

            return userIdentity;
        }
    }
}