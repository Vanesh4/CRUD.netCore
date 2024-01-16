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
							valor = reader["VrCreditos"].ToString(),
							nombre = ObtenerNombre(reader["Cedula"].ToString())
						}) ;
					}
				}
				//return Lista;
			}
			
            return Lista;
        }
		private string ObtenerNombre(string cedula)
		{
			string nombrePastor = "";
            using (var conexionAPP = new SqlConnection(_cn.getCadenaConAPP()))
            {
                conexionAPP.Open();
                SqlCommand cmdNOM = new SqlCommand($"SELECT NOMBRE FROM Pastores WHERE CÉDULA = @cedula", conexionAPP);
                SqlParameter parametro = new SqlParameter("@cedula", cedula);
                cmdNOM.Parameters.Add(parametro);
                using (var readerApp = cmdNOM.ExecuteReader())
                {
                    if(readerApp.Read())
            {
                        nombrePastor = readerApp["NOMBRE"].ToString();
                    }

                }
            }
			return nombrePastor;
		}

		public List<Cinco> listarDatos(string cedula) {
			string consulta = "SELECT * FROM AportesPastor WHERE Cedula = @cedula ;";
			SqlParameter[] parametros = { new SqlParameter("@cedula", cedula) };
			return ObtenerDatos(consulta, parametros);
		}

	}
}
