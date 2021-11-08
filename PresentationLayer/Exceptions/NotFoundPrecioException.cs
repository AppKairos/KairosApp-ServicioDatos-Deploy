using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.PresentationLayer.Exceptions
{
    public class NotFoundPrecioException:Exception
    {
        public NotFoundPrecioException(string message) : base(message)
        {

        }
    }
}
