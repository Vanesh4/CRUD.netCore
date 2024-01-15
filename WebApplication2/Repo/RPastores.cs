using System.Data.SqlClient;
using WebApplication2.Models;

namespace WebApplication2.Repo
{
	public class RPastores
	{
		private conexion _cn = new conexion();

		private List<MPastores> ObtenerDatos(string consulta, SqlParameter[] parametro)
		{
			var Lista = new List<MPastores>();

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
						Lista.Add(new MPastores()
						{
							cedula = Convert.ToInt32(reader["CÉDULA"]),
							nombre = reader["NOMBRE"].ToString() ?? string.Empty,
							contacto = reader["CONTACTO"].ToString() ?? string.Empty,
							email = reader["EMAIL"].ToString() ?? string.Empty,
							direccion = reader["DIRECCIÓN"].ToString() ?? string.Empty,
							distrito = reader["DISTRITO"].ToString(),
							fechaNacimiento = reader["FEC_NACIMIENTO"] != DBNull.Value ? Convert.ToDateTime(reader["FEC_NACIMIENTO"]).ToString("dd-MM-yyyy") : (string)null,
							nombreCongregacion = reader["NOMBRE_CONGREGACIÓN"].ToString() ?? string.Empty,
							estadoPastor = reader["ESTADO _PASTOR"].ToString() ?? string.Empty,
							clasePastor = reader["CLASE_PASTOR"].ToString() ?? string.Empty
						});
					}
				}
				return Lista;
			}
		}

		public List<MPastores> listarTodos()
		{
			return ObtenerDatos("select * from Pastores;", null);
		}

		public List<MPastores> listar20()
		{
			return ObtenerDatos("select TOP 20 * from Pastores;", null);
		}

		public List<MPastores> FiltroCedula(int cedula)
		{
			string consulta = "SELECT * FROM Pastores WHERE CÉDULA = @Cedula;";
			SqlParameter[] parametros = { new SqlParameter("@Cedula", cedula) };
			return ObtenerDatos(consulta, parametros);
		}

		//select con los distritos
		public List<MPastores> FiltroDistrito(string dis)
		{
			return ObtenerDatos("select * from Pastores where DISTRITO = {dis};",null);

		}

		public List<MPastores> FiltroNombre(string nombre)
		{
			string consulta = "SELECT * FROM Pastores WHERE NOMBRE LIKE '%' + @nombre + '%';";
			SqlParameter[] parametros = { new SqlParameter("@nombre", nombre) };
			return ObtenerDatos(consulta, parametros);
		}

		public bool Update(MPastores datosAct)
		{
			bool rpta = false;
			try
			{
				using (var conexion = new SqlConnection(_cn.getCadenaConAPP()))
				{
					conexion.Open();
					//SqlCommand cmd = new SqlCommand($"UPDATE ListadoPastores SET NOMBRE='{datosAct.nombre}' WHERE CÉDULA = {datosAct.cedula}", conexion);
					SqlCommand cmd = new SqlCommand("UPDATE Pastores SET NOMBRE=@nom,EMAIL=@email,CONTACTO=@con WHERE CÉDULA = @id", conexion);
					cmd.Parameters.AddWithValue("id", datosAct.cedula);
					cmd.Parameters.AddWithValue("nom", datosAct.nombre);
					cmd.Parameters.AddWithValue("email", datosAct.email);
					cmd.Parameters.AddWithValue("con", datosAct.contacto);
					cmd.ExecuteNonQuery();

				}
				rpta = true;

			}
			catch (Exception e)
			{
				string error = e.Message;
			}
			return rpta;
		}
	}
}
