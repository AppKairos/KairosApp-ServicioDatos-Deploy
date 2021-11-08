using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.BusinessLogicLayer.Models.Security
{
    public class UsuarioGmailModel
    {
        public long Id { get; set; }
        public String AuthToken { get; set; }
        public String IdToken { get; set; }
        public String Name { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String Rol { get; set; }
        public Boolean Activo { get; set; }
        public String TipoUsuario { get; set; }
        public UsuarioGmailModel()
        {
            TipoUsuario = "Gmail";
            Rol = "cliente";
            Activo = true;
        }
    }
}
