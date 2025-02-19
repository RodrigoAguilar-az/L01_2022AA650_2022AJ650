using L01_2022AA650_2022AJ650.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace L01_2022AA650_2022AJ650.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly ClaseContext _claseContext;

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

        [HttpPut]
        [Route("actualizar/{id}")]
        public IActionResult ActualizarUsuario(int id, [FromBody] usuarios usarioUpdate)
        {
            usuarios? UserActual = (from e in _claseContext.Usuarios
                                    where e.usuarioid == id
                                    select e).FirstOrDefault();



            if (UserActual == null)
            {
                return NotFound();
            }

            UserActual.nombreUsuario = usarioUpdate.nombreUsuario;
          //  UserActual.rolid = usarioUpdate.rolid;
            UserActual.nombreUsuario = usarioUpdate.nombreUsuario;
            UserActual.clave = usarioUpdate.clave;
            UserActual.nombre = usarioUpdate.nombre;
            UserActual.apellido = usarioUpdate.apellido;

            _claseContext.Entry(UserActual).State = EntityState.Modified;
            _claseContext.SaveChanges();


            return Ok(usarioUpdate);
        }

        [HttpDelete]
        [Route("eliminar/{id}")]
        public IActionResult EliminarUser(int id)
        {
            usuarios? usuarios = (from e in _claseContext.Usuarios
                                  where e.usuarioid == id
                                  select e).FirstOrDefault();



            if (usuarios == null)
            {
                return NotFound();
            }



            _claseContext.Usuarios.Attach(usuarios);
            _claseContext.Usuarios.Remove(usuarios);
            _claseContext.SaveChanges();



            return Ok(usuarios);
        }

        //Filtrar por nombre y apellido

        [HttpGet("{nombre}")]
        public IActionResult GetUsarios(string nombre, string apellido)
        {
            var user = (from e in _claseContext.Usuarios
                        where e.nombre == nombre && e.apellido == apellido
                        select new
                        {
                            e.usuarioid,
                            e.rolid,
                            e.nombreUsuario,
                            e.clave,
                            e.nombre,
                            e.apellido
                        }).FirstOrDefault();
            if (user == null)
            {
                return NotFound();

            }
            return Ok(user);

        }

        //Filtar por rol
        [HttpGet("Obtener_usuarios_por_Rol")]
        public IActionResult GetPorRol(string nombre)
        {
            var user = (from e in _claseContext.Usuarios
                        join t in _claseContext.roles
                        on e.usuarioid equals t.rolId
                        where t.rol == nombre
                        select new
                        {
                            e.usuarioid,
                            nombreRol= t.rol,
                            e.nombreUsuario,
                            e.clave,
                            e.nombre,
                            e.apellido
                        }).FirstOrDefault();
            if (user == null)
            {
                return NotFound();

            }
            return Ok(user);

        }
    }
}
