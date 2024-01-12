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
		}

		public List<Cinco> listarDatos(string nombre) {
			string consulta = "SELECT * FROM Pastores WHERE Observacion LIKE '%' + @nombre + '%';";
			SqlParameter[] parametros = { new SqlParameter("@nombre", nombre) };
			return ObtenerDatos(consulta, parametros);
		}

	}
}
