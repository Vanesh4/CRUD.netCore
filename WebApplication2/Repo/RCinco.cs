using System.Data;
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

				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						Lista.Add(new Cinco()
						{
							cedula = reader["Cedula"].ToString(),
							fecha = reader["Fecha"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha"]).ToString("dd-MM-yyyy") : (string)null,
							numComprobante = reader["NumComprob"].ToString(),
							valor = reader["VrDebitos"].ToString()
						});
					}
				}
				return Lista;
			}
			//using (var conexion = new SqlConnection(_cn.getCadenaConAPP()))
			//{
			//	conexion.Open();
			//	SqlCommand cmd = new SqlCommand($"SELECT * FROM Pastores WHERE CÉDULA = @cedula", conexion);
			//	cmd.Parameters.Add(new SqlParameter("@Cedula", SqlDbType.NVarChar) { Value = Lista[0].cedula });
			//	using (var reader = cmd.ExecuteReader())
			//	{
			//		while (reader.Read())
			//		{
			//			Lista.Add(new Cinco()
			//			{
			//				nombre = reader["NOMBRE"].ToString()
			//			});
			//		}
			//	}
			//	return Lista;
			//}
		}

		public List<Cinco> listarDatos(int cedula) {
			string consulta = "SELECT * FROM AportesPastor WHERE Cedula = @cedula ;";
			SqlParameter[] parametros = { new SqlParameter("@cedula", cedula) };
			return ObtenerDatos(consulta, parametros);
		}

	}
}
