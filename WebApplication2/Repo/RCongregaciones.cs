using System.Data.SqlClient;
using WebApplication2.Models;

namespace WebApplication2.Repo
{
	public class RCongregaciones
	{
		private conexion _cn = new conexion();

		public List<MCongregaciones> listarTodo()
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
						});
					}
				}
				return Lista;
			}
		}
	}
}
