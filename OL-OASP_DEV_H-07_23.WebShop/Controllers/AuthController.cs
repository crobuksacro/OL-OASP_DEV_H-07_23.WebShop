using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OL_OASP_DEV_H_07_23.WebShop.Services.Interfaces;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Binding.Common;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.Dto;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.Common;
using OL_OASP_DEV_H_07_23.WebShop.Shared.Models.ViewModel.UserModel;

namespace OL_OASP_DEV_H_07_23.WebShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private IAccountService accountService;


        public AuthController(IAccountService accountService)
        {
            this.accountService = accountService;

        }
        /// <summary>
        /// Authenticates a user and generates an access token and a refresh token.
        /// </summary>
        /// <param name="model">The login binding model containing the user's credentials.</param>
        /// <returns>
        /// A Task resulting in a <see cref="TokenViewModel"/> containing the access and refresh tokens for the user. Returns null if authentication fails.
        /// </returns>
        /// <remarks>
        /// This method attempts to authenticate a user using their username and password. If authentication succeeds, it generates a JWT access token and a refresh token for the user. The refresh token is stored in the user's record along with its expiry time, and both tokens are returned in a TokenViewModel. If authentication fails, the method returns null.
        /// </remarks>
        [AllowAnonymous]
        [Route("token")]
        [HttpPost]
        [ProducesResponseType(typeof(TokenViewModel), StatusCodes.Status200OK)]
        public async Task<IActionResult> Token([FromBody] LoginBinding model)
        {
            if (ModelState.IsValid)
            {
                var token = await accountService.GetToken(model);
                if (token == null)
                {
                    return BadRequest(new
                    {
                        Msg = "Invalid username or password!",
                    });
                }
                return Ok(token);
            }

            return BadRequest();
        }





    }
}
