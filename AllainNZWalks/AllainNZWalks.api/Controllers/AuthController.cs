using AllainNZWalks.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace AllainNZWalks.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IUserRepo userRepo;
        private readonly ITokenHandler tokenHandler;

        public AuthController(IUserRepo userRepo, ITokenHandler tokenHandler)
        {
            this.userRepo = userRepo;
            this.tokenHandler = tokenHandler;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> LoginAsync(Models.DTO.LoginRequest loginRequest)
        {
            //Check user if authenticated


            //Check UN and PW
            var user = await userRepo.AuthenticateAsync(loginRequest.UserName, loginRequest.Password);

            if (user != null)
            {
                //Generate a JWT Token
                var token = await tokenHandler.CreateTokenAsync(user);
                return Ok(token);
            }

            return BadRequest("Username or Password is incorrect.");
        }
    }
}
