using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using hostL.API.Infrastructure;
using hostL.API.Models;

namespace hostL.API.Controllers
{
    public class HostelsController : ApiController
    {
        private HostLDataContext db = new HostLDataContext();

        // GET: api/Hostels
        [Authorize]
        public IQueryable<Hostel> GetHostels()
        {
            return db.Hostels;
        }

        // GET: api/Hostels/5
        [Authorize]
        [ResponseType(typeof(Hostel))]
        public IHttpActionResult GetHostel(int id)
        {
            Hostel hostel = db.Hostels.Find(id);
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

            if (id != hostel.HostelId)
            {
                return BadRequest();
            }

            db.Entry(hostel).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
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
        [ResponseType(typeof(Hostel))]
        public IHttpActionResult PostHostel(Hostel hostel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Hostels.Add(hostel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hostel.HostelId }, hostel);
        }

        // DELETE: api/Hostels/5
        [ResponseType(typeof(Hostel))]
        public IHttpActionResult DeleteHostel(int id)
        {
            Hostel hostel = db.Hostels.Find(id);
            if (hostel == null)
            {
                return NotFound();
            }

            db.Hostels.Remove(hostel);
            db.SaveChanges();

            return Ok(hostel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HostelExists(int id)
        {
            return db.Hostels.Count(e => e.HostelId == id) > 0;
        }
    }
}