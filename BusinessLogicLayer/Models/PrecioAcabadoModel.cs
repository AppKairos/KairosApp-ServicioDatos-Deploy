using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.BusinessLogicLayer.Models.Security
{
    public class PrecioAcabadoModel
    {
        public long Id { get; set; }
        public String NombreProducto { get; set; }
        public double Precio { get; set; }
        public bool Estado { get; set; }
        public PrecioAcabadoModel()
        {
            Estado = true;
        }
    }
}
