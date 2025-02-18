using L01_2022AA650_2022AJ650.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace L01_2022AA650_2022AJ650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class publicacionesController : ControllerBase
    {
        private readonly ClaseContext _publicacionesContexto;

        public publicacionesController(ClaseContext publicacionesContexto)
        {
            _publicacionesContexto = publicacionesContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<publicaciones> publicaciones = (from l in _publicacionesContexto.publicaciones
                                  select l).ToList();
            if (publicaciones.Count() == 0)
            {
                return NotFound();

            }

            return Ok(publicaciones);
        }




    }
}
