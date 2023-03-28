using Microsoft.AspNetCore.Mvc;
using MvcNetCore2JMPV.Extensions;
using MvcNetCore2JMPV.Filters;
using MvcNetCore2JMPV.Models;
using MvcNetCore2JMPV.Repositories;
using System.Security.Claims;

namespace MvcNetCore2JMPV.Controllers
{
    public class LibrosController : Controller
    {

        private RepositoryLibros repo;

        public LibrosController(RepositoryLibros repo)
        {
            this.repo = repo;
        }


        [AuthorizeUsers]
        public async Task<IActionResult> Index(int? posicion)
        {
            if (posicion == null)
            {
                posicion = 0;
            }
            int numero = 0;
            Libros libros = this.repo.GetLibros(posicion.Value,ref numero);
               
         
            int siguiente = posicion.Value + 1;
            if (siguiente >= numero)
            {
                siguiente = 0;
            }
            int anterior = posicion.Value - 1;
            if (anterior < 0)
            {
                anterior = numero - 1;
            }
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;


            return View(libros);
        }



        public async Task<IActionResult> Generos(int genero)
        {
            List<Libros> libros = await this.repo.GetLibrosGenero(genero);

            return View(libros);
        }

        public async Task<IActionResult> Detalles(int idlibro, int? idlibrocarrito)
        {

            if (idlibrocarrito != null)
            {
                List<int> carrito;
                if (HttpContext.Session.GetObject<List<int>>("CARRITO") == null)
                {
                    carrito = new List<int>();
                }
                else
                {
                    carrito = HttpContext.Session.GetObject<List<int>>("CARRITO");

                }
                if (carrito.Contains(idlibrocarrito.Value) == false)
                {
                    carrito.Add(idlibrocarrito.Value);
                    HttpContext.Session.SetObject("CARRITO", carrito);

                }

            }

            Libros libros =await this.repo.GetLibro(idlibro);

            return View(libros);
            
        }

        [AuthorizeUsers]
        public async Task<IActionResult> Carrito(int? idlibro) 
        {
            List<int> carrito = HttpContext.Session.GetObject<List<int>>("CARRITO");
            if (carrito == null)
            {
                return View();
            }
            else
            {
                if (idlibro!= null)
                {
                    carrito.Remove(idlibro.Value);
                    HttpContext.Session.SetObject("CARRITO", carrito);

                }
                List<Libros> libros = await this.repo.GetLibrosCarritosAsync(carrito);
                return View(libros);
            }
        }


        public async Task<IActionResult> Compra() {

            DateTime now = DateTime.Now;
            List<int> carrito = HttpContext.Session.GetObject<List<int>>("CARRITO");
            int idusuario = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            foreach (int id in carrito) {
                await this.repo.InsertarPedido(now,id,idusuario);
            }

           
            HttpContext.Session.SetObject("CARRITO", null);

            return RedirectToAction("VistaPedidos");
        }

        public async Task<IActionResult> VistaPedidos() 
        {
            Int64 idusuario = int.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            List<VistaPedidos> pedidos = await this.repo.VerComprasUsuario(idusuario);

            return View(pedidos);


        }

    }
}
