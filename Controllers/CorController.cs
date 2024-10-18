using System.Collections.Generic;
using System.Web.Mvc;
using SistemaCadastro.Models;
using SistemaCadastro.Repo;
using SistemaCadastro.Repos;

namespace SistemaCadastro.Controllers
{
    public class CorController : Controller
    {
        private CorRepo repo;

        public CorController()
        {
            repo = new CorRepo();
        }

        public ActionResult ListarCor()
        {
            List<Cor> cores = repo.GetAll();
            return View(cores); // Retornando a lista de cores para a view
        }

        public ActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar(Cor cor)
        {
            if (ModelState.IsValid)
            {
                repo.Add(cor);
                return RedirectToAction("ListarCor");
            }
            return View(cor);
        }

        public ActionResult Editar(int id)
        {
            Cor cor = repo.GetById(id);
            return View(cor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Cor cor)
        {
            if (ModelState.IsValid)
            {
                repo.Update(cor);
                return RedirectToAction("ListarCor");
            }
            return View(cor);
        }
    }
}
