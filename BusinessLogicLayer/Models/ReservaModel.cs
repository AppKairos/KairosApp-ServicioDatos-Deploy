using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.BusinessLogicLayer.Models
{
    public class ReservaModel
    {
        public long Id { get; set; }
        public long? IdUsuario { get; set; }
        public long? IdItem { get; set; }
        public string? Nombre { get; set; }
        public string? TipoUsuario { get; set; }

        public Boolean Estado { get; set; }
        public ReservaModel()
        {
            Estado = true;
        }

    }
}
