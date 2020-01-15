using System;

namespace Exceptions
{
    // GeneralException-Class die Ausgelöst wird, wenn eine Allgemeine Exception im System vorliegt,
    // welche irrelevant für den User ist.
    public class GeneralException : Exception
    {
        // Allgemeine Message, um den User zu melden, dass ein Fehler im Programm vorliegt.
        public override string Message => "Wir bitten um Entschuldigung, da ist etwas im System falsch gelaufen. Bitte wenden Sie sich an den Hersteller!";
    }

}
