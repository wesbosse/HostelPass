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
    [RoutePrefix("api/amenities")]
    public class AmenitiesController : BaseApiController
    {
        private readonly IAmenityRepository _amenityRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AmenitiesController(IAmenityRepository amenityRepository, IUnitOfWork unitOfWork, IHostLUserRepository userRepository) : base(userRepository)
        {
            _amenityRepository = amenityRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Amenities
        [Authorize]
        [Route("")]
        public IQueryable<Amenity> GetAmenities()
        {
            return _amenityRepository.GetAll();
        }

        // GET: api/Amenities/5
        [Authorize]
        [ResponseType(typeof(Amenity))]
        [Route("{id:int}")]
        public IHttpActionResult GetAmenity(int id)
        {
            Amenity amenity = _amenityRepository.GetById(id);

            if (amenity == null)
            {
                return NotFound();
            }

            return Ok(amenity);
        }

        // PUT: api/Amenities/5
        [Authorize(Roles = "HostelOwner")]
        [ResponseType(typeof(void))]
        [Route("{id:int}")]
        public IHttpActionResult PutAmenity(int id, Amenity amenity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != amenity.AmenityId)
            {
                return BadRequest();
            }

            _amenityRepository.Update(amenity);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!AmenityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Amenities
        [Authorize(Roles = "HostelOwner")]
        [ResponseType(typeof(Amenity))]
        [Route("")]
        public IHttpActionResult PostAmenity(Amenity amenity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _amenityRepository.Add(amenity);
            _unitOfWork.Commit();

            return CreatedAtRoute("DefaultApi", new { id = amenity.AmenityId }, amenity);
        }

        // DELETE: api/Amenities/5
        [Authorize(Roles = "HostelOwner")]
        [ResponseType(typeof(Amenity))]
        [Route("{id:int}")]
        public IHttpActionResult DeleteAmenity(int id)
        {
            Amenity amenity = _amenityRepository.GetById(id);
            if (amenity == null)
            {
                return NotFound();
            }

            _amenityRepository.Delete(amenity);
            _unitOfWork.Commit();

            return Ok(amenity);
        }

        private bool AmenityExists(int id)
        {
            return _amenityRepository.Any(a => a.AmenityId == id);
        }
    }
}