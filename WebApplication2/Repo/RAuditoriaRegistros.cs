using WebApplication2.Models;
using System.Data.SqlClient;

namespace WebApplication2.Repo
{
    public class RAuditoriaRegistros
    {
        private conexion _cn = new conexion();
        private List<MAuditoriaRegistros> ObtenerDatos(string consulta)
        {
            var Lista = new List<MAuditoriaRegistros>();

            using (var conexion = new SqlConnection(_cn.getCadenaConAPP()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand(consulta, conexion);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Lista.Add(new MAuditoriaRegistros()
                        {
                            codTer = reader["COD_TER"].ToString() ?? string.Empty,
                            fechaActualizacion = reader["fechaActualizacion"].ToString() ?? string.Empty,
                            usuario = reader["usuario"].ToString() ?? string.Empty,
                            observacion = reader["ObservacionActualizacion"].ToString() ?? string.Empty,
                        });
                    }
                }
                return Lista;
            }
        }

        public List<MAuditoriaRegistros> RegistrosAuditoria()
        {
            return ObtenerDatos("SELECT TOP 20 * FROM RegAuditoriaRetiros ORDER BY fechaActualizacion DESC;");
        }
    }
}
