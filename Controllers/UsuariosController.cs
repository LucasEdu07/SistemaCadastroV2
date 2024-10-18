using SistemaCadastro.Models;
using System.Web.Mvc;

namespace SistemaCadastro.Controllers
{
    public class UsuariosController : Controller
    {
        private UsuarioRepo _repositorio = new UsuarioRepo();

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuarios usuario)
        {
            if (ModelState.IsValid)
            {
                var usuarioValido = _repositorio.ObterUsuario(usuario.NomeUsuario, usuario.Senha);

                if (usuarioValido != null)
                {
                    System.Diagnostics.Debug.WriteLine("Usuário válido: " + usuarioValido.NomeUsuario);

                    Session["UsuarioId"] = usuarioValido.UsuarioId;
                    Session["NomeUsuario"] = usuarioValido.NomeUsuario;
                    Session["TipoUsuario"] = usuarioValido.TipoUsuario;

                    return RedirectToAction("ObterVeiculos", "Veiculos");
                }
                else
                {
                    ModelState.AddModelError("", "Usuário ou senha inválidos.");
                }
            }
            return View(usuario);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Usuarios");
        }
    }
}