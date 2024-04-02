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
        RRetirosListado _retirosListado = new RRetirosListado();
        public IActionResult Cinco()
        {
            return View();
        }

        //private ReporteDatosCinco repDatos(string cedula)
        //{
        //    ReporteDatosCinco reportData = new ReporteDatosCinco
        //    {
        //        AportesPastor = _repoCINCO.AportesPastor(cedula),
        //        AfiliacionAF = _repoCINCO.Afiliaciones(cedula),
        //        InicialInvalidezIP = _repoCINCO.InicialInvalidez(cedula),
        //        NuevoInvalidezNI = _repoCINCO.NuevoInvalidez(cedula),
        //        SegVicepresidente = _repoCINCO.SegVicepresidente(cedula),
        //        CajaGeneral = _repoCINCO.CajaGeneral(cedula),
        //        CajaMenor = _repoCINCO.CajaMenor(cedula),
        //        BogotaCtasCorrientes = _repoCINCO.BogotaCtasCorrientes(cedula),
        //        TaxisyBuses = _repoCINCO.TaxisyBuses(cedula),
        //        InteresesCDT = _repoCINCO.InteresesCDT(cedula),
        //        Reafiliaciones = _repoCINCO.Reafiliaciones(cedula),
        //        Otros = _repoCINCO.Otros(cedula),
        //        GastosDirectivos = _repoCINCO.GastosDirectivos(cedula),
        //        Gastos = _repoCINCO.Gastos(cedula),
        //    };
        //    return reportData;
        //}

      
        public IActionResult CincoData(string cedula)
        {
            //return View("CincoData", repDatos(cedula));
            return View("CincoData", _repoCINCO.MOVCont(cedula));
        }
        public IActionResult PreviewCinco(string cedula)
        {
            //return View("Cinco", repDatos(cedula));
            return View("Cinco", _repoCINCO.MOVCont(cedula));
        }

        //vista de prueba Datos en for
        public IActionResult CIncoPrueba(string cedula)
        {
            return View(_repoCINCO.MOVCont(cedula));
        }

        public IActionResult ObtenerFechaCalculo(string cedula)
        {
            string fechaCal = _retirosListado.fechadeCalculo(cedula);
            return Json(new { FechaCalculo = fechaCal });
        }

        [HttpPost]
        public async Task<IActionResult> GenerarPDF(string cedula)
        {
            DateTime fecha = DateTime.Now;
            Orientation orientation = Orientation.Landscape;
            //var pdf = await new ViewAsPdf("CincoData", repDatos(cedula))
            var pdf = await new ViewAsPdf("CincoData", _repoCINCO.MOVCont(cedula))
            {
                PageOrientation = orientation,
            }.BuildFile(ControllerContext);

            string nombreArchivo = $"Reporte({fecha.ToString("yyyy-MM-dd")}) {cedula}.pdf";

            return File(pdf, "application/pdf", nombreArchivo);
        }
    }
}
