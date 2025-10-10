using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DominioWebApp.Filters
{
    public class FilterGerenteAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string rol = context.HttpContext.Session.GetString("usuarioRol");
            if (rol != "ADMINISTRADOR" || rol != "GERENTE")
            {
                context.Result = new RedirectToActionResult("Index", "Home", new { mensaje = "Debe ser un administrador o un gerente para acceder." });
            }
            base.OnActionExecuting(context);
        }
    }
}
