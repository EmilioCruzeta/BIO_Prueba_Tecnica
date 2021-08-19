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
                    com.ExecuteScalar(); 
                    cn.Close();

            }

                return true;
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
        public List<EstadoCuenta> LlenarEstadoCuenta()
        {
            List<EstadoCuenta> ListaCuenta = new List<EstadoCuenta>();
            try
            {
                using (SqlConnection cn = new SqlConnection(strCn))
                {
                    string sql = "select top 10 EstadoCuent_Id, EstadoCuent_Nombre, EstadoCuent_Apellido,EstadoCuent_Titular,EstadoCuent_Fecha from EstadoCuentaM ";
                    cn.Open();
                    SqlCommand com = new SqlCommand(sql, cn);
                    SqlDataReader Row = com.ExecuteReader();
                   while (Row.Read())
                    {
                        EstadoCuenta Datos = new EstadoCuenta()
                        {
                            EstadoCuent_Id = Convert.ToInt32(Row["EstadoCuent_Id"]),
                            EstadoCuent_Nombre = Row["EstadoCuent_Nombre"].ToString(),
                            EstadoCuent_Apellido = Row["EstadoCuent_Apellido"].ToString(),
                            EstadoCuent_Titular = Row["EstadoCuent_Titular"].ToString(),
                            EstadoCuent_Fecha = Convert.ToDateTime( Row["EstadoCuent_Fecha"]),
                        };
                        ListaCuenta.Add(Datos);
                    }
                  
                    
                }

                return ListaCuenta;
            }
            catch(Exception ex)
            {
                return ListaCuenta;
            }
        }


        public List<EstadoCuenta> LlenarEstadoCuentaDetalle(int id)
        {
            List<EstadoCuenta> ListaCuenta = new List<EstadoCuenta>();
            try
            {
                using (SqlConnection cn = new SqlConnection(strCn))
                {
                    string sql = string.Concat("select EstadoCuent_Fecha, EstadoCuent_Concepto,EstadoCuent_Debito,",
                                                      "EstadoCuent_Credito,",
                                                      " EstadoCuent_Balance",
                                                       " from EstadoCuentaD where EstadoCuent_Id = ", id);

                    cn.Open();
                    SqlCommand com = new SqlCommand(sql, cn);
                    SqlDataReader Row = com.ExecuteReader();
                    while (Row.Read())
                    {
                        EstadoCuenta Datos = new EstadoCuenta()
                        {
                            EstadoCuent_Balance = Convert.ToDouble(Row["EstadoCuent_Balance"]),
                            EstadoCuent_Concepto = Row["EstadoCuent_Concepto"].ToString(),
                            EstadoCuent_Credito = Convert.ToDouble( Row["EstadoCuent_Credito"]),
                            EstadoCuent_Debito = Convert.ToDouble(Row["EstadoCuent_Debito"]),
                            EstadoCuent_FechaD = Convert.ToDateTime(Row["EstadoCuent_Fecha"]),
                        };
                        ListaCuenta.Add(Datos);
                    }


                }

                return ListaCuenta;
            }
            catch (Exception ex)
            {
                return ListaCuenta;
            }
        }
    }
}
