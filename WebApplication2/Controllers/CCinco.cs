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
            ReporteDatosCinco reportData = new ReporteDatosCinco
            {
                AportesPastor = _repoCINCO.AportesPastor(cedula),
                SegVicepresidente = _repoCINCO.SegVicepresidente(cedula),
                CajaGeneral = _repoCINCO.CajaGeneral(cedula),
                GastosDirectivos = _repoCINCO.GastosDirectivos(cedula),
                Otros = _repoCINCO.Otros(cedula),
                TaxisyBuses = _repoCINCO.TaxisyBuses(cedula),
                CajaMenor = _repoCINCO.CajaMenor(cedula),
                BogotaCtasCorrientes = _repoCINCO.BogotaCtasCorrientes(cedula),
                InteresesCDT = _repoCINCO.InteresesCDT(cedula),
                Reafiliaciones = _repoCINCO.Reafiliaciones(cedula),
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
