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
            //return View(_repoListado.listarTodos());
            return View();
        }

        public IActionResult FiltroCedula(int cedula)
        {
            //try
            //{
            //    return View(_repoListado.FiltroCedula(cedula));
            //}
            //catch (Exception ex) {
            //    return View("ViewError");
            //} 
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
                    //return Json(new { redirectUrl = Url.Action("FiltroPorCodTer", "CTerceros", new { codTer = datosVer.codTer }) });
                    //return Json(new { success = true, redirectUrl = Url.Action("FiltroPorCodTer", "CTerceros", new { codTer = datosVer.codTer }) });
                    return RedirectToAction("FiltroPorCodTer", "CTerceros", new { codTer = datosVer.codTer.ToString() });
                }
                else
                {
                    TempData["RespuestaError"] = "Hubo un problema al actualizar los datos";
                    //return Json(new { redirectUrl = Url.Action("FiltroPorCodTer", "CTerceros", new { codTer = datosVer.codTer }) });
                    return RedirectToAction("FiltroPorCodTer", "CTerceros", new { codTer = datosVer.codTer.ToString() });
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
