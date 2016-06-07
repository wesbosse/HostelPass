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
using AspNetIdentity.WebApi.Infrastructure;
using AspNetIdentity.WebApi.Models;
using Microsoft.AspNet.Identity;

namespace AspNetIdentity.WebApi.Controllers
{

    [RoutePrefix("api/hostels")]
    public class HostelsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Route("")]
        [Authorize]
        public IQueryable<Hostel> GetManagedHostel()
        {
            return db.Hostels.Where(h => h.UserId == User.Identity.GetUserId());
        }

        [Route("search")]
        [Authorize]
        public IQueryable<Hostel> GetSearchedHostel(string city)
        {
            return db.Hostels.Where(h => h.City == city);
        }



        // GET: api/Hostels/5
        [Authorize]
        [Route("{id:int}")]
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
        [Route("{id:int}")]
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

        [Route("")]
        // POST: api/Hostels
        [Authorize(Roles = "HostelOwner")]
        [ResponseType(typeof(Hostel))]
        public IHttpActionResult PostHostel(Hostel hostel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            hostel.UserId = User.Identity.GetUserId();
            db.Hostels.Add(hostel);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = hostel.HostelId }, hostel);
        }

        // DELETE: api/Hostels/5
        [Authorize(Roles = "HostelOwner")]
        [Route("{id:int}")]
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