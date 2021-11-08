using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.Models
{
    public class ProductoModel
    {
        public long? Id { get; set; }
        [Required]
        public String Nombre { get; set; }
        public String Descripcion { get; set; }
        public Boolean Estado { get; set; }
        public ProductoModel()
        {
            Estado = true;
        }
       

    }
}
