﻿using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.Metrics;
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
							codTer = Convert.ToInt32(reader["CÉDULA"]),
							fechaParaCalculo = reader["fecha_para_calculo"] != DBNull.Value ? Convert.ToDateTime(reader["fecha_para_calculo"]).ToString("dd-MM-yyyy") : (string)null,
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
						});
                    }
                }
                return Lista;
            }
        }

        public List<MRetirosListado> listarTodos()
        {
            return ObtenerDatos("SELECT TOP 1000 * FROM RetirosVista;", null);
        }
        public List<MRetirosListado> FiltroCedula(int cedula)
        {
            string consulta = "SELECT * FROM RetirosVista WHERE COD_TER = @Cedula;";
            SqlParameter[] parametros = { new SqlParameter("@Cedula", cedula) };
            return ObtenerDatos(consulta, parametros);
        }

        public string validarDato(string liquidaciona)
        {
			if (liquidaciona == "0")
			{
				return "";
			}
            else
            {
                return liquidaciona;
            }
		}

		public bool Update(MRetirosListado datosVer)
		{
			bool res = false;
			try
			{
				using (var conexion = new SqlConnection(_cn.getCadenaConAPP()))
				{
					conexion.Open();               
                    SqlCommand cmd = new SqlCommand("UPDATE Retiros SET VerificadoFecha=GETDATE(),Verificacion=1,VerificadoUsuario=@user, fecha_para_calculo = @fechaCalculo WHERE COD_TER = @codTer;", conexion);
					cmd.Parameters.AddWithValue("user", datosVer.verficacionUsuario);
					cmd.Parameters.AddWithValue("codTer", datosVer.codTer);
                    cmd.Parameters.AddWithValue("fechaCalculo", datosVer.fechaParaCalculo);
					cmd.ExecuteNonQuery();
				}
				res = true;
			}
			catch (Exception e)
			{
				string error = e.Message;
			}
			return res;
		}

	}
}
