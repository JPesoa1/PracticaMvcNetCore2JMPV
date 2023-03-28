using Microsoft.EntityFrameworkCore;
using MvcNetCore2JMPV.Data;
using MvcNetCore2JMPV.Models;

namespace MvcNetCore2JMPV.Repositories
{
    public class RepositoryLibros
    {
        private LibroContext context;

        public RepositoryLibros(LibroContext context)
        {
            this.context = context;
        }


        public async Task<List<Generos>> GetGeneros()
            => await this.context.Generos.ToListAsync();

        public async Task<List<Libros>> GetLibrosGenero(int idgenero)
            => await this.context.Libros.Where(x => x.IdGenero == idgenero).ToListAsync();

        public Libros GetLibros(int posicion,ref int numeroLibros)
        {

            numeroLibros = this.context.Libros.Count();
            Libros libros = this.context.Libros.Skip(posicion).Take(3).FirstOrDefault();
            return libros;

        }
       


        public async Task<Libros> GetLibro(int idlibro)
           => await this.context.Libros.FirstOrDefaultAsync(x => x.IdLibro == idlibro);

        public async Task<Usuarios> ExisteUsuario(string email, string pass) {

            var consulta = this.context.Usuarios.Where(x => x.Email == email
                   && x.Pass == pass);

            return await consulta.FirstOrDefaultAsync();
        }

        public async Task<Usuarios> FindUsuario(int idUsuario)
            => await this.context.Usuarios
            .FirstOrDefaultAsync(x=> x.IdUsuario==idUsuario);

        public async Task<List<Libros>> GetLibrosCarritosAsync(List<int> idslibros)
        {

            return await this.context.Libros.Where(x => idslibros.Contains(x.IdLibro)).ToListAsync();
        }

        private int GetMaxPedido() {
            if (this.context.Pedidos.Count() == 0)
            {
                return 1;
            }
            else
            {
                return this.context.Pedidos.Max(z => z.IdPedido) + 1;
            }
        }


        public async Task InsertarPedido(DateTime fecha, int idlibro , int idusuario ) 
        { 
            Pedidos pedidos = new Pedidos();
            pedidos.IdPedido = this.GetMaxPedido();
            pedidos.IdFactura = this.GetMaxPedido();
            pedidos.Fecha = fecha;
            pedidos.IdLibro = idlibro;
            pedidos.IdUsuario  = idusuario;
            pedidos.Cantidad = 1;

            await this.context.Pedidos.AddAsync(pedidos);
            await this.context.SaveChangesAsync();

        }


        public async Task<List<VistaPedidos>> VerComprasUsuario(Int64 idusuario)
            => await this.context.VistaPedidos.Where(x => x.IdUsuario == idusuario).ToListAsync();
        
        
    }
}
