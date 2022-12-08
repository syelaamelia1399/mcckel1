using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_SystemSekolah.Models;
using API_SystemSekolah.Repositories.Data;
using API_SystemSekolah.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_SystemSekolah.Controllers
{
    [Route("api/[controller]")]
    public class GuruController : Controller
    {

        private GuruRepository _repository;

        public GuruController(GuruRepository guruRepository)
        {
            _repository = guruRepository;
        }
        // GET: api/values
        //[Authorize(Roles = "Guru Admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var data = _repository.Get();
                if (data == null)
                {
                    return Ok(new
                    {
                        Message = "Data Not Found"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Load Successfull",
                        Data = data
                    });
                }
            }
            catch
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Something Wrong.."
                });
            }
        }

        // GET api/values/5
        //[Authorize]
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            try
            {
                var data = _repository.GetById(id);
                if (data == null)
                {
                    return Ok(new { Message = "Data Not Found" });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Load Successfull",
                        Data = data
                    });
                }
            }
            catch
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Something Wrong..."
                });
            }
        }

        // POST api/values
        //[Authorize(Roles = "Guru Admin")]
        [HttpPost]
        public ActionResult Create(Guru guru, int Roles, string retypePassword)
        {
            try
            {
                var result = _repository.Create(guru, Roles, retypePassword);
                if (result == 0)
                {
                    return Ok(new { Message = "Email Already Exists" });
                }
                else if (result == 1)
                {
                    return Ok(new { Message = "Retype Password Invalid" });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Register Successfull"
                    });
                }

            }
            catch
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Something Wrong..."
                });
            }
        }

        // PUT api/values/5
        //[Authorize(Roles = "Guru Admin")]
        [HttpPut("{Id}")]
        public ActionResult Update(UpdateGuruVM updateGuru)
        {
            try
            {
                var result = _repository.Update(updateGuru);
                if (result == 0)
                {
                    return Ok(new { Message = "Failed Update Data" });
                }
                return Ok(new { Message = "Success Update Data" });
            }
            catch
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Something Wrong.."
                });
            }
        }

        // DELETE api/values/5
        //[Authorize(Roles = "Guru Admin")]
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var result = _repository.Delete(id);
                if (result == 0)
                {
                    return Ok(new { Message = "Failed Delete Data" });
                }
                return Ok(new { Message = "Deleted Data Successfull" });
            }
            catch
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Something Wrong.."
                });
            }
        }

        [HttpPost("Login")]
        public ActionResult Login(string email, string password)
        {
            try
            {
                var result = _repository.Login(email, password);
                if (result == null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Email or Password Invalid",
                    });
                }

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Login Successfull",
                    Token = result
                });
            }
            catch
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Something Wrong.."
                });
            }
        }

        [HttpPost("CheckResetPassword")]
        public ActionResult CheckResetPassword(string name, string email, string noTelp, string DateOfBirth)
        {
            try
            {
                var result = _repository.CheckResetPassword(name, email, noTelp, DateOfBirth);
                if (result == 0)
                {
                    return Ok(new { Message = "Check Reset Password Failed" });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Check Reset Password Successfull",
                        Data = result
                    });
                }
            }
            catch
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Something Wrong..."
                });
            }
        }

        [HttpPost("ResetPassword")]
        public ActionResult ResetPassword(int id, string password, string retypePassword)
        {
            try
            {
                var result = _repository.NewPassword(id, password, retypePassword);
                if (result == 0)
                {
                    return Ok(new { Message = "Confirm Password Invalid" });
                }
                else if (result == 1)
                {
                    return Ok(new { Message = "Reset Password Failed" });
                }
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Reset Password Successfull"
                });
            }
            catch
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Something Wrong.."
                });
            }
        }

        //[Authorize]
        [HttpPut("ChangePassword")]
        public ActionResult ChangePassword(int id, string oldPassword, string retypePassword, string password)
        {
            try
            {
                var result = _repository.ChangePassword(id, oldPassword, retypePassword, password);
                if(result == 0)
                {
                    return Ok(new { Message = "Change Password Failed" });
                } else if(result == 1)
                {
                    return Ok(new { Message = "Retype Password Failed" });
                }
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "Success Change Password"
                });
            } catch
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Something Wrong.."
                });
            }
        }
    }
}

