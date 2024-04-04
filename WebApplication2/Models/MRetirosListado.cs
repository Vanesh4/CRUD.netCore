namespace WebApplication2.Models
{
    public class MRetirosListado
    {
        public int? codTer { get; set; }
        public string? fechaParaCalculo { get; set; }

        public string? liquidacion2006 { get; set; }
        public string? liquidacion2007 { get; set; }
        public string? liquidacion2008 { get; set; }
        public string? liquidacion2009 { get; set; }
        public string? liquidacion2010 { get; set; }
        public string? liquidacion2011 { get; set; }
        public string? liquidacion2012 { get; set; }
        public string? liquidacion2013 { get; set; }
        public string? liquidacion2014 { get; set; }
        public string? liquidacion2015 { get; set; }
        public string? liquidacion2016 { get; set; }
        //public string? liquidacion2017PLUS { get; set; }
        //public string? liquidacion2018PLUS { get; set; }
        //public string? liquidacion2019PLUS { get; set; }
        //public string? liquidacion2020PLUS { get; set; }
        //public string? liquidacion2021PLUS { get; set; }
        //public string? liquidacion2022PLUS { get; set; }
        //public string? liquidacion2023PLUS { get; set; }

        public List<string> liquidacion2017 {  get; set; }
        public List<string> liquidacion2018 {  get; set; }


        public string verficacion { get; set; }
        public string verficacionFecha { get; set; }
        public string verficacionUsuario { get; set; }


    }
}
