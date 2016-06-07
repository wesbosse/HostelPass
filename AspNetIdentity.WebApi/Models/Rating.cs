using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetIdentity.WebApi.Models
{
    public class Rating
    {
        // Primary Key
        public int RatingId { get; set; }
        public int? HostelId { get; set; }
        public string TravellerId { get; set; }

        // Fields Relevant to Rating
        public int Cleanliness { get; set; }
        public int Atmosphere { get; set; }
        public int Location { get; set; }
        public int Friendliness { get; set; }
        public int Recommend { get; set; }
        public int Communication { get; set; }
        public double AverageRating
        {
            get
            {
                int[] factors = null;

                if(HostelId.HasValue)
                {
                    factors = new int[] { Cleanliness, Atmosphere, Location, Friendliness, Recommend, Communication };
                }
                else
                {
                    factors = new int[] { Cleanliness, Friendliness, Communication };
                }

                return Math.Round(factors.Average(), 1);
            }
        }

        // Relationship Fields
        public virtual Hostel Hostel { get; set; }
        public virtual HostLUser Traveller { get; set; }
    }
}