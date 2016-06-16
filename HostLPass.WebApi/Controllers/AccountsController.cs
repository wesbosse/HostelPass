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

        //[Authorize(Roles = "Admin")]
        //[Route("user/{id:guid}", Name = "GetUserById")]
        //public async Task<IHttpActionResult> GetUser(string Id)
        //{
        //    //Only SuperAdmin or Admin can delete users (Later when implement roles)
        //    var user = _userRepository.GetById(Id);

        //    if (user != null)
        //    {
        //        return Ok(new { Username = user.UserName, FirstName = user.FirstName, LastName =user.LastName });
        //    }

        //    return NotFound();

        //}

        //[Authorize(Roles = "Admin")]
        //[Route("user/{username}")]
        //public async Task<IHttpActionResult> GetUserByName(string username)
        //{
        //    //Only SuperAdmin or Admin can delete users (Later when implement roles)
        //    var user = await this.AppUserManager.FindByNameAsync(username);

        //    if (user != null)
        //    {
        //        return Ok(this.TheModelFactory.Create(user));
        //    }

        //    return NotFound();

        //}

        // POST api/Accounts/RegisterUser

        //[AllowAnonymous]
        //[HttpGet]
        //[Route("ConfirmEmail", Name = "ConfirmEmailRoute")]
        //public async Task<IHttpActionResult> ConfirmEmail(string userId = "", string code = "")
        //{
        //    if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
        //    {
        //        ModelState.AddModelError("", "User Id and Code are required");
        //        return BadRequest(ModelState);
        //    }

        //    IdentityResult result = await this.AppUserManager.ConfirmEmailAsync(userId, code);

        //    if (result.Succeeded)
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return GetErrorResult(result);
        //    }
        //}

        //[Authorize]
        //[Route("ChangePassword")]
        //public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    IdentityResult result = await this.AppUserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

        //    if (!result.Succeeded)
        //    {
        //        return GetErrorResult(result);
        //    }

        //    return Ok();
        //}

        //[Authorize(Roles = "Admin")]
        //[Route("user/{id:guid}")]
        //public async Task<IHttpActionResult> DeleteUser(string id)
        //{

        //    //Only SuperAdmin or Admin can delete users (Later when implement roles)

        //    var appUser = await this.AppUserManager.FindByIdAsync(id);

        //    if (appUser != null)
        //    {
        //        IdentityResult result = await this.AppUserManager.DeleteAsync(appUser);

        //        if (!result.Succeeded)
        //        {
        //            return GetErrorResult(result);
        //        }

        //        return Ok();

        //    }

        //    return NotFound();

        //}

        //[Authorize(Roles="Admin")]
        //[Route("user/{id:guid}/roles")]
        //[HttpPut]
        //public async Task<IHttpActionResult> AssignRolesToUser([FromUri] string id, [FromBody] string[] rolesToAssign)
        //{

        //    var appUser = await this.AppUserManager.FindByIdAsync(id);

        //    if (appUser == null)
        //    {
        //        return NotFound();
        //    }

        //    var currentRoles = await this.AppUserManager.GetRolesAsync(appUser.Id);

        //    var rolesNotExists = rolesToAssign.Except(this.AppRoleManager.Roles.Select(x => x.Name)).ToArray();

        //    if (rolesNotExists.Count() > 0) {

        //        ModelState.AddModelError("", string.Format("Roles '{0}' does not exixts in the system", string.Join(",", rolesNotExists)));
        //        return BadRequest(ModelState);
        //    }

        //    IdentityResult removeResult = await this.AppUserManager.RemoveFromRolesAsync(appUser.Id, currentRoles.ToArray());

        //    if (!removeResult.Succeeded)
        //    {
        //        ModelState.AddModelError("", "Failed to remove user roles");
        //        return BadRequest(ModelState);
        //    }

        //    IdentityResult addResult = await this.AppUserManager.AddToRolesAsync(appUser.Id, rolesToAssign);

        //    if (!addResult.Succeeded)
        //    {
        //        ModelState.AddModelError("", "Failed to add user roles");
        //        return BadRequest(ModelState);
        //    }

        //    return Ok();

        //}
    }
}