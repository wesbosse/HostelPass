using HostLPass.Core.Domain;
using HostLPass.Core.Infrastructure;
using HostLPass.Core.Repository;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace HostLPass.WebApi.Controllers
{
    public class HostLUsersController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public HostLUsersController(IHostLUserRepository userRepository, IUnitOfWork unitOfWork) : base(userRepository)
        {
            _unitOfWork = unitOfWork;
        }

        // PUT: api/HostLUsers/5
        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHostLUser(HostLUser hostLUser)
        {
            var id = CurrentUser.Id;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != hostLUser.Id)
            {
                return BadRequest();
            }

            _userRepository.Update(hostLUser);

            try
            {
                _unitOfWork.Commit();
            }
            catch (Exception)
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

        private bool HostLUserExists(int id)
        {
            return _userRepository.Any(u => u.Id == id);
        }
    }
}