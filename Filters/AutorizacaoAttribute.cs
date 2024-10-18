using System.Web;
using System.Web.Mvc;

namespace SistemaCadastro.Filters
{
    public class AutorizacaoAttribute : AuthorizeAttribute
    {
        // O método deve ser public e não protected
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // Verifica se o usuário está autenticado
            if (HttpContext.Current.Session["UsuarioId"] == null)
            {
                // Redireciona para a página de login se o usuário não estiver autenticado
                filterContext.Result = new RedirectResult("~/Usuarios/Login");
            }
            else
            {
                // Verifica o tipo de usuário e pode adicionar lógica adicional aqui, se necessário
                var tipoUsuario = HttpContext.Current.Session["TipoUsuario"]?.ToString();
                if (tipoUsuario != "Administrador" && tipoUsuario != "Operador")
                {
                    // Caso o usuário não tenha permissão, redireciona ou exibe uma mensagem
                    filterContext.Result = new HttpUnauthorizedResult();
                }
            }
        }
    }
}
