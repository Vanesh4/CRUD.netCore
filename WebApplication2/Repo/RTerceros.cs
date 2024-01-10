using System.Data.SqlClient;
using WebApplication2.Models;

namespace WebApplication2.Repo
{
	public class RTerceros
	{
		private conexion _cn = new conexion();

		public List<MTerceros> listar20()
		{
			var Lista = new List<MTerceros>();
			using (var conexion = new SqlConnection(_cn.getCadenaCon()))
			{
				conexion.Open();
				SqlCommand cmd = new SqlCommand("SELECT TOP 20 * FROM Terceros;", conexion);

				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						Lista.Add(new MTerceros()
						{
							COD_TER = reader["COD_TER"].ToString(),
							NOM_TER = reader["NOM_TER"].ToString(),
							DIR1 = reader["DIR1"].ToString(),
							EMAIL = reader["EMAIL"].ToString(),
							FEC_ING = reader["FEC_ING"] != DBNull.Value ? Convert.ToDateTime(reader["FEC_ING"]).ToString("dd-MM-yyyy") : (string)null
						});
					}
				}
				return Lista;
			}
		}

		public List<MTerceros> listarTodos()
		{
			var Lista = new List<MTerceros>();
			using (var conexion = new SqlConnection(_cn.getCadenaCon()))
			{
				conexion.Open();
				SqlCommand cmd = new SqlCommand("SELECT * FROM Terceros;", conexion);

				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						Lista.Add(new MTerceros()
						{
							COD_TER = reader["COD_TER"].ToString(),
							NOM_TER = reader["NOM_TER"].ToString(),
							DIR1 = reader["DIR1"].ToString(),
							EMAIL = reader["EMAIL"].ToString(),
							FEC_ING = reader["FEC_ING"] != DBNull.Value ? Convert.ToDateTime(reader["FEC_ING"]).ToString("dd-MM-yyyy") : (string)null
						});
					}
				}
				return Lista;
			}
		}
	}
}
