using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.PresentationLayer.Exceptions
{
    public class InvalidOperationEmpastadoException : Exception
    {
        public InvalidOperationEmpastadoException(string message) : base(message)
        {

        }
    }
}
