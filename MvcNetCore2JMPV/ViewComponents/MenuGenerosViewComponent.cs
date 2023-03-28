using Microsoft.AspNetCore.Mvc;
using MvcNetCore2JMPV.Models;
using MvcNetCore2JMPV.Repositories;

namespace MvcNetCore2JMPV.ViewComponents
{
    public class MenuGenerosViewComponent:ViewComponent
    {

        private RepositoryLibros repo;

        public MenuGenerosViewComponent(RepositoryLibros repo)
        {
            this.repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Generos> generos = await this.repo.GetGeneros();
            return View(generos);
        }
    }
}
