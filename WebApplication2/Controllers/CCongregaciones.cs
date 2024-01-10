using Microsoft.AspNetCore.Mvc;
using WebApplication2.Repo;

namespace WebApplication2.Controllers
{
	public class CCongregaciones : Controller
	{
		RCongregaciones _repoCongregaciones = new RCongregaciones();
		public IActionResult GetCongregaciones()
		{
			return View(_repoCongregaciones.listarTodo());
		}

		//public IActionResult FiltroPorNombre(string nombre)
		//{
		//	return View(_repoCongregaciones.FiltroNombre(nombre));
		//}
	}
}
