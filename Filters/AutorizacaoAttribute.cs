using System.Web;
using System.Web.Mvc;

namespace SistemaCadastro.Filters
{
    public class AutorizacaoAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (HttpContext.Current.Session["UsuarioId"] == null)
            {
                filterContext.Result = new RedirectResult("~/Usuarios/Login");
            }
            else
            {
                var tipoUsuario = HttpContext.Current.Session["TipoUsuario"]?.ToString();
                if (tipoUsuario != "Administrador" && tipoUsuario != "Operador")
                {
                    filterContext.Result = new HttpUnauthorizedResult();
                }
            }
        }
    }
}