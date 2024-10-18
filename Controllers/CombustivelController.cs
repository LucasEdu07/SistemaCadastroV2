using SistemaCadastro.Models;
using SistemaCadastro.Repos;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SistemaCadastro.Controllers
{
    public class CombustivelController : Controller
    {
        private CombustivelRepo repo;

        public CombustivelController()
        {
            repo = new CombustivelRepo();
        }

        public ActionResult ListarCombustivel()
        {
            List<Combustivel> combustiveis = repo.GetAll();
            return View(combustiveis);
        }

        public ActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar(Combustivel combustivel)
        {
            if (ModelState.IsValid)
            {
                repo.Add(combustivel);
                return RedirectToAction("ListarCombustivel");
            }
            return View(combustivel);
        }

        public ActionResult Editar(int id)
        {
            Combustivel combustivel = repo.GetById(id);
            return View(combustivel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Combustivel combustivel)
        {
            if (ModelState.IsValid)
            {
                repo.Update(combustivel);
                return RedirectToAction("ListarCombustivel");
            }
            return View(combustivel);
        }

        //public ActionResult Excluir(int id)
        //{
        //    Combustivel combustivel = repo.GetById(id);
        //    return View(combustivel);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Excluir(Combustivel combustivel)
        //{
        //    repo.Delete(combustivel.CombustivelId);
        //    return RedirectToAction("ListarCombustivel");
        //}
    }
}