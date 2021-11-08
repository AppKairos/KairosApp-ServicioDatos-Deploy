using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.BusinessLogicLayer.Models
{
    public class UsuarioModel
    {
        public long? Id { get; set; }
        public String Nombre { get; set; }
        public String Email { get; set; }
        public String Rol { get; set; }
        public Boolean Activo { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public long Telefono { get; set; }
        public String Password { get; set; }
        public String TipoUsuario { get; set; }
        public UsuarioModel()
        {
            Rol = "cliente";
            Activo = true;
            TipoUsuario = "Manual";
            PasswordHash = null;
            PasswordSalt = null;

        }
    }
}
