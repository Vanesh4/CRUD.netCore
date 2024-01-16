using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Repo;

namespace WebApplication2.Controllers
{
	public class CCinco : Controller
	{
		RCinco _repoCINCO = new RCinco();
		public IActionResult Cinco()
		{
			return View();
		}
        public IActionResult CincoData(string cedula)
        {
            //string cedula = "7514540";
            var datos = _repoCINCO.listarDatos(cedula);
            return View("Cinco", datos ?? new List<Cinco>());
            //return View("Cinco", _repoCINCO.listarDatos(cedula));
        }

    }
}
