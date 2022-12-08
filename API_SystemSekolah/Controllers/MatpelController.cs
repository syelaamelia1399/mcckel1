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
    public class MatpelController : Controller
    {
        private MatpelRepository repository;
        public MatpelController(MatpelRepository matpel)
        {
            this.repository = matpel;
        }
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                var data = repository.Get();
                if (data == null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data tidak ada"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data  ada",
                        Data = data
                    });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = e.Message
                });

            }
        }
        //Get By Id
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            try
            {
                var data = repository.Get(id);
                if (data == null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Tidak Ada"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data  ada",
                        Data = data
                    });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = e.Message
                });
            }

        }


        //Create
        [HttpPost]
        public ActionResult Create(Matpel matpel)
        {
            try
            {
                var data = repository.Create(matpel);
                if (data == null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Tidak Berhasil Disimpan"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Berhasil Disimpan",
                        Data = data
                    });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = e.Message
                });
            }

        }

        //Update
        [HttpPut]
        public ActionResult Update(Matpel matpel)
        {

            try
            {
                var data = repository.Update(matpel);
                if (data == null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Tidak Berhasil Di-Update"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data  Berhasil Di-Update",
                        Data = data
                    });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = e.Message
                });
            }

        }

        //Delete
        [HttpDelete]
        public ActionResult Delete(int id)
        {

            try
            {
                var data = repository.Delete(id);
                if (data == null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Tidak Berhasil Dihapus"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data  Berhasil Dihapus",
                        Data = data
                    });
                }
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = e.Message
                });
            }

        }
    }
}

