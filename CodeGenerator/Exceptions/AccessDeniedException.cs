using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    // Eigene Exceptionklasse, wird geworfen, wenn eine oder mehrere Dateien nicht gefunden wurden.
    public class AccessDeniedException : Exception
    {
        public string Text { get; set; }

        public AccessDeniedException(string message)
        {
            Text = message;
        }

        public override string Message => Text;
    }
}
