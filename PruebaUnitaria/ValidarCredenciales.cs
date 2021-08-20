using System;
using Xunit;
namespace PruebaUnitaria
{
   public class ValidarCredenciales
    {
        [Fact]
        public void Test1()
        {

            string clave = "prueba01223";
            bool ValidorCar = ValidarCaracteres(clave);


            Assert.True(ValidorCar);

        }

        private bool ValidarCaracteres(string  Cant)
        {
            try
            {
               
                return (Cant.Length >= 8);

            }
            catch
            {
                return false;
            }
        }
    }
}
