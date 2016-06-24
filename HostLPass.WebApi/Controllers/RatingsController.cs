using HostLPass.Core.Domain;
using HostLPass.Core.Infrastructure;
using HostLPass.Core.Repository;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace HostLPass.WebApi.Controllers
{
    [Authorize]
    public class RatingsController : BaseApiController
    {
        private readonly IRatingRepository _ratingRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RatingsController(IRatingRepository ratingRepository, IUnitOfWork unitOfWork, IHostLUserRepository userRepository) : base(userRepository)
        {
            _ratingRepository = ratingRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Ratings
        public IQueryable<Rating> GetRatings()
        {
            return _ratingRepository.GetAll();
        }

        // GET: api/Ratings/5
        [ResponseType(typeof(Rating))]
        public IHttpActionResult GetRating(int id)
        {
            Rating rating = _ratingRepository.GetById(id);

            if (rating == null)
            {
                return NotFound();
            }

            return Ok(rating);
        }

        

        // POST: api/Ratings
        [ResponseType(typeof(Rating))]
        public IHttpActionResult PostRating(Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (CurrentUser.Roles.Any(r => r.Role.Name == "HostelOwner"))
            {
                // If none of the hostels reservations are ones made by the user being reviewed
                if (CurrentUser.Reservations.All(reservation => reservation.UserId != rating.TravellerId))
                {
                    return NotFound();
                }
            }
            else
            {
                // If none of the Travellers reservations are ones for the Hostel being reviewed
                if (CurrentUser.Reservations.All(reservation => reservation.HostelId != rating.HostelId))
                {
                    return NotFound();
                }
            }

            _ratingRepository.Add(rating);
            _unitOfWork.Commit();

            return CreatedAtRoute("DefaultApi", new { id = rating.RatingId }, rating);
        }

        
    }
}