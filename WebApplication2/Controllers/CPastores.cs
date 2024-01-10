using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Repo;

namespace WebApplication2.Controllers
{
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
		public IActionResult FiltroPorCedula([FromForm] int cedula)
		{
			return View(_repoPastores.FiltroCedula(cedula));
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
