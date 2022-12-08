using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API_SystemSekolah.Models;
using API_SystemSekolah.Repositories.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API_SystemSekolah.Controllers
{
    [Route("api/[controller]")]
    public class KelasController : Controller
    {
        private readonly KelasRepository _repository;
        public KelasController(KelasRepository kelasRepository)
        {
            _repository = kelasRepository;
        }

        // GET: api/values
        [HttpGet]
        public ActionResult GetAll()
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
        public ActionResult Create(Kelas kelas)
        {
            try
            {
                var result = _repository.Create(kelas);
                if (result == 0)
                {
                    return Ok(new { Message = "Failed Create New Data" });
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
        [HttpPut("{Id}")]
        public ActionResult Update(Kelas kelas)
        {
            try
            {
                var result = _repository.Update(kelas);
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

