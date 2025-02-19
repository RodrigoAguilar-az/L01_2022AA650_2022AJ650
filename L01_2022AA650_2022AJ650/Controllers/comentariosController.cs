using L01_2022AA650_2022AJ650.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2022AA650_2022AJ650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class comentariosController : ControllerBase
    {
        private readonly ClaseContext _comentariosContexto;

        public comentariosController(ClaseContext comentariosContexto)
        {
            _comentariosContexto = comentariosContexto;
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get()
        {
            List<comentarios> comentarios = (from l in _comentariosContexto.comentarios
                                                 select l).ToList();
            if (comentarios.Count() == 0)
            {
                return NotFound();

            }

            return Ok(comentarios);
        }

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int id)
        {
            var comentarios = (from c in _comentariosContexto.comentarios
                        join p in _comentariosContexto.publicaciones
                        on c.publicacionId equals p.publicacionId
                        where p.publicacionId == id
                        select new
                        {
                          p.publicacionId,
                          c.comentario,

                        }).ToList();


            if (comentarios.Count() == 0)
            {
                return NotFound();

            }

            return Ok(comentarios);
        }



        [HttpPost]
        [Route("AddComentario")]
        public IActionResult AddComment([FromBody] comentarios comentarios)
        {
            try
            {
                _comentariosContexto.comentarios.Add(comentarios);
                _comentariosContexto.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult Actualiar(int id, [FromBody] comentarios conActualizar)
        {
            comentarios? conActual = (from c in _comentariosContexto.comentarios
                                        where c.cometarioId == id
                                        select c).FirstOrDefault();

            if (conActual == null)
            {
                return NotFound();
            }

            conActual.publicacionId = conActualizar.publicacionId;
            conActual.comentario = conActualizar.comentario;
            conActual.usuarioId = conActualizar.usuarioId;

            _comentariosContexto.Entry(conActual).State = EntityState.Modified;
            _comentariosContexto.SaveChanges();

            return Ok(conActualizar);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            comentarios? comentario = (from c in _comentariosContexto.comentarios
                                      where c.cometarioId == id
                                      select c).FirstOrDefault();


            if (comentario == null)
            {
                return NotFound();
            }

            _comentariosContexto.Attach(comentario);
            _comentariosContexto.comentarios.Remove(comentario);
            _comentariosContexto.SaveChanges();

            return Ok(comentario);

        }
    }
}
