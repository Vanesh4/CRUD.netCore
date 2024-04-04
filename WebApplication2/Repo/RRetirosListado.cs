﻿using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Reflection.Metadata;
using WebApplication2.Models;

namespace WebApplication2.Repo
{
    public class RRetirosListado
    {
        private conexion _cn = new conexion();

        private List<MRetirosListado> ObtenerDatos(string consulta, SqlParameter[] parametro)
        {
            var Lista = new List<MRetirosListado>();

            using (var conexion = new SqlConnection(_cn.getCadenaConAPP()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(consulta, conexion);
                if (parametro != null)
                {
                    cmd.Parameters.AddRange(parametro);
                }

                using (var reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        Lista.Add(new MRetirosListado()
                        {
                            //codTer = Convert.ToInt32(reader["CÉDULA"]), este es con la vista
                            codTer = reader["COD_TER"] != DBNull.Value ? Convert.ToInt32(reader["COD_TER"]) : 0,
                            liquidacion2006 = validarDato(reader["liquidacion_2006"].ToString()),
                            liquidacion2007 = validarDato(reader["liquidacion_2007"].ToString()),
                            liquidacion2008 = validarDato(reader["liquidacion_2008"].ToString()),
                            liquidacion2009 = validarDato(reader["liquidacion_2009"].ToString()),
                            liquidacion2010 = validarDato(reader["liquidacion_2010"].ToString()),
                            liquidacion2011 = validarDato(reader["liquidacion_2011"].ToString()),
                            liquidacion2012 = validarDato(reader["liquidacion_2012"].ToString()),
                            liquidacion2013 = validarDato(reader["liquidacion_2013"].ToString()),
                            liquidacion2014 = validarDato(reader["liquidacion_2014"].ToString()),
                            liquidacion2015 = validarDato(reader["liquidacion_2015"].ToString()),
                            liquidacion2016 = validarDato(reader["liquidacion_2016"].ToString()),
                            //liquidacion2017PLUS = validarDato(reader["liquidacion_2017PLUS"].ToString()),
                            //liquidacion2018PLUS = validarDato(reader["liquidacion_2018PLUS"].ToString()),
                            //liquidacion2019PLUS = validarDato(reader["liquidacion_2019PLUS"].ToString()),
                            //liquidacion2020PLUS = validarDato(reader["liquidacion_2020PLUS"].ToString()),
                            //liquidacion2021PLUS = validarDato(reader["liquidacion_2021PLUS"].ToString()),
                            //liquidacion2022PLUS = validarDato(reader["liquidacion_2022PLUS"].ToString()),
                            //liquidacion2023PLUS = validarDato(reader["liquidacion_2023PLUS"].ToString()),

                            verficacion = reader["Verificacion"].ToString(),
                            verficacionFecha = reader["VerificadoFecha"] != DBNull.Value ? Convert.ToDateTime(reader["VerificadoFecha"]).ToString("dd-MM-yyyy") : (string)null,
                            verficacionUsuario = reader["VerificadoUsuario"].ToString(),
                        });
                    }
                }
                return Lista;
            }
        }


        private List<MRetirosListado> ObtenerLiquidaciones(string consulta, SqlParameter[] parametro)
        {
            var Lista = new List<MRetirosListado>();

            using (var conexion = new SqlConnection(_cn.getCadenaConAPP()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(consulta, conexion);
                if (parametro != null)
                {
                    cmd.Parameters.AddRange(parametro);
                }

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int codTer = reader["COD_TER"] != DBNull.Value ? Convert.ToInt32(reader["COD_TER"]) : 0;
                        Lista.Add(new MRetirosListado()
                        {
                            //codTer = Convert.ToInt32(reader["CÉDULA"]), este es con la vista
                            codTer = codTer,
                            fechaParaCalculo = reader["fecha_para_calculo"] != DBNull.Value ? Convert.ToDateTime(reader["fecha_para_calculo"]).ToString("dd-MM-yyyy") : (string)null,

                            liquidacion2006 = validarDato(anio2006(codTer).ToString()),
                            liquidacion2007 = validarDato(anio2007(codTer).ToString()),

                            
                            liquidacion2017 = new List<string> { validarDato(anio2017(codTer).Item1.ToString()), validarDato(anio2017(codTer).Item2.ToString()), validarDato(anio2017(codTer).Item3.ToString()) },
                            liquidacion2018 = new List<string> { "222222222", "3333333333" },

                            verficacion = reader["Verificacion"].ToString(),
                            verficacionFecha = reader["VerificadoFecha"] != DBNull.Value ? Convert.ToDateTime(reader["VerificadoFecha"]).ToString("dd-MM-yyyy") : (string)null,
                            verficacionUsuario = reader["VerificadoUsuario"].ToString(),
                        });
                    }
                }
                return Lista;
            }
        }

