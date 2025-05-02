using Application.Contracts;
using Application.Dtos.Request;
using Infrastruture.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthController(AppDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(x => x.Email == request.Email && x.Senha == request.Senha);

            

            if (user != null)
            {
                var token = _jwtService.GerarToken(request);

                Response.Cookies.Append("access_token", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,

                    SameSite = SameSiteMode.Lax,
                    Expires = DateTime.Now.AddMinutes(30)
                });

                return Ok(token);
            }

            return BadRequest("Erro ao efetuar login. Confira as credencias e tente novamente");
        }

       
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("access_token");

            return Ok();
        }


    }
}
