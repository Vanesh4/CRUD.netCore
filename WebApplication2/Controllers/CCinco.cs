using Microsoft.AspNetCore.Mvc;
using WebApplication2.Repo;

namespace WebApplication2.Controllers
{
	public class CCinco : Controller
	{
		RCinco _repoCINCO = new RCinco();
		public IActionResult Cinco()
		{
			return View(_repoCINCO.listarDatos(7514540));
		}

	}
}
