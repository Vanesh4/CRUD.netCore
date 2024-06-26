﻿using System.Data;
using System.Data.SqlClient;
using WebApplication2.Models;

namespace WebApplication2.Repo
{
    public class RCinco
    {
        private conexion _cn = new conexion();
        private List<Cinco> ObtenerDatos(string consulta, SqlParameter[] parametro)
        {
            var Lista = new List<Cinco>();

            using (var conexion = new SqlConnection(_cn.getCincaConCinco()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(consulta, conexion);
                if (parametro != null)
                {
                    cmd.Parameters.AddRange(parametro);
                }
                bool p = true;
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string nombre = p ? ObtenerNombre(reader["Cedula"].ToString()) : null;
                        Lista.Add(new Cinco()
                        {
                            codComprobante = reader["CodComprob"].ToString(),
                            cedula = reader["Cedula"].ToString(),
                            fecha = reader["Fecha"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha"]).ToString("dd-MM-yyyy") : (string)null,
                            numComprobante = reader["NumComprob"].ToString(),
                            observacion = reader["Observacion"].ToString(),
                            valorDebitos = reader["VrDebitos"].ToString(),
                            valorCreditos = reader["VrCreditos"].ToString(),
                            nombre = nombre,                            
                            cuenta = reader["Cuenta"].ToString(),
                            CuentaDescripcion = reader["Descripcion"].ToString()
                        });
                        if (p)
                        {
                            p = false;
                        }
                    }
                }
            }

            return Lista;
        }
        private string ObtenerNombre(string cedula)
        {
            string nombrePastor = "";
            using (var conexionAPP = new SqlConnection(_cn.getCadenaConAPP()))
            {
                conexionAPP.Open();
                SqlCommand cmdNOM = new SqlCommand($"SELECT NOM_TER FROM Terceros WHERE COD_TER = @cedula", conexionAPP);
                SqlParameter parametro = new SqlParameter("@cedula", cedula);
                cmdNOM.Parameters.Add(parametro);
                using (var readerApp = cmdNOM.ExecuteReader())
                {
                    if (readerApp.Read())
                    {
                        nombrePastor = readerApp["NOM_TER"].ToString();
                    }
                }
            }
            return nombrePastor;
        }

        public List<Cinco> MOVCont(string cedula)
        {
            string consulta = "SELECT * FROM REPMoviCont WHERE Cedula = @cedula order by Cuenta, Fecha;";
            SqlParameter[] parametros = { new SqlParameter("@cedula", cedula) };
            return ObtenerDatos(consulta, parametros);
        }

        public List<Cinco> porCuentas(string cuenta, string cedula)
        {
            string consulta = "SELECT * FROM REPMoviCont WHERE Cedula = @cedula and Cuenta = @cuenta order by fecha;";
            SqlParameter[] parametros = {
                new SqlParameter("@cedula", cedula),
                new SqlParameter("@cuenta", cuenta)
            };
            return ObtenerDatos(consulta, parametros);
        }

        public List<Cinco> Rep20MovCont()
        {
            string consulta = "SELECT TOP(20) * FROM [dbo].[REPMoviCont] ORDER BY Fecha;";
            return ObtenerDatos(consulta, null);
        }
            

        //uno por uno
        //public List<Cinco> AportesPastor(string cedula)
        //{
        //    string consulta = "SELECT * FROM REPAportesPastor WHERE Cedula = @cedula order by Fecha;";
        //    SqlParameter[] parametros = { new SqlParameter("@cedula", cedula) };
        //    return ObtenerDatos(consulta, parametros);
        //}

        //public List<Cinco> CajaGeneral(string cedula)
        //{
        //    string consulta = "SELECT * FROM REPCajaGeneral WHERE Cedula = @cedula order by Fecha;";
        //    SqlParameter[] parametros = { new SqlParameter("@cedula", cedula) };
        //    return ObtenerDatos(consulta, parametros);
        //}

