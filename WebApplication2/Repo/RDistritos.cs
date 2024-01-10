using System.Data.SqlClient;
using WebApplication2.Models;

namespace WebApplication2.Repo
{
	public class RDistritos
	{
		private conexion _cn = new conexion();
		public List<MDistritos> listarTodos()
		{
			var Lista = new List<MDistritos>();

			using (var conexion = new SqlConnection(_cn.getCadenaCon()))
			{
				conexion.Open();
				SqlCommand cmd = new SqlCommand("select * from Distritos;", conexion);
				using (var reader = cmd.ExecuteReader())
				{
					while (reader.Read())
					{
						Lista.Add(new MDistritos()
						{
							COD_DIST = Convert.ToInt32(reader["COD_DIST"]),
							NOM_DIST = reader["NOM_DIST"].ToString(),
							DETALLE = reader["DETALLE"].ToString() ?? string.Empty,
							COMPUEST = reader["COMPUEST"].ToString() ?? string.Empty,
							COD_SUPERVISOR = reader["COD_SUPERVISOR"].ToString() ?? string.Empty
						});
					}
				}
				return Lista;
			}
		}
	}
}
