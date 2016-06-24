using HostLPass.Core.Infrastructure;
using HostLPass.Core.Models;
using HostLPass.Core.Repository;
using System.Threading.Tasks;
using System.Web.Http;

namespace HostLPass.WebApi.Controllers
{
    public class AccountsController : BaseApiController
    {
        private readonly IAuthorizationRepository _authorizationRepository;

        public AccountsController(IAuthorizationRepository authorizationRepository, IHostLUserRepository userRepository) : base(userRepository)
        {
            _authorizationRepository = authorizationRepository;
        }

        [AllowAnonymous]
        [Route("api/accounts/Create")]
        public async Task<IHttpActionResult> RegisterUser(CreateUserBindingModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _authorizationRepository.Register(userModel);

            var errorResult = GetErrorResult(result);

            return errorResult ?? Ok();
        }

        
    }
}