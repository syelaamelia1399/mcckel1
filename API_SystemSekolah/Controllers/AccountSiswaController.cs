using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API_SystemSekolah.Context;
using API_SystemSekolah.Repositories.Data;
using API.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using API_SystemSekolah.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_SystemSekolah.Controllers
{
    [Route("api/[controller]")]
    public class AccountSiswaController : Controller
    {
        private AccountSiswaRepository account;
        private MyContext context;
        private IConfiguration _configuration;
        public AccountSiswaController(AccountSiswaRepository repository, MyContext mycontext, IConfiguration configuration)
        {
            this.account = repository;
            this.context = mycontext;
            this._configuration = configuration;
        }
        //[AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult Login(LoginSiswaViewModel Login)
        {
            try
            {
                var data = account.Login(Login.Email, Login.Password);
                if (data == null)
                {
                    return Ok(new
                    {
                        Message = "Email/Password salah",
                        StatusCode = 200
                    });
                }
                else
                {
                    string token = Token(Login.Email, Login.Password);
                    return Ok(new
                    {
                        Message = "Berhasil Login",
                        StatusCode = 200,
                        Data = data,
                        token
                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message,
                    StatusCode = 400
                });
            }
        }

        [HttpPut]
        [Route("ChangePassword")]
        public ActionResult ChangePassword(string email, string password, string newPassword)
        {
            try
            {
                var item = account.ChangePassword(email, password, newPassword);

                return item switch
                {
                    1 => Ok(new
                    {
                        StatusCode = 200,
                        Messege = "Berhasil Untuk Change Password",
                    }),
                    2 => Ok(new
                    {
                        StatusCode = 200,
                        Messege = "Password Lama Anda Salah"
                    }),
                    3 => Ok(new
                    {
                        StatusCode = 200,
                        Messege = "Email Salah"
                    })

                };
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Messege = ex.Message
                });
            }
        }

        [HttpPut]
        [Route("ForgotPassword")]
        public ActionResult ForgetPasswordSiswa(string email, string newPassword)
        {
            try
            {
                var item = account.ForgotPassword(email, newPassword);

                return item switch
                {
                    1 => Ok(new
                    {
                        StatusCode = 200,
                        Messege = "Berhasil Untuk Forgot Password",
                    }),
                    2 => Ok(new
                    {
                        StatusCode = 200,
                        Messege = "Email Tidak Terdaftar"
                    })
                };
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Messege = ex.Message
                });
            }
        }

        private string Token(string email, string password)
        {
            var data = context.Siswas.FirstOrDefault(option =>
            option.Email.Equals(email));

            //Hashing.validatePassword(password, data.Password)
            bool validate = Hashing.ValidatePassword(password, data.Password);
            if (validate)
            {
                var claims = new[] {
                            new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                            new Claim("Name", data.Name),
                            new Claim("email", data.Email),
                           //new Claim("id",data.),
                            //new Claim("role", data.Role.Name)
                        };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signIn);
                var tokenCode = new JwtSecurityTokenHandler().WriteToken(token);
                return tokenCode;
            }
            return null;
        }

    }
}

