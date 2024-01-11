using System.Data.SqlClient;
using WebApplication2.Models;

namespace WebApplication2.Repo
{
	public class RCongregaciones
	{
		private conexion _cn = new conexion();

		private List<MCongregaciones> ObtenerDatos(string consulta, SqlParameter[] parametro)
		{
			var Lista = new List<MCongregaciones>();

			using (var conexion = new SqlConnection(_cn.getCadenaCon()))
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
						});
					}
				}
				return Lista;
			}
		}

		public List<MCongregaciones> listar20()
		{
			return ObtenerDatos("select TOP 20 * from Congregaciones;", null);
		}

		public List<MCongregaciones> listarTodo()
		{
			return ObtenerDatos("SELECT * FROM Congregaciones", null);
		}


		public List<MCongregaciones> filtroCedulaPastor(int cedula)
		{
			string consulta = "SELECT * FROM Congregaciones WHERE CedulaPastor = @Cedula;";
			SqlParameter[] parametros = { new SqlParameter("@Cedula", cedula) };
			return ObtenerDatos(consulta, parametros);
		}
		public List<MCongregaciones> filtroNomCongre(string nombre)
		{
			string consulta = "SELECT * FROM Congregaciones WHERE NombreTemplo LIKE '%' + @nombre + '%';";
			SqlParameter[] parametros = { new SqlParameter("@nombre", nombre) };
			return ObtenerDatos(consulta, parametros);
		}

		public List<MCongregaciones> filtroNombrePastor(string nombre)
		{
			string consulta = $"SELECT Congregaciones.*, Pastores.Nombre AS NombrePastor FROM Congregaciones JOIN Pastores ON Congregaciones.CedulaPastor = Pastores.CÉDULA WHERE Pastores.NOMBRE like '%{nombre}%';";
			return ObtenerDatos(consulta, null);
		}

	}
}
