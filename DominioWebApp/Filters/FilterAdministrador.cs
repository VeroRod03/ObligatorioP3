using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DominioWebApp.Filters
{
    public class FilterAdministrador : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string rol = context.HttpContext.Session.GetString("usuarioRol");
            if (rol != "1") //admin
            {
                context.Result = new RedirectToActionResult("Index", "Home", new { mensaje = "No tiene acceso." });
            }
            base.OnActionExecuting(context);
        }
    }
}
