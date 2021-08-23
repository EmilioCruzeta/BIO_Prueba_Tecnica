using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using BIO_Prueba_Tecnica.Models;
using BIO_Prueba_Tecnica.Interfaces;
using System.Net.Mail;

namespace BIO_Prueba_Tecnica.Models
{
    public class MantenimientoUsuario: IMantenimientoUsuario
    {
        private string strCn = "Data Source=DESKTOP-01IH626\\MSSQLSERVER14;Initial Catalog=BIO;Integrated Security=True";

        public int ValidarUsuario (Login DatosLogin)
        {
            try
            {
                int i = 0;
            using (SqlConnection cn  = new SqlConnection(strCn))
            {
                    string sql = string.Concat("select count(*) from Usuarios where  usuario_Clave ='", DatosLogin.usuario_Clave,
                                               "' and  usuario_Nombre = '", DatosLogin.usuario_Nombre, "' ");
                    SqlCommand com = new SqlCommand(sql,cn);

                    cn.Open();
                  i =  (int)com.ExecuteScalar(); 
                    cn.Close();

            }

                return i;
            }
            catch
            {
               return 0;
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
            catch
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
            catch
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
                            EstadoCuent_FechaD = Convert.ToDateTime(Row["EstadoCuent_Fecha"])
                        };
                        ListaCuenta.Add(Datos);
                    }


                }

                return ListaCuenta;
            }
            catch 
            {
                return ListaCuenta;
            }
        }


       public bool RecuperarCredenciales(string email)
        {
            try
            {
                if(ValidarMail(email))
                {
                    using (SqlConnection cn = new SqlConnection(strCn))
                    {
                        string sql = string.Concat("select usuario_Clave, usuario_Nombre from Usuarios where usuario_Email = '",email,"'");

                        SqlCommand com = new SqlCommand(sql,cn);
                        cn.Open();

                        SqlDataReader rows = com.ExecuteReader();

                        if (rows.Read())
                        {
                            Usuarios Datos = new Usuarios()
                            {
                                usuario_Clave = rows["usuario_Clave"].ToString(),
                                usuario_Nombre = rows["usuario_Nombre"].ToString()
                            };

                            string bodydatos = string.Concat("<table border striped><thead><tr><td>Nombre</td><td>Clave</td></thead>",
                             "<tbody><tr><td>", Datos.usuario_Nombre,"</td><td>",Datos.usuario_Clave,"</td></tbody></table>");
                            MailMessage mail = new MailMessage();
                            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                            mail.From = new MailAddress("prueba.envio.tenico@gmail.com");
                            mail.To.Add(email);
                            mail.Subject = "Recuperar Clave";
                            mail.Body = bodydatos;
                            mail.IsBodyHtml = true;

                            SmtpServer.Port = 587;
                            SmtpServer.Credentials = new System.Net.NetworkCredential("prueba.envio.tenico@gmail.com", "1111111111+");
                            SmtpServer.EnableSsl = true;
                            SmtpServer.Send(mail);
                        }


                    }

                    return true;
                }
                return false;
            }

            catch
            {

                return false;
            }
        }


        public bool ValidarMail(string mail)
        {
            try
            {

                int ValorMail;
                using (SqlConnection cn = new SqlConnection(strCn))
                {
                    string sql = string.Concat(" select count(usuario_Email)  from  Usuarios where usuario_Email ='", mail, "'");
                    SqlCommand com = new SqlCommand(sql,cn);
                    cn.Open();

                    ValorMail = (int)com.ExecuteScalar();
                    cn.Close();
                }

                return (ValorMail > 0) ;
            }

            catch
            {

                return false;
            }
        }


        public void EnviarEstadoCuenta( string idCuenta,string clave, string usuario)
        {
            
            try
            {
                string email = recuperarMal(clave,usuario);
                string AttRuta = @"C:\BIODocumento\documento.pdf";
                using (SqlConnection cn = new SqlConnection(strCn))
                {

                  
                    string bodydatos = string.Concat("<table border striped><thead><tr><td>Fecha</td><td>Concepto</td><td>Debito</td>",
                                                     "<td>Crédito</td><td>Balance</td></tr></thead><tbody>");

                    string sql = string.Concat("select EstadoCuent_Fecha, EstadoCuent_Concepto,EstadoCuent_Debito,",
                                                      "EstadoCuent_Credito,",
                                                      " EstadoCuent_Balance",
                                                       " from EstadoCuentaD where EstadoCuent_Id = ", idCuenta);

                    cn.Open();
                    SqlCommand com = new SqlCommand(sql, cn);
                    SqlDataReader Row = com.ExecuteReader();
                    while (Row.Read())
                    {
                        EstadoCuenta Datos = new EstadoCuenta()
                        {
                            EstadoCuent_Balance = Convert.ToDouble(Row["EstadoCuent_Balance"]),
                            EstadoCuent_Concepto = Row["EstadoCuent_Concepto"].ToString(),
                            EstadoCuent_Credito = Convert.ToDouble(Row["EstadoCuent_Credito"]),
                            EstadoCuent_Debito = Convert.ToDouble(Row["EstadoCuent_Debito"]),
                            EstadoCuent_FechaD = Convert.ToDateTime(Row["EstadoCuent_Fecha"])
                        };

                        bodydatos += string.Concat( $"<tr><td>{Datos.EstadoCuent_FechaD.ToString("d")}</td><td>{Datos.EstadoCuent_Concepto}</td><td>{Datos.EstadoCuent_Debito}</td>" +
                                                    $" <td>{Datos.EstadoCuent_Credito}</td>  <td>{Datos.EstadoCuent_Balance}</td></tr>");

                    }
                    bodydatos += "</tbody></table>";




                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    mail.From = new MailAddress("prueba.envio.tenico@gmail.com");
                    mail.To.Add(email);
                    mail.Subject = "Estado de Cuenta";
                    mail.Body = bodydatos;
                    mail.IsBodyHtml = true;
                    System.Net.Mail.Attachment archivo = new Attachment(AttRuta);
                    mail.Attachments.Add(archivo);
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("prueba.envio.tenico@gmail.com", "1111111111+");
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mail);


                }

              
            }
            catch (Exception ex)
            {
                throw new Exception("Error enviando estado de cuenta", ex);
            }
        }

         public   string recuperarMal(string clave, string usuario)
        {
            try
            {
                string mail = "";
                using (SqlConnection cn = new SqlConnection(strCn))
                {
                    string sql = $"select usuario_Email  from  Usuarios where usuario_Clave = '{clave}'and usuario_Nombre ='{usuario}' ";
                    SqlCommand com = new SqlCommand(sql, cn);
                    cn.Open();
                    mail =  com.ExecuteScalar().ToString();
                    cn.Close();
                }
                return mail;
            }
            catch
            {
                return "";
            }
           
        }
    }
}
