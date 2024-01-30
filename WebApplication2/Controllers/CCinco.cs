using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Repo;
using Rotativa.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Rotativa.AspNetCore.Options;

namespace WebApplication2.Controllers
{
    [Authorize]
    public class CCinco : Controller
	{
		RCinco _repoCINCO = new RCinco();
		public IActionResult Cinco()
		{
			return View();
		}

        public ReporteDatosCinco repDatos(string cedula)
        {
            List<Cinco> listaAportesPastor = _repoCINCO.AportesPastor(cedula);
            List<Cinco> listaCajaGeneral = _repoCINCO.CajaGeneral(cedula);
            ReporteDatosCinco reportData = new ReporteDatosCinco
            {
                CajaGeneral = listaCajaGeneral,
                AportesPastor = listaAportesPastor,
                GastosDirectivos = _repoCINCO.GastosDirectivos(cedula),
            };
            return reportData;
        }
        public IActionResult CincoData(string cedula)
        {          
            return View("CincoData", repDatos(cedula));
        }
        public IActionResult PreviewCinco(string cedula)
        {
            return View("Cinco", repDatos(cedula));
        }

        [HttpPost]
        public async Task<IActionResult> GenerarPDF(string cedula, string nombre)
        {
            DateTime fecha = DateTime.Now;
            Orientation orientation = Orientation.Landscape;
            var pdf = await new ViewAsPdf("CincoData", repDatos(cedula))
            {
                PageOrientation = orientation,
            }.BuildFile(ControllerContext);
            

            string nom = nombre.Split(' ')[0];
            string nombreArchivo = $"Reporte({fecha.ToString("yyyy-MM-dd")}) {cedula}{nom}.pdf";
            
            return File(pdf, "application/pdf", nombreArchivo);
        }
    }
}
