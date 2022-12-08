using API_SystemSekolah.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_SystemSekolah.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JadwalSiswaController : Controller
    {
        private readonly JadwalSiswaRepository _repository;

        public JadwalSiswaController(JadwalSiswaRepository jadwalSiswaRepository)
        {
            _repository = jadwalSiswaRepository;
        }

        [HttpGet("{nis}")]
        public ActionResult JadwalSiswa(int nis)
        {
            try
            {
                var data = _repository.GetJadwalSiswa(nis);
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
