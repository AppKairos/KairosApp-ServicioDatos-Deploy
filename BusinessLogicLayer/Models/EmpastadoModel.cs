using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.BusinessLogicLayer.Models
{
    public class EmpastadoModel
    {
        public long? Id { get; set; }
        public String Facultad { get; set; }
        public String Carrera { get; set; }
        public String TituloTesis { get; set; }
        public String Trabajo { get; set; }
        public String Autor { get; set; }
        public String Tutor { get; set; }
        public String Mes { get; set; }
        public long Anio { get; set; }
        public long Cantidad { get; set; }
        public long IdUsuario { get; set; }
        public String TipoUsuario { get; set; }
        public Boolean Estado { get; set; }
        public EmpastadoModel()
        {
            Estado = true;
        }
    }
}
