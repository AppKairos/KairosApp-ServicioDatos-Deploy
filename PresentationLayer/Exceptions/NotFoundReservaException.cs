using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.PresentationLayer.Exceptions
{
    public class NotFoundReservaException:Exception
    {
        public NotFoundReservaException(string message):base(message){
           
        }
    }
}
