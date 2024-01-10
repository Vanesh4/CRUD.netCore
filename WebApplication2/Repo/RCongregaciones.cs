using System.Data.SqlClient;
using WebApplication2.Models;

namespace WebApplication2.Repo
{
	public class RCongregaciones
	{
		private conexion _cn = new conexion();
		public List<MCongregaciones> listar20()
		{
			var Lista = new List<MCongregaciones>();

			using (var conexion = new SqlConnection(_cn.getCadenaCon()))
			{
				conexion.Open();
				SqlCommand cmd = new SqlCommand("select TOP 20 * from Congregaciones;", conexion);

				using (var reader = cmd.ExecuteReader())
				{

					while (reader.Read())
					{
						Lista.Add(new MCongregaciones()
						{
							Código = Convert.ToInt32(reader["Código"]),
							NombreTemplo = reader["NombreTemplo"].ToString() ?? string.Empty,
							Estado = reader["Estado"].ToString() ?? string.Empty,
							Distrito = reader["Distrito"].ToString() ?? string.Empty,
							CedulaPastor = reader["CedulaPastor"].ToString() ?? string.Empty,
							Municipio = reader["Municipio"].ToString() ?? string.Empty,
							Dirección = reader["Dirección"].ToString() ?? string.Empty,
							FechaApertura = reader["Fecha Apertura"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha Apertura"]).ToString("dd-MM-yyyy") : (string)null,
						}) ;
					}
				}
				return Lista;
			}
		}

		public List<MCongregaciones> listarTodo()
		{
			var Lista = new List<MCongregaciones>();

			using (var conexion = new SqlConnection(_cn.getCadenaCon()))
			{
				conexion.Open();
				SqlCommand cmd = new SqlCommand("select * from Congregaciones;", conexion);

				using (var reader = cmd.ExecuteReader())
				{

					while (reader.Read())
					{
						Lista.Add(new MCongregaciones()
						{
							Código = Convert.ToInt32(reader["Código"]),
							NombreTemplo = reader["NombreTemplo"].ToString() ?? string.Empty,
							Estado = reader["Estado"].ToString() ?? string.Empty,
							Distrito = reader["Distrito"].ToString() ?? string.Empty,
							CedulaPastor = reader["CedulaPastor"].ToString() ?? string.Empty,
						});
					}
				}
				return Lista;
			}
		}
	}
}
