using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Repo;

namespace WebApplication2.Controllers
{
    public class CRetirosListado : Controller
    {
        RRetirosListado _repoListado = new RRetirosListado();
        public IActionResult GetRetiros()
        {
            return View(_repoListado.listarTodos());
        }

        public IActionResult FiltroCedula(int cedula)
        {
            return View(_repoListado.FiltroCedula(cedula));
        }

        [HttpPost]
        public IActionResult Update(MRetirosListado datosVer)
        {
            try
            {
                var respuesta = _repoListado.Update(datosVer);

                if (respuesta)
                {
                    TempData["Respuesta"] = "Se actualizaron los datos";
                    return Json(new { redirectUrl = Url.Action("FiltroCedula", new { cedula = datosVer.codTer }) });
                }
                else
                {
                    TempData["Respuesta"] = "Hubo un problema al actualizar los datos";
                    return RedirectToAction("FiltroCedula", new { cedula = datosVer.codTer });
                }
            }
            catch (Exception ex)
            {
                TempData["Respuesta"] = "Error: " + ex.Message;
                return RedirectToAction("Cinco", "CCinco");

            }
        }
    }
}
