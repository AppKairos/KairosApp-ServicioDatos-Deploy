using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.Exceptions
{
    public class InvalidOperationProductException:Exception
    {
        public InvalidOperationProductException(string message):base(message)
        {

        }
    }
}
