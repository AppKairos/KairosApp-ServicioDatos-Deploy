using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.BusinessLogicLayer.Models.Security
{
    public class UsuarioToShowGmailModel
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String Nombre { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String Rol { get; set; }
        public Boolean Activo { get; set; }
        public String TipoUsuario { get; set; }
        public long Telefono { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public String AuthToken { get; set; }
        public String IdToken { get; set; }
    }
}
