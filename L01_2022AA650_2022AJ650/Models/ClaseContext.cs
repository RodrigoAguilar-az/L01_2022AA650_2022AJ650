using Microsoft.EntityFrameworkCore;

namespace L01_2022AA650_2022AJ650.Models
{
    public class ClaseContext: DbContext
    {
        public ClaseContext(DbContextOptions<ClaseContext> options) : base(options)
        {

        }

        //public DbSet<Autor> Autor { get; set; }
        //public DbSet<Libro> Libro { get; set; }
    }
}
