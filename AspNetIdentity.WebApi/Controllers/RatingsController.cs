using AspNetIdentity.Core.Domain;
using AspNetIdentity.Core.Infrastructure;
using AspNetIdentity.Core.Repository;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace AspNetIdentity.WebApi.Controllers
{
    [RoutePrefix("api/ratings")]
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
        [Route("")]
        public IQueryable<Rating> GetRatings()
        {
            return _ratingRepository.GetAll();
        }

        // GET: api/Ratings/5
        [ResponseType(typeof(Rating))]
        [Route("{id:int}")]
        public IHttpActionResult GetRating(int id)
        {
            Rating rating = _ratingRepository.GetById(id);

            if (rating == null)
            {
                return NotFound();
            }

            return Ok(rating);
        }

        // PUT: api/Ratings/5
        /*[ResponseType(typeof(void))]
        [Route("{id:int}")]
        public IHttpActionResult PutRating(int id, Rating rating)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != rating.RatingId)
            {
                return BadRequest();
            }

            _ratingRepository.Update(rating);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!RatingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }*/

        // POST: api/Ratings
        [ResponseType(typeof(Rating))]
        [Route("")]
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

        // DELETE: api/Ratings/5
        /*[ResponseType(typeof(Rating))]
        [Route("{id:int}")]
        public IHttpActionResult DeleteRating(int id)
        {
            Rating rating = _ratingRepository.GetById(id);
            if (rating == null)
            {
                return NotFound();
            }

            _ratingRepository.Delete(rating);
            _unitOfWork.Commit();

            return Ok(rating);
        }*/

/*        private bool RatingExists(int id)
        {
            return _ratingRepository.Any(e => e.RatingId == id);
        }*/
    }
}