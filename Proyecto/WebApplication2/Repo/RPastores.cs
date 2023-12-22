using System.Data.SqlClient;
using WebApplication2.Models;

namespace WebApplication2.Repo
{
	public class RPastores
	{
		private conexion _cn = new conexion();

		public List<MPastores> listarTodos()
		{
			var Lista = new List<MPastores>();

			using (var conexion = new SqlConnection(_cn.getCadenaCon()))
			{
				conexion.Open();
				SqlCommand cmd = new SqlCommand("select TOP 20 * from Pastores;", conexion);
				//SqlCommand cmd = new SqlCommand("select CÉDULA, NOMBRE from ListadoPastores;", conexion);

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

		public List<MPastores> FiltroCedula(int cedula)
		{
			var Lista = new List<MPastores>();

			using (var conexion = new SqlConnection(_cn.getCadenaCon()))
			{
				conexion.Open();
				SqlCommand cmd = new SqlCommand($"select * from Pastores where CÉDULA = @Cedula;", conexion);
				cmd.Parameters.AddWithValue("@Cedula", cedula);
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

		public List<MPastores> FiltroDistrito(string dis)
		{
			var Lista = new List<MPastores>();

			using (var conexion = new SqlConnection(_cn.getCadenaCon()))
			{
				conexion.Open();
				SqlCommand cmd = new SqlCommand($"select * from Pastores where DISTRITO = {dis};", conexion);

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

		public List<MPastores> FiltroNombre(string nombre)
		{
			var Lista = new List<MPastores>();

			using (var conexion = new SqlConnection(_cn.getCadenaCon()))
			{
				conexion.Open();
				SqlCommand cmd = new SqlCommand($"SELECT * FROM Pastores WHERE NOMBRE like '%{nombre}%';", conexion);

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

		public bool Update(MPastores datosAct)
		{
			bool rpta = false;
			try
			{

				using (var conexion = new SqlConnection(_cn.getCadenaCon()))
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
