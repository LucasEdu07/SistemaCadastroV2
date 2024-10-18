using System.Web.Mvc;
using SistemaCadastro.Models;
using SistemaCadastro.Repo;

namespace SistemaCadastro.Controllers
{
    public class UsuariosController : Controller
    {
        private UsuarioRepo _repositorio = new UsuarioRepo();

        // GET: Usuarios/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Usuarios/Login
        [HttpPost]
        public ActionResult Login(Usuarios usuario)
        {
            if (ModelState.IsValid)
            {
                // Validar o usuário
                var usuarioValido = _repositorio.ObterUsuario(usuario.NomeUsuario, usuario.Senha);

                // Adicionando depuração
                if (usuarioValido != null)
                {
                    // Verifique se o usuário está realmente retornando
                    System.Diagnostics.Debug.WriteLine("Usuário válido: " + usuarioValido.NomeUsuario);

                    // Armazenar informações do usuário na sessão
                    Session["UsuarioId"] = usuarioValido.UsuarioId;
                    Session["NomeUsuario"] = usuarioValido.NomeUsuario;
                    Session["TipoUsuario"] = usuarioValido.TipoUsuario;

                    // Redirecionar para a view ObterVeiculos
                    return RedirectToAction("ObterVeiculos", "Veiculos");
                }
                else
                {
                    ModelState.AddModelError("", "Usuário ou senha inválidos.");
                }
            }
            return View(usuario);
        }

        // Método para sair (Logout)
        public ActionResult Logout()
        {
            Session.Clear(); // Limpar a sessão
            return RedirectToAction("Login", "Usuarios");
        }
    }
}
