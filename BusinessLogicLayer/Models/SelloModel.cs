using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.BusinessLogicLayer.Models
{
    public class SelloModel
    {
        public long? Id { get; set; }
        public String Nombre { get; set; }
        public String Tipo { get; set; }
        public String Maquinas { get; set; }
        public Double PrecioMaquina { get; set; }
        public Double PrecioTotal { get; set; }
        public Boolean Estado { get; set; }
        public SelloModel()
        {
            Estado = true;
        }
    }
}
