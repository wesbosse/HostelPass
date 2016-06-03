using hostL.API.Infrastructure;
using hostL.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace hostL.API.Controllers
{
    [RoutePrefix("api/accounts")]
    public class AccountsController : BaseApiController
    {
        private AuthRepository _repo = new AuthRepository();

        [AllowAnonymous]
        [Route("register")]
        public async Task<IHttpActionResult> Register(UserRegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _repo.RegisterUser(model);

            if (result.Succeeded)
            {
                // Ask Camron  about this line
                var user = await _repo.FindUser(model.UserName, model.Password);
                Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

                return Created(locationHeader, TheModelFactory.Create(user));
            }
            else
            {

                return BadRequest(string.Join(",", result.Errors));
            }
        }

        protected override void Dispose(bool disposing)
        {
            _repo.Dispose();
        }

        [Route("users")]
        public IHttpActionResult GetUsers()
        {
            return Ok(this.HostLUserManager.Users.ToList().Select(u => this.TheModelFactory.Create(u)));
        }

        [Route("user/{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(string Id)
        {
            var HostLUser = await this.HostLUserManager.FindByIdAsync(Id);

            if (HostLUser != null)
            {
                return Ok(this.TheModelFactory.Create(HostLUser));
            }

            return NotFound();

        }

        [Route("user/{username}")]
        public async Task<IHttpActionResult> GetUserByName(string username)
        {
            var user = await this.HostLUserManager.FindByNameAsync(username);

            if (user != null)
            {
                return Ok(this.TheModelFactory.Create(user));
            }

            return NotFound();

        }
    }

}



