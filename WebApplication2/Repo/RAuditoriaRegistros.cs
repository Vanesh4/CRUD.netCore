using WebApplication2.Models;
using System.Data.SqlClient;

namespace WebApplication2.Repo
{
    public class RAuditoriaRegistros
    {
        private conexion _cn = new conexion();
        private List<MAuditoriaRegistros> ObtenerDatos(string consulta, SqlParameter[] parametro)
        {
            var Lista = new List<MAuditoriaRegistros>();
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
            return ObtenerDatos("SELECT TOP 20 * FROM RegAuditoriaRetiros ORDER BY fechaActualizacion DESC;", null);
        }

        public List<MAuditoriaRegistros> MonitoriaFiltros(string fecha, string usuario, int opc)
        {
            if (opc == 1) {
                string con = "SELECT * FROM RegAuditoriaRetiros WHERE CAST(fechaActualizacion AS DATE) = @fecha ORDER BY fechaActualizacion DESC;";
                SqlParameter[] parametros = { new SqlParameter("@fecha", fecha) };
                return ObtenerDatos(con, parametros);
            }
            else if (opc == 2) {
                string con = "SELECT * FROM RegAuditoriaRetiros WHERE usuario = @user;";
                SqlParameter[] parametros = { new SqlParameter("@user", usuario) };
                return ObtenerDatos(con, parametros);
            }
            else
            {
                string con = "SELECT * FROM RegAuditoriaRetiros WHERE usuario = @user and CAST(fechaActualizacion AS DATE) = @fecha ORDER BY fechaActualizacion DESC; ";
                SqlParameter[] parametros = {
                    new SqlParameter("@fecha", fecha),
                    new SqlParameter("@user", usuario)
                };
                return ObtenerDatos(con, parametros);
            }
        }
    }
}
