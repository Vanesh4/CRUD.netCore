using System.Data.SqlClient;
using WebApplication2.Models;

namespace WebApplication2.Repo
{
    public class RRetirosListado
    {
        private conexion _cn = new conexion();

        private List<MRetirosListado> ObtenerDatos(string consulta, SqlParameter[] parametro)
        {
            var Lista = new List<MRetirosListado>();

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
                        Lista.Add(new MRetirosListado()
                        {
                            codTer = Convert.ToInt32(reader["COD_TER"]),
                            fechaParaCalculo = reader["fecha_para_calculo"] != DBNull.Value ? Convert.ToDateTime(reader["fecha_para_calculo"]).ToString("dd-MM-yyyy") : (string)null,
                            
                        });
                    }
                }
                return Lista;
            }
        }

        public List<MRetirosListado> listarTodos()
        {
            return ObtenerDatos("SELECT * FROM Retiros;", null);
        }
        public List<MRetirosListado> FiltroCedula(int cedula)
        {
            string consulta = "SELECT * FROM Retiros WHERE COD_TER = @Cedula;";
            SqlParameter[] parametros = { new SqlParameter("@Cedula", cedula) };
            return ObtenerDatos(consulta, parametros);
        }
    }
}
