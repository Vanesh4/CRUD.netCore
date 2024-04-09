using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Repo;

namespace WebApplication2.Controllers
{
    //[Authorize]
    public class CPastores : Controller
	{
		RPastores _repoPastores = new RPastores();
		public IActionResult GetPastores()
		{
			return View(_repoPastores.listar20());
		}
		public IActionResult GetPastoresTodos()
		{
			return View("GetPastores", _repoPastores.listarTodos());
		}

		public IActionResult FiltroPorNombre(string nombre)
		{
			return View(_repoPastores.FiltroNombre(nombre));
		}

		[HttpGet]
		[HttpPost]
		public IActionResult FiltroPorCedula(int cedula)
		{
			return View(_repoPastores.FiltroCedula(cedula));
		}

        public IActionResult NombreCedula(int cedula)
        {
            List<MPastores> RegPastor = _repoPastores.FiltroCedula(cedula);
            string nombre = "";
            if (RegPastor != null && RegPastor.Count > 0)
            {
                nombre = RegPastor[0].nombre;
            }

            return Json(new { Nombre = nombre });
        }

        [HttpPost]
		public IActionResult Update(MPastores datosAct)
		{
			var respuesta = _repoPastores.Update(datosAct);

			if (respuesta)
				return RedirectToAction("FiltroPorCedula", new { cedula = datosAct.cedula });
			else
			{
				TempData["Respuesta"] = "Hubo un problema al actualizar los datos.";
				return RedirectToAction("FiltroPorCedula", new { cedula = datosAct.cedula });
			}

		}
	}
}
