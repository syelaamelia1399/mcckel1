using API_SystemSekolah.Repositories.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_SystemSekolah.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NilaiSiswaController : Controller
    {
        private readonly NilaiSiswaRepository _repository;

        public NilaiSiswaController(NilaiSiswaRepository nilaiSiswaRepository)
        {
            _repository = nilaiSiswaRepository;
        }

        [HttpGet("{nis}")]
        public ActionResult NilaiSiswa(int nis)
        {
            try
            {
                var data = _repository.GetNilaiSiswa(nis);
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
