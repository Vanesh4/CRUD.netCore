using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Repo;

namespace WebApplication2.Controllers
{
    //[Authorize]
    public class CTerceros : Controller
	{
		RTerceros _repoTerceros = new RTerceros();
		public IActionResult GetTerceros()
		{
			var ListaPastores = _repoTerceros.listar20();
			return View(ListaPastores);
		}
		public IActionResult GetTercerosTodos()
		{
			return View("GetTerceros", _repoTerceros.listarTodos());
		}

		[HttpGet]
		[HttpPost]
		public IActionResult FiltroPorCodTer(string codTer)
		{
			return View(_repoTerceros.FiltroId(codTer));
		}

        public IActionResult NombreCedula(string cedula)
        {
            List<MTerceros> fechaCal = _repoTerceros.FiltroId(cedula);
            string nombre = "";
            if (fechaCal != null && fechaCal.Count > 0)
            {
                nombre = fechaCal[0].NOM_TER;
            }

            return Json(new { Nombre = nombre });
        }

        public IActionResult FiltroPorNomTer([FromForm] string nomTer)
		{
			return View(_repoTerceros.FiltroPorNomTer(nomTer));
		}

        [HttpPost]
        public IActionResult Delete(int idRow)
        {
            var respuesta = _repoTerceros.Delete(idRow);
            if (respuesta)
                return Json(new { success = true, message = "El elemento se eliminó correctamente." });
            else
            {
                return Json(new { success = false, message = "Error al eliminar el elemento." });
            }
        }
    }
}
