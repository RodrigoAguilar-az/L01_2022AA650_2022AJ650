using System.ComponentModel.DataAnnotations;

namespace L01_2022AA650_2022AJ650.Models
{
    public class usuarios
    {
        [Key]
        public int usuarioid { get; set; }
        public int rolid { get; set; }

        public string nombreUsuario { get; set; }

        public string clave { get; set; }

        public string nombre { get; set; }
        public string apellido { get; set; }

    }
}
