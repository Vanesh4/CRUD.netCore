using System.Data.SqlClient;
using WebApplication2.Models;

namespace WebApplication2.Repo
{
	public class RTerceros
	{
		private conexion _cn = new conexion();
		private List<MTerceros> ObtenerDatos(string consulta, SqlParameter[] parametro)
		{
			var Lista = new List<MTerceros>();
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
						Lista.Add(new MTerceros()
						{
							COD_TER = reader["COD_TER"].ToString(),
							NOM_TER = reader["NOM_TER"].ToString(),
							DIR1 = reader["DIR1"].ToString(),
							CIUDAD = reader["NOM1"].ToString(),
							EMAIL = reader["EMAIL"].ToString(),
							FEC_ING = reader["FEC_ING"] != DBNull.Value ? Convert.ToDateTime(reader["FEC_ING"]).ToString("dd-MM-yyyy") : (string)null
						});
					}
				}
				return Lista;
			}
		}

		public List<MTerceros> listar20()
		{
			return ObtenerDatos("SELECT TOP 20 * FROM Terceros;", null);
		}

		public List<MTerceros> listarTodos()
		{
			return ObtenerDatos("SELECT * FROM Terceros;", null);

		}
		public List<MTerceros> FiltroId(string codTer)
		{
			string consulta = "SELECT * FROM Terceros WHERE COD_TER = @codTer;";
			SqlParameter[] parametros = { new SqlParameter("@codTer", codTer) };
			return ObtenerDatos(consulta, parametros);
		}

		public List<MTerceros> FiltroPorNomTer(string nomTer)
		{
			string con = "SELECT * FROM Terceros WHERE NOM_TER LIKE '%' + @nomTer + '%';";
			SqlParameter[] parametros = { new SqlParameter("@nomTer", nomTer) };
			return ObtenerDatos(con, parametros);

		}
	}
}
