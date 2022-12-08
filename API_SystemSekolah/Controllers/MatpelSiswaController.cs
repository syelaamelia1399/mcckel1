using API_SystemSekolah.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_SystemSekolah.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatpelSiswaController : Controller
    {
        private readonly MatpelSiswaRepository _repository;

        public MatpelSiswaController(MatpelSiswaRepository matpelSiswaRepository)
        {
            _repository = matpelSiswaRepository;
        }

        [HttpGet("{nis}")]
        public ActionResult MatpelSiswa(int nis)
        {
            try
            {
                var data = _repository.GetMatpelSiswa(nis);
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
    }
}
