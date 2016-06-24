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
        public IQueryable<Amenity> GetAmenities()
        {
            return _amenityRepository.GetAll();
        }

        

        // POST: api/Amenities
        [Authorize(Roles = "HostelOwner")]
        [ResponseType(typeof(Amenity))]
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

     

        private bool AmenityExists(int id)
        {
            return _amenityRepository.Any(a => a.AmenityId == id);
        }
    }
}