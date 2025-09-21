using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DominioWebApp.Filters
{
    public class FilterAutenticado : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string logueado = context.HttpContext.Session.GetString("usuario");
            if (string.IsNullOrWhiteSpace(logueado))
            {
                context.Result = new RedirectToActionResult("Login", "Home", new { mensaje = "Inicie sesion para continuar." });
            }
            base.OnActionExecuting(context);
        }
    }
}
