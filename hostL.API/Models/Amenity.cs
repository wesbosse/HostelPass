using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hostL.API.Models
{
    public class Amenity
    {
        // Primary Key
        public int AmenityId { get; set; }
        public string TextAmenity { get; set; }

        //Relationship Fields
        public virtual ICollection<Hostel> Hostels { get; set; }
    }
}