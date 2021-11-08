using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.PresentationLayer.Exceptions
{
    public class InvalidOperationSelloException : Exception
    {
        public InvalidOperationSelloException(string message) : base(message)
        {

        }
    }
}
