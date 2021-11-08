using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.BusinessLogicLayer.Models
{
    enum Cantidad_Placa
    {
        Full_Color=4,
        Duo_Tono=2,
        Mono_Color=1
    }

    public class AficheModel
    {

        public string  Color { get; set; }  
        public string Tam_Papel { get; set; }
        public string Tipo_papel { get; set; }
        public string Gramaje_papel { get; set; }
        public int Cantidad { get; set; }
        public string Tam_Placa { get; set; }
        public double Precio_Design { get; set; }
        public double Precio_Acabado { get; set; }
        public double Ganancia { get; set; }


    }
}
