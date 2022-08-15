using ApiClimate.Models;
using ApiClimate.Services;
using Microsoft.AspNetCore.Mvc;


namespace ApiClimate.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class LoginController : ControllerBase
    {
        private readonly AuthenticationServices authenticationServices;

        public LoginController(AuthenticationServices _authenticationServices)
        {
            authenticationServices = _authenticationServices;   
        }


        [HttpPost]     
        public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] AuthenticationRequest modelRequest)
        {
            AuthenticationResponse autheticationResponse = new AuthenticationResponse();

            AuthenticationResponse result = await authenticationServices.GetToken(modelRequest);

            switch (result.StatusCode)
            {
                case 200:
                    return Ok(result);
                case 400:
                    return BadRequest();
                case 401:
                    return Unauthorized();
                default:
                    return BadRequest();
            }          
        }

      
    }
}
