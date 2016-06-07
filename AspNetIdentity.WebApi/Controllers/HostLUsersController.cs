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
    [RoutePrefix("api/hostlusers")]
    public class HostLUsersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // PUT: api/HostLUsers/5
        [Authorize]
        [ResponseType(typeof(void))]
        [Route("{id:int}")]
        public IHttpActionResult PutHostLUser(HostLUser hostLUser)
        {
            var id = User.Identity.GetUserId();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hostLUser.Id)
            {
                return BadRequest();
            }

            db.Entry(hostLUser).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HostLUserExists(id))
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HostLUserExists(string id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}