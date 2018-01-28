using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization; //授权
using JwtAuthSample;
using System.Security.Claims;

using Microsoft.IdentityModel.Tokens;// 一些算法在这里面
using Microsoft.Extensions.Options;//
using System.Text;

using System.IdentityModel.Tokens.Jwt;


namespace JwtAuthSample.Controllers
{

    [Route("api/[controller]")]
    public class AuthorizeController : Controller
    {
        private JwtSettings _jwtSettings;

        public AuthorizeController(IOptions<JwtSettings> jwtSettingsAccesser)
        {
            _jwtSettings = jwtSettingsAccesser.Value; //拿到配置信息
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        //默认是使用POST方式访问
        [HttpPost]
        public IActionResult Token([FromBody]LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.User == "atwind" && model.Password == "123456")
                {
                    var claims = new Claim[]{
                            new Claim(ClaimTypes.Name ,model.User),
                            new Claim(ClaimTypes.Role,"Admin")
                        };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));

                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        _jwtSettings.Issuer,
                        _jwtSettings.Audience,
                        claims,
                        DateTime.Now,
                        DateTime.Now.AddMinutes(30),
                        creds);
                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });

                }
                else
                {
                    return BadRequest();
                }
            }

            return BadRequest();

        }


    }
}
