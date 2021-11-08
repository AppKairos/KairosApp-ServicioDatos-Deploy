using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImprentaAPI.PresentationLayer.Exceptions
{
    public class NotFoundSelloException : Exception
    {
        public NotFoundSelloException(string message) : base(message)
        {

        }
    }
}
