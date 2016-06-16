using HostLPass.Core.Domain;
using HostLPass.Core.Repository;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace HostLPass.WebApi.Controllers
{
    public class BaseApiController : ApiController
    {
        protected readonly IHostLUserRepository _userRepository;

        public BaseApiController(IHostLUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        protected HostLUser CurrentUser
        {
            get
            {
                return _userRepository.FirstOrDefault(u => u.UserName == User.Identity.Name);
            }
        }

        protected IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}