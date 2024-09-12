using System.Data.SqlClient;
using WebApplication2.Models;

namespace WebApplication2.Repo
{
    public class RTerceros
	{
		private conexion _cn = new conexion();
		private List<MTerceros> ObtenerDatos(string consulta, SqlParameter[] parametro)
		{
			var Lista = new List<MTerceros>();
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
						Lista.Add(new MTerceros()
						{
							COD_TER = reader["COD_TER"].ToString(),
							NOM_TER = reader["NOM_TER"].ToString(),
							//DIR1 = reader["DIR1"].ToString(),
							//EMAIL = reader["EMAIL"].ToString(),
							//CIUDAD = reader["CIUDAD"].ToString(),
							FEC_ING = reader["FEC_ING"] != DBNull.Value ? Convert.ToDateTime(reader["FEC_ING"]).ToString("dd-MM-yyyy") : (string)null,
                            FEC_APORT = reader["FEC_APORT"] != DBNull.Value ? Convert.ToDateTime(reader["FEC_APORT"]).ToString("dd-MM-yyyy") : (string)null,
                            FEC_MINIS = reader["FEC_MINIS"] != DBNull.Value ? Convert.ToDateTime(reader["FEC_MINIS"]).ToString("dd-MM-yyyy") : (string)null,
							IdRow = reader["idRow"].ToString()
                        });
					}
				}
				return Lista;
			}
		}

		public List<MTerceros> listar20()
		{
			return ObtenerDatos("SELECT TOP 20 * FROM Terceros;", null);
		}

		public List<MTerceros> listarTodos()
		{
			return ObtenerDatos("SELECT * FROM Terceros;", null);

		}
		public List<MTerceros> FiltroId(string codTer)
		{
			string consulta = "SELECT * FROM Terceros WHERE COD_TER = @codTer;";
			SqlParameter[] parametros = { new SqlParameter("@codTer", codTer) };
			return ObtenerDatos(consulta, parametros);
		}

		public List<MTerceros> FiltroPorNomTer(string nomTer)
		{
            string[] nombrebusqueda = nomTer.Split(' ');
            // Agregar '%' entre las palabras
            string parametroNombre = string.Join("%", nombrebusqueda);

            string con = "SELECT * FROM Terceros WHERE NOM_TER LIKE '%' + @nomTer + '%';";
			SqlParameter[] parametros = { new SqlParameter("@nomTer", parametroNombre) };
			return ObtenerDatos(con, parametros);
		}

        public bool Update(MPastores datosAct)
        {
            try
            {
                using (var conexion = new SqlConnection(_cn.getCadenaConAPP()))
                {
                    conexion.Open();                    
                    SqlCommand cmd = new SqlCommand("UPDATE Pastores SET NOMBRE=@nom,EMAIL=@email,CONTACTO=@con WHERE CÉDULA = @id", conexion);
                    cmd.Parameters.AddWithValue("id", datosAct.cedula);
                    cmd.Parameters.AddWithValue("nom", datosAct.nombre);
                    cmd.Parameters.AddWithValue("email", datosAct.email);
                    cmd.Parameters.AddWithValue("con", datosAct.contacto);
                    cmd.ExecuteNonQuery();

                }
                return true;

            }
            catch (Exception e)
            {
                string error = e.Message;
                return false;
            }            
        }
        public bool Delete(int idRow)
        {
            try
            {
                using (var conexion = new SqlConnection(_cn.getCadenaConAPP()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Terceros where idRow= @id", conexion);
                    cmd.Parameters.AddWithValue("id", idRow);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception e)
            {
                string error = e.Message;
				return false;
            }            
        }
    }
}
