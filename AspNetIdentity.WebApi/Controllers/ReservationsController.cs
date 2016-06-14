using AspNetIdentity.Core.Domain;
using AspNetIdentity.Core.Infrastructure;
using AspNetIdentity.Core.Repository;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using AspNetIdentity.Data.Repository;

namespace AspNetIdentity.WebApi.Controllers
{
    [Authorize]
    public class ReservationsController : BaseApiController
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostelRepository _hostelRepository;

        public ReservationsController(IReservationRepository reservationRepository, IHostelRepository hostelRepository,
            IUnitOfWork unitOfWork, IHostLUserRepository userRepository) : base(userRepository)
        {
            _reservationRepository = reservationRepository;
            _hostelRepository = hostelRepository;
            _unitOfWork = unitOfWork;
            
        }

        // GET: api/Reservations
        
        public IQueryable<Reservation> GetReservations()
        {
            if(CurrentUser.Roles.Any(r => r.Role.Name == "HostelOwner"))
            {
                return _reservationRepository.Where(r => CurrentUser.Hostels.Any(h => h.HostelId == r.HostelId));
            }
            else
            {
                return _reservationRepository.Where(r => r.UserId == CurrentUser.Id);
            }
        }

        // GET: api/Reservations/5
        [ResponseType(typeof(Reservation))]
        public IHttpActionResult GetReservation(int id)
        {
            Reservation reservation = _reservationRepository.GetById(id);
            if (CurrentUser.Roles.Any(r => r.Role.Name == "HostelOwner"))
            {
                if (CurrentUser.Hostels.All(h => h.HostelId != reservation.HostelId))
                {
                    return NotFound();
                }
            } else
            {
                if (reservation.UserId != CurrentUser.Id)
                {
                    return NotFound();
                }
            }

            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        // PUT: api/Reservations/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutReservation(int id, Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reservation.ReservationId)
            {
                return NotFound();
            }

            _reservationRepository.Update(reservation);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!ReservationExists(id))
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

        // POST: api/Reservations
        [ResponseType(typeof(Reservation))]
        public IHttpActionResult PostReservation(Reservation reservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            reservation.UserId = CurrentUser.Id;
            reservation.CreatedDate = DateTime.Now;
            reservation.ConfirmedStatus = false;
            reservation.TotalPaid = 0;
            reservation.TotalCost = _hostelRepository.GetById(reservation.HostelId).Price;

            _reservationRepository.Add(reservation);
            _unitOfWork.Commit();

            return CreatedAtRoute("DefaultApi", new { id = reservation.ReservationId }, reservation);
        }

        // DELETE: api/Reservations/5
        [ResponseType(typeof(Reservation))]
        public IHttpActionResult DeleteReservation(int id)
        {
            Reservation reservation = _reservationRepository.GetById(id);
            if (reservation == null || reservation.Traveller.Id != CurrentUser.Id)
            {
                return NotFound();
            }

            _reservationRepository.Delete(reservation);
            _unitOfWork.Commit();

            return Ok(reservation);
        }

        private bool ReservationExists(int id)
        {
            return _reservationRepository.Any(e => e.ReservationId == id);
        }
    }
}