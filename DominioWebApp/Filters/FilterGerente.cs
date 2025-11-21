using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DominioWebApp.Filters
{
    public class FilterGerente : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string rol = context.HttpContext.Session.GetString("usuarioRol");
            if (rol != "2") //gerente
            {
                context.Result = new RedirectToActionResult("Index", "Home", new { mensaje = "No tiene acceso." });
            }
            base.OnActionExecuting(context);
        }
    }
}
