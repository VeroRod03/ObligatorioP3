using DominioWebApp.DTOs;
using DominioWebApp.Filters;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAppClienteHttp.Auxiliares;

namespace DominioWebApp.Controllers
{
    [FilterAutenticado]
    [FilterAdministrador]
    public class TipoGastoController : Controller
    {
        public string URLApiTipoGastos { get; set; }

        public TipoGastoController(IConfiguration config)
        {
            URLApiTipoGastos = config.GetValue<string>("URLApiTipoGastos");
        }
        
        // GET: TipoGastoController
        public ActionResult Index(string mensaje, string error)
        {
            ViewBag.Mensaje = mensaje;
            ViewBag.Error = error;
            IEnumerable<TipoGastoDTO> tipoGastos = new List<TipoGastoDTO>();

            try
            {
                string token = HttpContext.Session.GetString("token");
                HttpResponseMessage respuesta = AuxiliarClienteHttp.EnviarSolicitud(URLApiTipoGastos, "GET", null, token);

                string body = AuxiliarClienteHttp.ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode) 
                {
                    tipoGastos = JsonConvert.DeserializeObject<IEnumerable<TipoGastoDTO>>(body); 
                }
                else 
                {
                    ViewBag.Error = body; 
                }
            }
            catch (Exception)
            {
                ViewBag.Error = "Ocurrió un error inesperado. Intente de nuevo más tarde.";
            }
            return View(tipoGastos);
        }

        // GET: TipoGastoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoGastoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TipoGastoDTO gasto)
        {
           try
            {
                string token = HttpContext.Session.GetString("token");
                HttpResponseMessage respuesta = AuxiliarClienteHttp.EnviarSolicitud(URLApiTipoGastos, "POST", gasto, token);

                string body = AuxiliarClienteHttp.ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode)
                {
					return RedirectToAction(nameof(Index), new {mensaje = "Tipo de gasto creado existosamente! :)"});
                }
                else 
                {
                    ViewBag.Error = body;
                }
            }
            catch (Exception)
            {
               ViewBag.Error = "Ocurrió un error inesperado. Intente de nuevo más tarde.";
            }
            return View();
        }

        // GET: TipoGastoController/Edit/5
        public ActionResult Edit(int id)
        {
            try
            {
                string token = HttpContext.Session.GetString("token");
                HttpResponseMessage respuesta = AuxiliarClienteHttp.EnviarSolicitud($"{URLApiTipoGastos}/{id}", "GET", null, token);

                string body = AuxiliarClienteHttp.ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode)
                {
                    TipoGastoDTO gasto = JsonConvert.DeserializeObject<TipoGastoDTO>(body);
					return View(gasto);
                }
                else 
                {
                return RedirectToAction(nameof(Index), new { error = body });
                }
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index), new { error = "Ocurrió un error inesperado. Intente de nuevo más tarde." });
            }
        }

        // POST: TipoGastoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TipoGastoDTO dto)
        {
			try
            {
                string token = HttpContext.Session.GetString("token");
                HttpResponseMessage respuesta = AuxiliarClienteHttp.EnviarSolicitud(URLApiTipoGastos + $"/{dto.Id}", "PUT", dto, token);

                string body = AuxiliarClienteHttp.ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode)
                {
					return RedirectToAction(nameof(Index), new {mensaje = "Tipo de gasto editado correctamente! :)"});
                }
                else 
                {
                    ViewBag.Error = body;
					HttpResponseMessage respuestaGet = AuxiliarClienteHttp.EnviarSolicitud(URLApiTipoGastos + $"/{dto.Id}", "GET", null, token);

                	string bodyGet = AuxiliarClienteHttp.ObtenerBody(respuestaGet);

                	if (respuestaGet.IsSuccessStatusCode)
               		{
                    	TipoGastoDTO gasto = JsonConvert.DeserializeObject<TipoGastoDTO>(bodyGet);
						return View(gasto);
                	}
                	else 
                	{
                		return RedirectToAction(nameof(Index), new { error = bodyGet });
                	}
                }

            }
            catch (Exception)
            {
                ViewBag.Error = "Ocurrió un error inesperado. Intente de nuevo más tarde.";
                string token = HttpContext.Session.GetString("token");
                HttpResponseMessage respuestaGet = AuxiliarClienteHttp.EnviarSolicitud(URLApiTipoGastos + $"/{dto.Id}", "GET", null, token);

                string bodyGet = AuxiliarClienteHttp.ObtenerBody(respuestaGet);

                if (respuestaGet.IsSuccessStatusCode)
               	{
                    TipoGastoDTO gasto = JsonConvert.DeserializeObject<TipoGastoDTO>(bodyGet);
					return View(gasto);
                }
                else 
                {
                	return RedirectToAction(nameof(Index), new { error = bodyGet });
                }
            }
        }

        // GET: TipoGastoController/Delete/5
        public ActionResult Delete(int id, string mensaje)
        {
			try
            {
                ViewBag.Error = mensaje;
                string token = HttpContext.Session.GetString("token");
                HttpResponseMessage respuesta = AuxiliarClienteHttp.EnviarSolicitud(URLApiTipoGastos + $"/{id}", "GET", null, token);

                string body = AuxiliarClienteHttp.ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode)
                {
                    TipoGastoDTO gasto = JsonConvert.DeserializeObject<TipoGastoDTO>(body);
					return View(gasto);
                }
                else 
                {
                return RedirectToAction(nameof(Index), new { error = body });
                }
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index), new { error = "Ocurrió un error inesperado. Intente de nuevo más tarde." });
            }
        }

        // POST: TipoGastoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
      		try
            {
                string token = HttpContext.Session.GetString("token");
                HttpResponseMessage respuesta = AuxiliarClienteHttp.EnviarSolicitud(URLApiTipoGastos + $"/{id}", "DELETE", null, token);
                string body = AuxiliarClienteHttp.ObtenerBody(respuesta);
                if (respuesta.IsSuccessStatusCode)
                {
                	return RedirectToAction(nameof(Index), new { mensaje = "Tipo de Gasto borrado correctamente" });
                }
                else 
                {
					ViewBag.Error = body;
					HttpResponseMessage respuestaGet = AuxiliarClienteHttp.EnviarSolicitud(URLApiTipoGastos + $"/{id}", "GET", null, token);
                	string bodyGet = AuxiliarClienteHttp.ObtenerBody(respuestaGet);
                	if (respuestaGet.IsSuccessStatusCode)
               		{
                    	TipoGastoDTO gasto = JsonConvert.DeserializeObject<TipoGastoDTO>(bodyGet);
						return View(gasto);
                	}
                	else 
                	{
                		return RedirectToAction(nameof(Index), new { error = bodyGet });
                	}                
				}
            }
            catch (Exception)
            {
                ViewBag.Error = "Ocurrió un error inesperado. Intente de nuevo más tarde.";
                string token = HttpContext.Session.GetString("token");
                HttpResponseMessage respuestaGet = AuxiliarClienteHttp.EnviarSolicitud(URLApiTipoGastos + $"/{id}", "GET", null, token);

                string bodyGet = AuxiliarClienteHttp.ObtenerBody(respuestaGet);

                if (respuestaGet.IsSuccessStatusCode)
               	{
                    TipoGastoDTO gasto = JsonConvert.DeserializeObject<TipoGastoDTO>(bodyGet);
					return View(gasto);
                }
                else 
                {
                	return RedirectToAction(nameof(Index), new { error = bodyGet });
                }
            }
        }
    }
}
