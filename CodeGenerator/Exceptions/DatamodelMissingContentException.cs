using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public class DatamodelMissingContentException : Exception
    {
        public DatamodelMissingContentException(string message) : base(message)
        {

        }

    }
}
