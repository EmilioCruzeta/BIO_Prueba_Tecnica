using System;
using Xunit;

namespace PruebaUnitaria
{
    public class ValidarEmailUsuario
    {
        [Fact]
        public void Test1()
        {

            string Correo = "prueba@gmail.com";
            bool ValidorEmail = IsValidEmail(Correo);
           
            
            Assert.True(ValidorEmail);

        }


      private  bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