        public string fechadeCalculo(string cedula)
        {
            using (var conexion = new SqlConnection(_cn.getCadenaConAPP()))
            {
                try
                {
                    conexion.Open();
                    string fechaParaCalculo = "no hay fecha";
                    SqlCommand cmd = new SqlCommand("SELECT fecha_para_calculo FROM Retiros WHERE COD_TER=@codTer;", conexion);
                    cmd.Parameters.AddWithValue("@codTer", cedula);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            fechaParaCalculo = reader["fecha_para_calculo"] != DBNull.Value ? Convert.ToDateTime(reader["fecha_para_calculo"]).ToString("dd-MM-yyyy") : "no hay fecha";
                        }
                    }
                    return fechaParaCalculo;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al obtener la fecha de cálculo: " + ex.Message);
                    return "no hay fecha";
                }
            }
        }

        public List<MRetirosListado> listarTodos()
        {
            return ObtenerDatos("SELECT TOP 1000 * FROM RetirosVista;", null);
        }
        public List<MRetirosListado> FiltroCedula(int cedula)
        {
            //string consulta = "SELECT * FROM RetirosVista WHERE COD_TER = @Cedula;";
            string consulta = "SELECT * FROM RetirosNet WHERE COD_TER = @Cedula;";
            SqlParameter[] parametros = { new SqlParameter("@Cedula", cedula) };
            return ObtenerLiquidaciones(consulta, parametros);
        }

        public string validarDato(string liquidaciona)
        {
            if (liquidaciona == "0")
            {
                return "";
            }
            else
            {
                decimal valorDecimal;
                if (decimal.TryParse(liquidaciona, out valorDecimal))
                {
                    return valorDecimal.ToString("#,0.###", System.Globalization.CultureInfo.InvariantCulture);
                }
                else
                {
                    return liquidaciona;
                }
            }
        }

        

        public bool Update(MRetirosListado datosVer)
        {
            //bool res = false;
            try
            {
                DateTime? fechaActual = null;

                using (var conexion = new SqlConnection(_cn.getCadenaConAPP()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SELECT fecha_para_calculo as dato FROM RetirosNet where COD_TER = @cod_ter", conexion);
                    cmd.Parameters.AddWithValue("@cod_ter", datosVer.codTer);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            fechaActual = Convert.ToDateTime(reader["dato"]);
                        }
                    }
                    conexion.Close();
                }

                using (var conexion = new SqlConnection(_cn.getCadenaConAPP()))
                {
                    conexion.Open();
                    //Tabla retiros
                    //SqlCommand cmd = new SqlCommand("UPDATE Retiros SET VerificadoFecha=GETDATE(),Verificacion=1,VerificadoUsuario=@verficacionUsuario, fecha_para_calculo = @fechaParaCalculo WHERE COD_TER = @codTer;", conexion);
                    
                    SqlCommand cmd = new SqlCommand("UPDATE RetirosNet SET VerificadoFecha = @verificadoFecha,Verificacion=1,VerificadoUsuario=@verficacionUsuario, fecha_para_calculo = @fechaParaCalculo, fechaRespaldo = @fechaRespaldo WHERE COD_TER = @codTer;", conexion);
                    cmd.Parameters.AddWithValue("@verificadoFecha", DateTime.Now);
                    cmd.Parameters.AddWithValue("@verficacionUsuario", datosVer.verficacionUsuario);
                    cmd.Parameters.AddWithValue("@codTer", datosVer.codTer);
                    cmd.Parameters.AddWithValue("@fechaParaCalculo", datosVer.fechaParaCalculo);
                    cmd.Parameters.AddWithValue("@fechaRespaldo", fechaActual ?? (object)DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
                //res = true;
                return true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                return false;
                throw;
            }
            //return res;
        }


        //conexion fechas
        private int ObtenerFechaInt(string consulta, SqlParameter[] parametro)
        {
            int fecha = 0;
            using (var conexion = new SqlConnection(_cn.getCadenaConAPP()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(consulta, conexion);
                if (parametro != null)
                {
                    cmd.Parameters.AddRange(parametro);
                }

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        fecha = Convert.ToInt32(reader["dato"]);
                    }
                }
                return fecha;
            }
        }


        //calculos de liquidaciones
        private (int, int, int) retAnioMesDia(int cod_ter)
        {
            
            string conanio = "SELECT YEAR(fecha_para_calculo) as dato FROM RetirosNet WHERE COD_TER = @cod_ter;";
            SqlParameter[] parametroAnio = { new SqlParameter("@cod_ter", cod_ter) };
            int anio = ObtenerFechaInt(conanio, parametroAnio);
            
            string conmes = "SELECT MONTH(fecha_para_calculo) as dato FROM RetirosNet WHERE COD_TER = @cod_ter;";
            SqlParameter[] parametroMes = { new SqlParameter("@cod_ter", cod_ter) };
            int mes = ObtenerFechaInt(conmes, parametroMes);

            string condia = "SELECT DAY(fecha_para_calculo) as dato FROM RetirosNet WHERE COD_TER = @cod_ter;";
            SqlParameter[] parametroDia = { new SqlParameter("@cod_ter", cod_ter) };
            int dia = ObtenerFechaInt(condia, parametroDia);

            return (anio, mes, dia);
        }
        private double anio2006(int cod_ter)
        {
            double calculo = 0;
            var r = retAnioMesDia(cod_ter);
            if (r.Item1 <= 2006)
            {
                int valorFijo = 1000000;
                int difAño = 2006 - r.Item1;
                int difMes = 12 - r.Item2;
                int difDia = 31 - r.Item3;

                calculo = (difAño * valorFijo) + ((difMes * valorFijo) / 12) + (((difDia * valorFijo) / 12) / 30);
                return (int)Math.Ceiling(calculo);
            }
            else return calculo;
        }
        private double anio2007(int cod_ter)
        {
            double calculo = 0;
            var r = retAnioMesDia(cod_ter);
            int valorFijo = 576000;
            if (r.Item1 <= 2006)
            {
                calculo = valorFijo;
                return calculo;
            }
            else if (r.Item1 == 2007)
            {

                int difMes = 12 - r.Item2;
                int difDia = 31 - r.Item3;

                calculo = ((difMes * valorFijo) / 12) + (((difDia * valorFijo) / 12) / 30);
                return (int)Math.Ceiling(calculo);
            }
            else return calculo;
        }

        //PLUS 
        double divAnioDias = 365.0 / 12.0;
        private double calculoPlus(int cod_ter, DateTime fechaFin, int plusVal)
        {
            
            DateTime fechaInicio;
            if (!DateTime.TryParse(fechadeCalculo(cod_ter.ToString()), out fechaInicio))
            {
                fechaInicio = DateTime.MinValue;
            }           
            TimeSpan diferencia = fechaFin.Subtract(fechaInicio);
            int diferenciaEnDias = diferencia.Days;

            int plusValor = 0;
            var r = retAnioMesDia(cod_ter);
            if (DateTime.Now.Year - r.Item1 > 5)
            {
                plusValor = plusVal;
            }
            double plus = (diferenciaEnDias / divAnioDias) * plusValor;
            return plus;
        }

        private (double, double, double) anio2017(int cod_ter)
        {
            double calculo = 0;
            var r = retAnioMesDia(cod_ter);
            int valorFijo = 2033763;
            if (r.Item1 < 2017)
            {
                double liquidacion = valorFijo;
                double plus = calculoPlus(cod_ter, new DateTime(2017, 12, 31), 4891);
                return (liquidacion, plus, liquidacion + plus);
                
            }
            else if (r.Item1 == 2017)
            {
                int difMes = 12 - r.Item2;
                int difDia = 31 - r.Item3;

                double liquidacion = ((difMes * valorFijo) / 12) + (((difDia * valorFijo) / 12) / 30);
                double plus = calculoPlus(cod_ter, new DateTime(2017, 12, 31), 4891);
                return (liquidacion, plus, liquidacion + plus);
            }
            else return (0,calculo, 0);
        }

    }
}
