using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Repo;
using Rotativa.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Rotativa.AspNetCore.Options;

namespace WebApplication2.Controllers
{
    //[Authorize]
    public class CCinco : Controller
	{
		RCinco _repoCINCO = new RCinco();
		public IActionResult Cinco()
		{
			return View();
		}
        public IActionResult CincoData(string cedula)
        {
            return View("CincoData", _repoCINCO.AportesPastor("7514540"));
        }
        public IActionResult PreviewCinco(string cedula)
        {
            return View("Cinco", _repoCINCO.AportesPastor(cedula));
        }

        [HttpPost]
        public async Task<IActionResult> GenerarPDF(string cedula, string nombre)
        {
            DateTime fecha = DateTime.Now;
            var model = _repoCINCO.AportesPastor(cedula);
            Orientation orientation = Orientation.Landscape;
            var pdf = await new ViewAsPdf("CincoData", model)
            {
                PageOrientation = orientation,
            }.BuildFile(ControllerContext);
            

            string nom = nombre.Split(' ')[0];
            string nombreArchivo = $"Reporte({fecha.ToString("yyyy-MM-dd")}) {cedula}{nom}.pdf";
            
            return File(pdf, "application/pdf", nombreArchivo);
        }
    }
}
