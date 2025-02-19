using L01_2022AA650_2022AJ650.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        [HttpGet]
        [Route("GetById")]
        public IActionResult GetById(int id)
        {
            var pubs = (from p in _publicacionesContexto.publicaciones
                          join u in _publicacionesContexto.Usuarios
                          on p.usuarioId equals u.usuarioid
                          where u.usuarioid == id
                          select new
                          {
                              u.nombre,
                              p.titulo,
                              p.descripcion
                          }).ToList();


            if (pubs.Count() == 0)
            {
                return NotFound();

            }

            return Ok(pubs);
        }

        [HttpGet]
        [Route("GetCantidad")]
        public IActionResult GetByPub()
        {
            var pubs = (from p in _publicacionesContexto.publicaciones
                        join c in _publicacionesContexto.comentarios
                        on p.publicacionId equals c.publicacionId
                        group c by new { p.publicacionId, p.titulo } into g
                        orderby g.Count() descending
                        select new
                        {

                            publicacionId = g.Key.publicacionId,
                            titulo = g.Key.titulo,
                            cantidadComentarios = g.Count()
                        }).Take(4).ToList();


            if (pubs.Count() == 0)
            {
                return NotFound();

            }

            return Ok(pubs);
        }


        [HttpPost]
        [Route("AddPublicacion")]
        public IActionResult AddPub([FromBody] publicaciones publicaciones)
        {
            try
            {
                _publicacionesContexto.publicaciones.Add(publicaciones);
                _publicacionesContexto.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("Actualizar/{id}")]
        public IActionResult Actualiar(int id, [FromBody] publicaciones pubActualizar)
        {
            publicaciones? pubActual = (from p in _publicacionesContexto.publicaciones
                                  where p.publicacionId == id
                                  select p).FirstOrDefault();

            if (pubActual == null)
            {
                return NotFound();
            }

            pubActual.titulo = pubActualizar.titulo;
            pubActual.descripcion = pubActualizar.descripcion;
            pubActual.usuarioId = pubActualizar.usuarioId;

            _publicacionesContexto.Entry(pubActual).State = EntityState.Modified;
            _publicacionesContexto.SaveChanges();

            return Ok(pubActualizar);
        }


        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult Eliminar(int id)
        {
            publicaciones? publicacion = (from p in _publicacionesContexto.publicaciones
                                        where p.publicacionId == id
                                        select p).FirstOrDefault();


            if (publicacion == null)
            {
                return NotFound();
            }

            _publicacionesContexto.Attach(publicacion); 
            _publicacionesContexto.publicaciones.Remove(publicacion);
            _publicacionesContexto.SaveChanges();

            return Ok(publicacion);

        }


    }
}
