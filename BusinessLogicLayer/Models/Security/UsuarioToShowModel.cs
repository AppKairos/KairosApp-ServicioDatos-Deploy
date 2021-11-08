using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.BusinessLogicLayer.Models
{
    public class UsuarioToShowModel
    {
        public long? Id { get; set; }
        public String Nombre { get; set; }
        public String Email { get; set; }
        public String Rol { get; set; }
        public Boolean Activo { get; set; }
        public long Telefono { get; set; }
        public String TipoUsuario { get; set; }

    }
}
