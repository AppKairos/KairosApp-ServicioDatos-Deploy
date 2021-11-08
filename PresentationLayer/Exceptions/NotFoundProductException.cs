using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.Exceptions
{
    public class NotFoundProductException :Exception
    {
        public NotFoundProductException(string message):base(message)
        {

        }
    }
}
