using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_SystemSekolah.Models;
using API_SystemSekolah.Repositories.Data;
using API_SystemSekolah.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_SystemSekolah.Controllers
{
    [Route("api/[controller]")]
    public class SiswaController : Controller
    {
        private readonly SiswaRepository _repository;

        public SiswaController(SiswaRepository siswaRepository)
        {
            _repository = siswaRepository;
        }

        // GET: api/values
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var data = _repository.Get();
                if (data == null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Not Found",
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Load Successful",
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


        // GET api/values/5
        [HttpGet("{nis}")]
        public ActionResult GetById(int nis)
        {
            try
            {
                var data = _repository.GetById(nis);
                if (data == null)
                {
                    return Ok(new { Message = "Data Not Found" });
                }
                else
                {
                    return Ok(new
                    {
                        Message = "Data Load Successful",
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
        [HttpPost]
        public ActionResult Create(Siswa siswa)
        {
            try
            {
                var result = _repository.Create(siswa);
                if (result == 0)
                {
                    return Ok(new { Message = "Email or Name Already Exist" });
                }
                return Ok(new { Message = "Success Create New Data" });
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
        [HttpPut("{NIS}")]
        public ActionResult Update(UpdateSiswaVM updateSiswa)
        {
            try
            {
                var result = _repository.Update(updateSiswa);
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
                    Message = "Something Wrong..."
                });
            }

        }


        // DELETE api/values/5
        [HttpDelete("{nis}")]
        public ActionResult Delete(int nis)
        {
            try
            {
                var result = _repository.Delete(nis);
                if (result == 0)
                {
                    return Ok(new { Message = "Failed Delete Data" });

                }
                return Ok(new { Message = "Deleted Data Sucessful" });
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
    }
}

