﻿namespace WebApplication2.Models
{
    public class ReporteDatosCinco
    {
        public List<Cinco> CajaGeneral { get; set; }
        public List<Cinco> SegVicepresidente {  get; set; }
        public List<Cinco> AportesPastor { get; set; }
        public List<Cinco> Gastos { get; set; }
        public List<Cinco> GastosDirectivos { get; set; }
        public List<Cinco> Otros { get; set; }

        public List<Cinco> TaxisyBuses { get; set; }

        public List<Cinco> CajaMenor { get; set; }
        public List<Cinco> BogotaCtasCorrientes { get; set; }
    }
}
