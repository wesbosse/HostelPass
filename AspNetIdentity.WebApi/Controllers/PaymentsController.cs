using AspNetIdentity.Core.Domain;
using AspNetIdentity.Core.Infrastructure;
using AspNetIdentity.Core.Repository;
using Microsoft.AspNet.Identity;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace AspNetIdentity.WebApi.Controllers
{
    [RoutePrefix("api/payments")]
    [Authorize]
    public class PaymentsController : BaseApiController
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentsController(IPaymentRepository paymentRepository, IUnitOfWork unitOfWork, IHostLUserRepository userRepository) : base(userRepository) 
        {
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Payments
        [Route("")]
        public IQueryable<Payment> GetPayments()
        {
            if (CurrentUser.Roles.Any(r => r.Role.Name == "HostelOwner"))
            {
                return _paymentRepository.Where(p => CurrentUser.Hostels.Any(h => h.HostelId == p.Reservation.HostelId));
            }
            if(CurrentUser.Roles.Any(r => r.Role.Name == "Traveller"))
            {
                return _paymentRepository.Where(p => p.Reservation.UserId == CurrentUser.Id);
            }
            return null;
        }

        // GET: api/Payments/5
        [ResponseType(typeof(Payment))]
        [Route("{id:int}")]
        public IHttpActionResult GetPayment(int id)
        {
            Payment payment = _paymentRepository.GetById(id);
            if (payment == null || CurrentUser.Reservations.All(r => r.PaymentId != id))
            {
                return NotFound();
            }

            return Ok(payment);
        }

        // PUT: api/Payments/5
        /*[ResponseType(typeof(void))]
        [Route("{id:int}")]
        public IHttpActionResult PutPayment(int id, Payment payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != payment.PaymentId)
            {
                return BadRequest();
            }

            _paymentRepository.Update(payment);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
            {
                if (!PaymentExists(id))
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

        // POST: api/Payments
        [ResponseType(typeof(Payment))]
        [Route("")]
        public IHttpActionResult PostPayment(Payment payment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (CurrentUser.Reservations.All(r => r.ReservationId != payment.ReservationId))
            {
                return BadRequest();
            }

            _paymentRepository.Add(payment);
            _unitOfWork.Commit();

            return CreatedAtRoute("DefaultApi", new { id = payment.PaymentId }, payment);
        }

        /*// DELETE: api/Payments/5
        [ResponseType(typeof(Payment))]
        [Route("{id:int}")]
        public IHttpActionResult DeletePayment(int id)
        {
            Payment payment = _paymentRepository.GetById(id);
            if (payment == null)
            {
                return NotFound();
            }

            _paymentRepository.Delete(payment);
            _unitOfWork.Commit();

            return Ok(payment);
        }*/

        private bool PaymentExists(int id)
        {
            return _paymentRepository.Any(e => e.PaymentId == id);
        }
    }
}