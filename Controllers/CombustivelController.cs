using System.Collections.Generic;
using System.Web.Mvc;
using SistemaCadastro.Models;
using SistemaCadastro.Repos;

namespace SistemaCadastro.Controllers
{
    public class CombustivelController : Controller
    {
        private CombustivelRepo repo;

        // Modificado para instanciar CombustivelRepo sem passar a string de conexão
        public CombustivelController()
        {
            repo = new CombustivelRepo(); // Sem parâmetros, agora usando o construtor padrão
        }

        // GET: Combustivel/ListarCombustivel
        public ActionResult ListarCombustivel()
        {
            List<Combustivel> combustiveis = repo.GetAll();
            return View(combustiveis);
        }

        // GET: Combustivel/Adicionar
        public ActionResult Adicionar()
        {
            return View();
        }

        // POST: Combustivel/Adicionar
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

        // GET: Combustivel/Editar/5
        public ActionResult Editar(int id)
        {
            Combustivel combustivel = repo.GetById(id);
            return View(combustivel);
        }

        // POST: Combustivel/Editar
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

        // GET: Combustivel/Excluir/5
        public ActionResult Excluir(int id)
        {
            Combustivel combustivel = repo.GetById(id);
            return View(combustivel);
        }

        // POST: Combustivel/Excluir/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Excluir(Combustivel combustivel)
        {
            repo.Delete(combustivel.CombustivelId);
            return RedirectToAction("ListarCombustivel");
        }
    }
}