        //public List<Cinco> GastosDirectivos(string cedula)
        //{
        //    string consulta = "SELECT * FROM REPGastosDirectivos WHERE Cedula = @cedula order by Fecha;";
        //    SqlParameter[] parametros = { new SqlParameter("@cedula", cedula) };
        //    return ObtenerDatos(consulta, parametros);
        //}
        //public List<Cinco> Otros(string cedula)
        //{
        //    string consulta = "SELECT * FROM REPOtros WHERE Cedula = @cedula order by Fecha;";
        //    SqlParameter[] parametros = { new SqlParameter("@cedula", cedula) };
        //    return ObtenerDatos(consulta, parametros);
        //}
        //public List<Cinco> TaxisyBuses(string cedula)
        //{
        //    string consulta = "SELECT * FROM REPTaxisBuses WHERE Cedula = @cedula order by Fecha;";
        //    SqlParameter[] parametros = { new SqlParameter("@cedula", cedula) };
        //    return ObtenerDatos(consulta, parametros);
        //}
        //public List<Cinco> BogotaCtasCorrientes(string cedula)
        //{
        //    string consulta = "SELECT * FROM REPBogotaCtasCorrientes WHERE Cedula = @cedula order by Fecha;";
        //    SqlParameter[] parametros = { new SqlParameter("@cedula", cedula) };
        //    return ObtenerDatos(consulta, parametros);
        //}
        //public List<Cinco> Gastos(string cedula)
        //{
        //    string consulta = "SELECT * FROM RepGastos WHERE Cedula = @cedula order by Fecha;";
        //    SqlParameter[] parametros = { new SqlParameter("@cedula", cedula) };
        //    return ObtenerDatos(consulta, parametros);
        //}
        //public List<Cinco> SegVicepresidente(string cedula)
        //{
        //    string consulta = "SELECT * FROM REPSegVicepresidente WHERE Cedula = @cedula order by Fecha;";
        //    SqlParameter[] parametros = { new SqlParameter("@cedula", cedula) };
        //    return ObtenerDatos(consulta, parametros);
        //}
        //public List<Cinco> CajaMenor(string cedula)
        //{
        //    string consulta = "SELECT * FROM REPCajaMenor WHERE Cedula = @cedula order by Fecha;";
        //    SqlParameter[] parametros = { new SqlParameter("@cedula", cedula) };
        //    return ObtenerDatos(consulta, parametros);
        //}
        //public List<Cinco> Reafiliaciones(string cedula)
        //{
        //    string consulta = "SELECT * FROM REPReafiliaciones WHERE Cedula = @cedula order by Fecha;";
        //    SqlParameter[] parametros = { new SqlParameter("@cedula", cedula) };
        //    return ObtenerDatos(consulta, parametros);
        //}
        //public List<Cinco> InteresesCDT(string cedula)
        //{
        //    string consulta = "SELECT * FROM REPInteresesCDT WHERE Cedula = @cedula order by Fecha;";
        //    SqlParameter[] parametros = { new SqlParameter("@cedula", cedula) };
        //    return ObtenerDatos(consulta, parametros);
        //}
        //public List<Cinco> Afiliaciones(string cedula)
        //{
        //    string consulta = "SELECT * FROM REPAfiliaciones WHERE Cedula = @cedula order by Fecha;";
        //    SqlParameter[] parametros = { new SqlParameter("@cedula", cedula) };
        //    return ObtenerDatos(consulta, parametros);
        //}
        //public List<Cinco> InicialInvalidez(string cedula)
        //{
        //    string consulta = "SELECT * FROM REPInicialInvalidez WHERE Cedula = @cedula order by Fecha;";
        //    SqlParameter[] parametros = { new SqlParameter("@cedula", cedula) };
        //    return ObtenerDatos(consulta, parametros);
        //}
        //public List<Cinco> NuevoInvalidez(string cedula)
        //{
        //    string consulta = "SELECT * FROM REPNuevoInvalidez WHERE Cedula = @cedula order by Fecha;";
        //    SqlParameter[] parametros = { new SqlParameter("@cedula", cedula) };
        //    return ObtenerDatos(consulta, parametros);
        //}
    }
}
