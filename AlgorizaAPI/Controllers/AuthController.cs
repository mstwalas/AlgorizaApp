using Microsoft.AspNetCore.Mvc;
using UnitOfWorkLayer;
using RepositryLayer;
using CoreLayer.Model;

namespace AlgorizaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        UnitOfWork UnitOfWork;
        public Identity UserIdentity = new Identity();
        private readonly IConfiguration configuration;

        public AuthController(IConfiguration configuration)
        {
            UnitOfWork = new UnitOfWork(new AppDbContext());
            this.configuration = configuration;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<Identity>> Register(UserDto UserRequest)
        {
            //TODO ADD User FUN
            UnitOfWork.Identites.CreatePasswordHash(UserRequest.Password, out byte[] PasswordHash, out byte[] PasswordSalt);
            UserIdentity.Username = UserRequest.Username;
            UserIdentity.PasswordHash = PasswordHash;
            UserIdentity.PasswordSalt = PasswordSalt;
            UserIdentity.Name = UserRequest.Name;
            UserIdentity.Enable = true;
            UnitOfWork.Identites.Add(UserIdentity);
            UnitOfWork.Complete();
            return Ok(UserIdentity);

        }

        [HttpPost("Login")]
        public async Task<ActionResult<string>> Login(UserDto UserRequest)
        {
            var User = UnitOfWork.Identites.FindBy(e => e.Username == UserRequest.Username).FirstOrDefault();
            if (User == null)
            {
                return BadRequest("Wrong UserName");
            }
            else
            {
                if (!UnitOfWork.Identites.verifyPasswordHash(UserRequest.Password, User.PasswordHash, User.PasswordSalt))
                {
                    return BadRequest("Wrong Password");
                }
                else
                {
                    string Token =UnitOfWork.Identites.CreateToken(User);
                    return Ok(Token);
                }
            }
        }
        /* END */
        


    }
}
