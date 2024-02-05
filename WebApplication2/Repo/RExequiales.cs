using WebApplication2.Models;
using System.Data.SqlClient;

namespace WebApplication2.Repo
{
    public class RExequiales
    {
        private conexion _cn = new conexion();
        private List<MExequialesTerceros> ObtenerDatos(string consulta, SqlParameter[] parametro)
        {
            var Lista = new List<MExequialesTerceros>();

            using (var conexion = new SqlConnection(_cn.getCadConExequiales()))
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
                        Lista.Add(new MExequialesTerceros()
                        {
                            codTer = reader["COD_CLI"].ToString(),
                            nomCliente = reader["NOM_CLI"].ToString(),
                            fechaNacimiento = reader["FEC_NAC"] != DBNull.Value ? Convert.ToDateTime(reader["FEC_NAC"]).ToString("dd-MM-yyyy") : (string)null,
                            distrito = reader["DISTRITO"].ToString(),
                            benef = reader["BENEF"].ToString(),
                            tipoPlan = reader["COD_PLAN"].ToString(), //llamar columna del plan inner join
                            cuidad = reader["NOM_CIU"].ToString(),
                            edad = CalcularEdad(Convert.ToDateTime(reader["FEC_NAC"])),
                            codCentroCosto = reader["COD_CCO"].ToString(),
                            estadoCivil = reader["EST_CIVIL"].ToString(),
                            numHijos = reader["NUM_HIJOS"].ToString(),
                            contrato = reader["CONTRATO"].ToString(),
                            direccion = reader["DIR_CASA"].ToString(),
                            telefono = reader["TEL_CASA"].ToString(),
                            fechaIngreso = reader["FEC_ING"] != DBNull.Value ? Convert.ToDateTime(reader["FEC_ING"]).ToString("dd-MM-yyyy") : (string)null
                        });
                    }
                }
            }
            return Lista;
        }

        public List<MExequialesTerceros> TercerosMaestra(string cedula)
        {
            string consulta = "SELECT * FROM VistaMaestraTerceros where COD_CLI = @cedula ;";
            SqlParameter[] parametros = { new SqlParameter("@cedula", cedula) };
            return ObtenerDatos(consulta, parametros);
        }

        private static int CalcularEdad(DateTime fechaNacimiento)
        {
            DateTime fechaActual = DateTime.Now;
            int edad = fechaActual.Year - fechaNacimiento.Year;

            if (fechaNacimiento > fechaActual.AddYears(-edad))
            {
                edad--;
            }
            return edad;
        }
    }
}
