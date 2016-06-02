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
    public class AccountsController : ApiController
    {
        private AuthRepository _repo = new AuthRepository();

        [AllowAnonymous]
        [Route("api/accounts/register")]
        public async Task<IHttpActionResult> Register(UserRegistrationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _repo.RegisterUser(model);

            if (result.Succeeded)
            {
                return Ok();
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

    }
}