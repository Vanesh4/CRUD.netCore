using Microsoft.AspNetCore.Components.Routing;
using System.Collections;
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
                        var liq2017 = anio2017(codTer);
                        var liq2018 = anio2018(codTer);
                        var liq2019 = anio2019(codTer);
                        var liq2020 = anio2020(codTer);
                        var liq2021 = anio2021(codTer);
                        var liq2022 = anio2022(codTer);
                        var liq2023 = anio2023(codTer);
                        Lista.Add(new MRetirosListado()
                        {
                            //codTer = Convert.ToInt32(reader["CÉDULA"]), este es con la vista
                            codTer = codTer,
                            fechaParaCalculo = reader["fecha_para_calculo"] != DBNull.Value ? Convert.ToDateTime(reader["fecha_para_calculo"]).ToString("dd-MM-yyyy") : (string)null,
                            
                            liquidacion2006 = validarDato(anio2006(codTer).ToString()),
                            liquidacion2007 = validarDato(anio2007(codTer).ToString()),
                            liquidacion2008 = validarDato(anio2008(codTer).ToString()),
                            liquidacion2009 = validarDato(anio2009(codTer).ToString()),
                            liquidacion2010 = validarDato(anio2010(codTer).ToString()),
                            liquidacion2011 = validarDato(anio2011(codTer).ToString()),
                            liquidacion2012 = validarDato(anio2012(codTer).ToString()),
                            liquidacion2013 = validarDato(anio2013(codTer).ToString()),
                            liquidacion2014 = validarDato(anio2014(codTer).ToString()),
                            liquidacion2015 = validarDato(anio2015(codTer).ToString()),
                            liquidacion2016 = validarDato(anio2016(codTer).ToString()),
                            
                            liquidacion2017 = new List<string> { validarDato(liq2017.Item1.ToString()), validarDato(liq2017.Item2.ToString()), validarDato(liq2017.Item3.ToString()) },
                            liquidacion2018 = new List<string> { validarDato(liq2018.Item1.ToString()), validarDato(liq2018.Item2.ToString()), validarDato(liq2018.Item3.ToString()) },
                            liquidacion2019 = new List<string> { validarDato(liq2019.Item1.ToString()), validarDato(liq2019.Item2.ToString()), validarDato(liq2019.Item3.ToString()) },
                            liquidacion2020 = new List<string> { validarDato(liq2020.Item1.ToString()), validarDato(liq2020.Item2.ToString()), validarDato(liq2020.Item3.ToString()) },
                            liquidacion2021 = new List<string> { validarDato(liq2021.Item1.ToString()), validarDato(liq2021.Item2.ToString()), validarDato(liq2021.Item3.ToString()) },
                            liquidacion2022 = new List<string> { validarDato(liq2022.Item1.ToString()), validarDato(liq2022.Item2.ToString()), validarDato(liq2022.Item3.ToString()) },
                            liquidacion2023 = new List<string> { validarDato(liq2023.Item1.ToString()), validarDato(liq2023.Item2.ToString()), validarDato(liq2023.Item3.ToString()) },

                            totalLiquidaciones = sumaTotalLiquidaciones(codTer),

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
                    SqlCommand cmd = new SqlCommand("SELECT fecha_para_calculo FROM RetirosNet WHERE COD_TER=@codTer;", conexion);
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
                return null;
            }
            else
            {
                decimal valorDecimal;
                if (decimal.TryParse(liquidaciona, out valorDecimal))
                {
                    return valorDecimal.ToString("#,0", System.Globalization.CultureInfo.InvariantCulture);
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

                    SqlCommand cmdInsert = new SqlCommand("INSERT INTO RegAuditoriaRetiros (COD_TER, fecha_para_calculo, fechaActualizacion, usuario, ObservacionActualizacion) VALUES (@codTer, @fechaParaCalculo, @fechaActualizacion, @usuario, @observacionActualizacion);", conexion);
                    cmdInsert.Parameters.AddWithValue("@codTer", datosVer.codTer);
                    cmdInsert.Parameters.AddWithValue("@fechaParaCalculo", datosVer.fechaParaCalculo);
                    cmdInsert.Parameters.AddWithValue("@fechaActualizacion", DateTime.Now);
                    cmdInsert.Parameters.AddWithValue("@usuario", datosVer.verficacionUsuario);
                    cmdInsert.Parameters.AddWithValue("@observacionActualizacion", datosVer.ObservacionActualizacion);
                    cmdInsert.ExecuteNonQuery();
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
            if (r.Item1 < 2007)
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
        private double anio2008(int cod_ter)
        {
            double calculo = 0;
            var r = retAnioMesDia(cod_ter);
            int valorFijo = 670472;
            if (r.Item1 < 2008)
            {
                calculo = valorFijo;
                return calculo;
            }
            else if (r.Item1 == 2008)
            {
                int difMes = 12 - r.Item2;
                int difDia = 31 - r.Item3;

                calculo = ((difMes * valorFijo) / 12) + (((difDia * valorFijo) / 12) / 30);
                return (int)Math.Ceiling(calculo);
            }
            else return calculo;
        }
        private double anio2009(int cod_ter)
        {
            double calculo = 0;
            var r = retAnioMesDia(cod_ter);
            int valorFijo = 780350;
            if (r.Item1 < 2009)
            {
                calculo = valorFijo;
                return calculo;
            }
            else if (r.Item1 == 2009)
            {
                int difMes = 12 - r.Item2;
                int difDia = 31 - r.Item3;

                calculo = ((difMes * valorFijo) / 12) + (((difDia * valorFijo) / 12) / 30);
                return (int)Math.Ceiling(calculo);
            }
            else return calculo;
        }
        private double anio2010(int cod_ter)
        {
            double calculo = 0;
            var r = retAnioMesDia(cod_ter);
            int valorFijo = 823000;
            if (r.Item1 < 2010)
            {
                calculo = valorFijo;
                return calculo;
            }
            else if (r.Item1 == 2010)
            {
                int difMes = 12 - r.Item2;
                int difDia = 31 - r.Item3;

                calculo = ((difMes * valorFijo) / 12) + (((difDia * valorFijo) / 12) / 30);
                return (int)Math.Ceiling(calculo);
            }
            else return calculo;
        }
        private double anio2011(int cod_ter)
        {
            double calculo = 0;
            var r = retAnioMesDia(cod_ter);
            int valorFijo = 945000;
            if (r.Item1 < 2011)
            {
                calculo = valorFijo;
                return calculo;
            }
            else if (r.Item1 == 2011)
            {
                int difMes = 12 - r.Item2;
                int difDia = 31 - r.Item3;

                calculo = ((difMes * valorFijo) / 12) + (((difDia * valorFijo) / 12) / 30);
                return (int)Math.Ceiling(calculo);
            }
            else return calculo;
        }
        private double anio2012(int cod_ter)
        {
            double calculo = 0;
            var r = retAnioMesDia(cod_ter);
            int valorFijo = 1000000;
            if (r.Item1 < 2012)
            {
                calculo = valorFijo;
                return calculo;
            }
            else if (r.Item1 == 2012)
            {
                int difMes = 12 - r.Item2;
                int difDia = 31 - r.Item3;

                calculo = ((difMes * valorFijo) / 12) + (((difDia * valorFijo) / 12) / 30);
                return (int)Math.Ceiling(calculo);
            }
            else return calculo;
        }
        private double anio2013(int cod_ter)
        {
            double calculo = 0;
            var r = retAnioMesDia(cod_ter);
            int valorFijo = 1090000;
            if (r.Item1 < 2013)
            {
                calculo = valorFijo;
                return calculo;
            }
            else if (r.Item1 == 2013)
            {
                int difMes = 12 - r.Item2;
                int difDia = 31 - r.Item3;

                calculo = ((difMes * valorFijo) / 12) + (((difDia * valorFijo) / 12) / 30);
                return (int)Math.Ceiling(calculo);
            }
            else return calculo;
        }
        private double anio2014(int cod_ter)
        {
            double calculo = 0;
            var r = retAnioMesDia(cod_ter);
            int valorFijo = 1263000;
            if (r.Item1 < 2014)
            {
                calculo = valorFijo;
                return calculo;
            }
            else if (r.Item1 == 2014)
            {
                int difMes = 12 - r.Item2;
                int difDia = 31 - r.Item3;

                calculo = ((difMes * valorFijo) / 12) + (((difDia * valorFijo) / 12) / 30);
                return (int)Math.Ceiling(calculo);
            }
            else return calculo;
        }
        private double anio2015(int cod_ter)
        {
            double calculo = 0;
            var r = retAnioMesDia(cod_ter);
            int valorFijo = 1336600;
            if (r.Item1 < 2015)
            {
                calculo = valorFijo;
                return calculo;
            }
            else if (r.Item1 == 2015)
            {
                int difMes = 12 - r.Item2;
                int difDia = 31 - r.Item3;

                calculo = ((difMes * valorFijo) / 12) + (((difDia * valorFijo) / 12) / 30);
                return (int)Math.Ceiling(calculo);
            }
            else return calculo;
        }
        private double anio2016(int cod_ter)
        {
            double calculo = 0;
            var r = retAnioMesDia(cod_ter);
            int valorFijo = 1336600;
            if (r.Item1 < 2016)
            {
                calculo = valorFijo;
                return calculo;
            }
            else if (r.Item1 == 2016)
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
                double total = liquidacion + plus;
                return (liquidacion, (int)Math.Ceiling(plus), total);
                
            }
            else if (r.Item1 == 2017)
            {
                int difMes = 12 - r.Item2;
                int difDia = 31 - r.Item3;

                double liquidacion = ((difMes * valorFijo) / 12) + (((difDia * valorFijo) / 12) / 30);
                double plus = calculoPlus(cod_ter, new DateTime(2017, 12, 31), 4891);
                double total = (liquidacion + plus);
                return (liquidacion, plus, total);
            }
            else return (0,calculo, 0);
        }

        private (double, double, double) anio2018(int cod_ter)
        {
            double calculo = 0;
            var r = retAnioMesDia(cod_ter);
            int valorFijo = 2064174;
            if (r.Item1 < 2018)
            {
                double liquidacion = valorFijo;
                double plus = calculoPlus(cod_ter, new DateTime(2018, 12, 31), 4898);
                double total = liquidacion + plus;
                return (liquidacion, plus, total);
            }
            else if (r.Item1 == 2018)
            {
                int difMes = 12 - r.Item2;
                int difDia = 31 - r.Item3;

                double liquidacion = ((difMes * valorFijo) / 12) + (((difDia * valorFijo) / 12) / 30);
                double plus = calculoPlus(cod_ter, new DateTime(2018, 12, 31), 4898);
                double total = (liquidacion + plus);
                return ((int)Math.Ceiling(liquidacion), (int)Math.Ceiling(plus), total);
            }
            else return (0, calculo, 0);
        }

        private (double, double, double) anio2019(int cod_ter)
        {
            double calculo = 0;
            var r = retAnioMesDia(cod_ter);
            int valorFijo = 2209842;
            if (r.Item1 < 2019)
            {
                double liquidacion = valorFijo;
                double plus = calculoPlus(cod_ter, new DateTime(2019, 12, 31), 4997);
                double total = liquidacion + plus;
                return (liquidacion, plus, total);
            }
            else if (r.Item1 == 2019)
            {
                int difMes = 12 - r.Item2;
                int difDia = 31 - r.Item3;

                double liquidacion = ((difMes * valorFijo) / 12) + (((difDia * valorFijo) / 12) / 30);
                double plus = calculoPlus(cod_ter, new DateTime(2019, 12, 31), 4997);
                double total = (liquidacion + plus);
                return ((int)Math.Ceiling(liquidacion), (int)Math.Ceiling(plus), total);
            }
            else return (0, calculo, 0);
        }
        private (double, double, double) anio2020(int cod_ter)
        {
            double calculo = 0;
            var r = retAnioMesDia(cod_ter);
            int valorFijo = 2103661;
            if (r.Item1 < 2020)
            {
                double liquidacion = valorFijo;
                double plus = calculoPlus(cod_ter, new DateTime(2020, 12, 31), 4601);
                double total = liquidacion + plus;
                return (liquidacion, plus, total);
            }
            else if (r.Item1 == 2020)
            {
                int difMes = 12 - r.Item2;
                int difDia = 31 - r.Item3;

                double liquidacion = ((difMes * valorFijo) / 12) + (((difDia * valorFijo) / 12) / 30);
                double plus = calculoPlus(cod_ter, new DateTime(2020, 12, 31), 4601);
                double total = (liquidacion + plus);
                return ((int)Math.Ceiling(liquidacion), (int)Math.Ceiling(plus), total);
            }
            else return (0, calculo, 0);
        }
        private (double, double, double) anio2021(int cod_ter)
        {
            double calculo = 0;
            var r = retAnioMesDia(cod_ter);
            int valorFijo = 2462064;
            if (r.Item1 < 2021)
            {
                double liquidacion = valorFijo;
                double plus = calculoPlus(cod_ter, new DateTime(2021, 12, 31), 5531);
                double total = liquidacion + plus;
                return (liquidacion, plus, total);
            }
            else if (r.Item1 == 2021)
            {
                int difMes = 12 - r.Item2;
                int difDia = 31 - r.Item3;

                double liquidacion = ((difMes * valorFijo) / 12) + (((difDia * valorFijo) / 12) / 30);
                double plus = calculoPlus(cod_ter, new DateTime(2021, 12, 31), 5531);
                double total = (liquidacion + plus);
                return ((int)Math.Ceiling(liquidacion), (int)Math.Ceiling(plus), total);
            }
            else return (0, calculo, 0);
        }
        private (double, double, double) anio2022(int cod_ter)
        {
            double calculo = 0;
            var r = retAnioMesDia(cod_ter);
            int valorFijo = 3243572;
            if (r.Item1 < 2022)
            {
                double liquidacion = valorFijo;
                double plus = calculoPlus(cod_ter, new DateTime(2022, 12, 31), 7209);
                double total = liquidacion + plus;
                return (liquidacion, plus, total);
            }
            else if (r.Item1 == 2022)
            {
                int difMes = 12 - r.Item2;
                int difDia = 31 - r.Item3;

                double liquidacion = ((difMes * valorFijo) / 12) + (((difDia * valorFijo) / 12) / 30);
                double plus = calculoPlus(cod_ter, new DateTime(2002, 12, 31), 7209);
                double total = (liquidacion + plus);
                return ((int)Math.Ceiling(liquidacion), (int)Math.Ceiling(plus), total);
            }
            else return (0, calculo, 0);
        }
        private (double, double, double) anio2023(int cod_ter)
        {
            double calculo = 0;
            var r = retAnioMesDia(cod_ter);
            int valorFijo = 3243572;
            if (r.Item1 < 2023)
            {
                double liquidacion = valorFijo;
                double plus = calculoPlus(cod_ter, new DateTime(2023, 12, 31), 7209);
                double total = liquidacion + plus;
                return (liquidacion, plus, total);
            }
            else if (r.Item1 == 2023)
            {
                int difMes = 12 - r.Item2;
                int difDia = 31 - r.Item3;

                double liquidacion = ((difMes * valorFijo) / 12) + (((difDia * valorFijo) / 12) / 30);
                double plus = calculoPlus(cod_ter, new DateTime(2023, 12, 31), 7209);
                double total = (liquidacion + plus);
                return ((int)Math.Ceiling(liquidacion), (int)Math.Ceiling(plus), total);
            }
            else return (0, calculo, 0);
        }

        private string sumaTotalLiquidaciones(int codTer)
        {
            double suma = anio2006(codTer) + anio2007(codTer) + anio2008(codTer) + anio2009(codTer) + anio2010(codTer) + anio2011(codTer) + anio2012(codTer) + anio2013(codTer) + anio2014(codTer) + anio2015(codTer) + anio2016(codTer) + anio2017(codTer).Item3 + anio2018(codTer).Item3 + anio2019(codTer).Item3 + anio2020(codTer).Item3 + anio2021(codTer).Item3 + anio2022(codTer).Item3 + anio2022(codTer).Item3;
            return validarDato(suma.ToString());
        }

    }
}
