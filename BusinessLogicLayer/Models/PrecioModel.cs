using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.BusinessLogicLayer.Models
{
    public class PrecioModel
    {
        public long id { get; set; }
        public string? nombre { get; set; }
        public double? precioneto { get; set; }
        public string tipo { get; set; }
        public bool estado { get; set; }
        public string tam { get; set; }
        public double? precioresma { get; set; }
        public double? pliegos { get; set; }




    }
}
