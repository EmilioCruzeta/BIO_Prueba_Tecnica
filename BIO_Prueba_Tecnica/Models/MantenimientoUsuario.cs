using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BIO_Prueba_Tecnica.Models;
using BIO_Prueba_Tecnica.Interfaces;


namespace BIO_Prueba_Tecnica.Models
{
    public class MantenimientoUsuario: IMantenimientoUsuario
    {
        private string strCn = "Data Source=DESKTOP-01IH626\\MSSQLSERVER14;Initial Catalog=BIO;Integrated Security=True";

        public bool ValidarUsuario (Login DatosLogin)
        {
            try
            {

            bool Row = false;
            using (SqlConnection cn  = new SqlConnection(strCn))
            {
                    string sql = string.Concat("select count(*) from Usuarios where  usuario_Clave ='", DatosLogin.usuario_Clave,
                                               "' and  usuario_Nombre = '", DatosLogin.usuario_Nombre, "' ");
                    SqlCommand com = new SqlCommand(sql,cn);

                    cn.Open();
                    Row = (bool)com.ExecuteScalar();
                    cn.Close();

            }

                return Row;
            }
            catch(Exception ex)
            {
               return false;
            }
        }

        public  bool CrearUsua(Usuarios DatosUsuario)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(strCn))
                {
                    string sql = string.Concat("insert into Usuarios(usuario_Nombre ,",
                                                            "usuario_Clave,",
                                                            "usuario_FechaCrea,",
                                                            "usuario_FechaNac,",
                                                            "usuario_FechaMod,",
                                                            "usuario_Email,",
                                                            "usuario_Genero,",
                                                            "usuario_Edad) values",
                                                            "('", DatosUsuario.usuario_Nombre,"','" ,
                                                              DatosUsuario.usuario_Clave,"','",
                                                              DatosUsuario.usuario_FechaCrea, "','",
                                                              DatosUsuario.usuario_FechaNac, "','",
                                                              DatosUsuario.usuario_FechaMod, "','",
                                                              DatosUsuario.usuario_Email, "','",
                                                              DatosUsuario.usuario_Genero, "','",
                                                              DatosUsuario.usuario_Edad, "')");
                    SqlCommand com = new SqlCommand(sql,cn);
                    cn.Open();
                     com.ExecuteNonQuery();
                    cn.Close();
                }

                return true; 
            }
            catch(Exception ex)
            {
                return false;
            }
        }

    }
}
