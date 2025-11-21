using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DominioWebApp.Filters
{
    public class FilterGerenteEmpleado : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string rol = context.HttpContext.Session.GetString("usuarioRol");
            if (rol != "2" && rol != "3") //gerente y empleado
            {
                context.Result = new RedirectToActionResult("Index", "Home", new { mensaje = "No tiene acceso." });
            }
            base.OnActionExecuting(context);
        }
    }
}
