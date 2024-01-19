using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Repo;
using Rotativa.AspNetCore;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Drawing.Printing;

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
            return View("CincoData", _repoCINCO.listarDatos(cedula));
        }
        public IActionResult PreviewCinco(string cedula)
        {
            return View("Cinco", _repoCINCO.listarDatos(cedula));
        }

        [HttpPost]
        public async Task<IActionResult> GenerarPDF(string cedula, string nombre)
        {
            DateTime fecha = DateTime.Now;
            var model = _repoCINCO.listarDatos(cedula);
            var pdf = await new ViewAsPdf("CincoData", model).BuildFile(ControllerContext);
            string nom = nombre.Split(' ')[0];
            string nombreArchivo = $"Reporte({fecha.ToString("yyyy-MM-dd")}){cedula}{nom}.pdf";
            
            return File(pdf, "application/pdf", nombreArchivo);
        }
    }
}
