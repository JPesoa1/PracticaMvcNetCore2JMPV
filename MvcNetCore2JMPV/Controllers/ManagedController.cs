using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MvcNetCore2JMPV.Filters;
using MvcNetCore2JMPV.Models;
using MvcNetCore2JMPV.Repositories;
using System.Security.Claims;

namespace MvcNetCore2JMPV.Controllers
{
    public class ManagedController : Controller
    {
        private RepositoryLibros repo;

        public ManagedController(RepositoryLibros repo)
        {
            this.repo = repo;
        }

        public async Task<IActionResult> Login(string email, string pass)
        {
            Usuarios user = await this.repo.ExisteUsuario(email, pass);
            if (user == null)
            {
                ViewData["MENSAJE"] = "Credenciales incorrectas";
                return View();
            }
            else
            {
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.NameIdentifier);

                Claim claimId = new Claim(ClaimTypes.NameIdentifier, user.IdUsuario.ToString());
                Claim claimUser = new Claim(ClaimTypes.Name, user.Nombre);
                Claim claimEmail = new Claim(ClaimTypes.Email, user.Email);
                Claim claimImagen = new Claim("imagen", user.Foto);
               
            
                    identity.AddClaim(claimId);
                    identity.AddClaim(claimUser);
                    identity.AddClaim(claimEmail);
                    identity.AddClaim(claimImagen);



                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);


                string controller = TempData["controller"].ToString();
                string action = TempData["action"].ToString();
           

                return RedirectToAction(action, controller);

            }
            return View();
        }


        [AuthorizeUsers]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Libros");

        }
    }
}
