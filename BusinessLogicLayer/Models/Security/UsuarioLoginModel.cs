using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.BusinessLogicLayer.Models
{
    public class UsuarioLoginModel
    {
        public long? Id { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String TipoUsuario { get; set; }

    }
}
