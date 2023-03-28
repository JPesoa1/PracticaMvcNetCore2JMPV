using Microsoft.EntityFrameworkCore;
using MvcNetCore2JMPV.Models;

namespace MvcNetCore2JMPV.Data
{
    public class LibroContext : DbContext
    {
        public LibroContext(DbContextOptions<LibroContext> options) : base(options)
        {
        }

        public DbSet<Generos> Generos { get; set; }
        public DbSet<Libros> Libros{ get; set; }
        public DbSet<Pedidos> Pedidos { get; set; }
        public DbSet<Usuarios> Usuarios{ get; set; }
        public DbSet<VistaPedidos> VistaPedidos { get; set; }



    }
}
