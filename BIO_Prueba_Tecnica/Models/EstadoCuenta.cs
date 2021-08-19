using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BIO_Prueba_Tecnica.Models
{
    public class EstadoCuenta
    {
        public int EstadoCuent_Id { get; set; }
        public string EstadoCuent_Nombre { get; set; }
        public string EstadoCuent_Apellido { get; set; }
        public string EstadoCuent_Titular { get; set; }
        public DateTime EstadoCuent_Fecha { get; set; }
        public DateTime EstadoCuent_FechaD { get; set; }
        public int EstadoCuent_Linea { get; set; }
        public string EstadoCuent_Concepto { get; set; }
        public double EstadoCuent_Debito { get; set; }
        public double EstadoCuent_Credito { get; set; }
        public double EstadoCuent_Balance { get; set; }


    }
}
