using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    // Eigene Exceptionklasse, wird geworfen, wenn eine oder mehrere Dateien nicht gefunden wurden.
    public class FileIsNotChoosenException : Exception
    {
        public FileIsNotChoosenException(): base() { }

        // Die Message-Eigenschaft der Exception wird mit eigener Message überschrieben.
        public override string Message => "Die benötigten Pfade wurden nicht gefunden! Bitte wählen Sie sowohl die Modellierdatei, als auch den Ausgabeort!";
    }
}
