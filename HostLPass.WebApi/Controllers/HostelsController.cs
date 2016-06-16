using HostLPass.Core.Domain;
using HostLPass.Core.Infrastructure;
using HostLPass.Core.Repository;
using Microsoft.AspNet.Identity;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace HostLPass.WebApi.Controllers
{
    public class HostelsController : BaseApiController
    {
        private readonly IHostelRepository _hostelRepository;
        private readonly IUnitOfWork _unitOfWork;

        public HostelsController(IHostelRepository hostelRepository, IUnitOfWork unitOfWork, IHostLUserRepository userRepository) : base(userRepository)
        {
            _hostelRepository = hostelRepository;
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public IQueryable<Hostel> GetManagedHostel()
        {
            return _hostelRepository.Where(h => h.UserId == CurrentUser.Id);
        }

        [Route("api/hostels/search/{city}")]
        [Authorize]
        public IQueryable<Hostel> GetSearchedHostel(string city)
        {
            return _hostelRepository.Where(h => h.City == city);
        }



        // GET: api/Hostels/5
        [Authorize]
        [ResponseType(typeof(Hostel))]
        public IHttpActionResult GetHostel(int id)
        {
            Hostel hostel = _hostelRepository.GetById(id);

            if (hostel == null)
            {
                return NotFound();
            }

            return Ok(hostel);
        }

        // PUT: api/Hostels/5
        [Authorize(Roles = "HostelOwner")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHostel(int id, Hostel hostel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                       
            if (id != hostel.HostelId || CurrentUser.Hostels.All(h => h.HostelId != id))
            {
                return BadRequest();
            }

            _hostelRepository.Update(hostel);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!HostelExists(id))
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


        // POST: api/Hostels
        [Authorize(Roles = "HostelOwner")]
        [ResponseType(typeof(Hostel))]
        public IHttpActionResult PostHostel(Hostel hostel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            hostel.UserId = CurrentUser.Id;
            _hostelRepository.Add(hostel);
            _unitOfWork.Commit();

            return CreatedAtRoute("DefaultApi", new { id = hostel.HostelId }, hostel);
        }

        // DELETE: api/Hostels/5
        [Authorize(Roles = "HostelOwner")]
        [ResponseType(typeof(Hostel))]
        public IHttpActionResult DeleteHostel(int id)
        {
            Hostel hostel = _hostelRepository.GetById(id);
            if (hostel == null || CurrentUser.Hostels.All(h => h.HostelId != id))
            {
                return NotFound();
            }

            _hostelRepository.Delete(hostel);
            _unitOfWork.Commit();

            return Ok(hostel);
        }

        private bool HostelExists(int id)
        {
            return _hostelRepository.Any(e => e.HostelId == id);
        }
    }
}