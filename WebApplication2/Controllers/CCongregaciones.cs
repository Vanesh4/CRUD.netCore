using Microsoft.AspNetCore.Mvc;
using WebApplication2.Repo;

namespace WebApplication2.Controllers
{
	public class CCongregaciones : Controller
	{
		RCongregaciones _repoCongregaciones = new RCongregaciones();
		public IActionResult GetCongregaciones()
		{
			return View(_repoCongregaciones.listar20());
		}
		public IActionResult GetCongregacionesTodos()
		{
			return View("GetCongregaciones",_repoCongregaciones.listarTodo());
		}

		public IActionResult FiltroPorNombre(string nombre)
		{
			return View(_repoCongregaciones.filtroNombrePastor(nombre));
		}
		public IActionResult FiltroNombreCongre(string nombre)
		{
			return View(_repoCongregaciones.filtroNomCongre(nombre));
		}

		public IActionResult filtroPorCedula(int cedula)
		{
			return View(_repoCongregaciones.filtroCedulaPastor(cedula));
		}
	}
}
