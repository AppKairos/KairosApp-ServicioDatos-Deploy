using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.BusinessLogicLayer.Models
{
    public class CotizarAficheModel
    {
        public double? Neto { get; set; }
        public double? IVA { get; set; }
        public double? Con_Factura { get; set; } 
        public double? Total { get; set; }
    }
}
