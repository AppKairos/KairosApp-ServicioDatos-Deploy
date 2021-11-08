using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.PresentationLayer.Exceptions
{
    public class NotFoundEmpastadoException : Exception
    {
        public NotFoundEmpastadoException(string message) : base(message)
        {

        }
    }
}
