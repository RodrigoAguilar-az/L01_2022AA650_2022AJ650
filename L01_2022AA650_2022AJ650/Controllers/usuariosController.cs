using L01_2022AA650_2022AJ650.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace L01_2022AA650_2022AJ650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly  ClaseContext _claseContext;

        public usuariosController(ClaseContext claseContext) { 
        
         _claseContext = claseContext;
        }

        //Metodo para obtener todos los usarios

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<usuarios> listadoUsuarios = (from e in _claseContext.Usuarios
                                              select e).ToList();
            if (listadoUsuarios.Count() == 0)
            {
                return NotFound();
            }

            return Ok(listadoUsuarios);
        }

        [HttpPost]
        [Route("Add")]
        public IActionResult GuardarUsario([FromBody] usuarios usuarios)
        {
            try
            {
                _claseContext.Usuarios.Add(usuarios);
                _claseContext.SaveChanges();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
