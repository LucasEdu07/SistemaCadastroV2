using SistemaCadastro.Models;
using SistemaCadastro.Repos;
using System.Collections.Generic;
using System.Web.Mvc;

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
            return View(cores);
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