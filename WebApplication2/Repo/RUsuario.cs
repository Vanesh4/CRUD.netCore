using System.Security.Cryptography;
using System.Text;
using WebApplication2.Models;
using System.Data.SqlClient;

namespace WebApplication2.Repo
{
    public class RUsuario
    {
        conexion _cn = new conexion(); 
        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convertir el hash a una cadena hexadecimal
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
        public bool AgregarUsuario(Usuario u)
        {
            try
            {
                using (var conexion = new SqlConnection(_cn.getCadenaConAPP()))
                {
                    conexion.Open();
                    string hashedPassword = HashPassword(u.clave);

                    // Consulta de inserción con contraseña cifrada
                    SqlCommand cmd = new SqlCommand("INSERT INTO Usuarios (nombre, UserName, correoElectronico, Clave, rol) VALUES (@nom, @user, @email, @pass, @rol)", conexion);
                    cmd.Parameters.AddWithValue("nom", u.nombre);
                    cmd.Parameters.AddWithValue("user", u.userName);
                    cmd.Parameters.AddWithValue("email", u.correo);
                    cmd.Parameters.AddWithValue("pass", hashedPassword);
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex) {
                string error = ex.Message;
                return false;
            }
        }

        public bool VerificarRol(string user, int rol)
        {
            try
            {
                using (var conexion = new SqlConnection(_cn.getCadenaConAPP()))
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM permisos where usuario = @user and rol = @rol;", conexion);
                    cmd.Parameters.AddWithValue("user", user);
                    cmd.Parameters.AddWithValue("rol", rol);
                    
                    int count = (int)cmd.ExecuteScalar();
                    if (count == 1) return true;
                    else return false;
                }
                
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
        }
    }
}
