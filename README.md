# CodeGenerator

Dieses Programm erstellt ".cs"-Dateien und füllt sie mit generiertem Quelltext, welcher auf der bereitgestellten ".graphml"-Datei beruht.

## Test: Controller

Die Testkomponente des Controllers "CodeGenerator.ControllerTest" kreiert den Ausgabepfad im Verzeichnis der Program-Klasse und den Dateipfad der (im Verzeichnis der Komponente abgespeicherten) "classdiagram.cs"-Datei. Mit übergabe dieser Pfade, ruft sie die Methode StartProcess auf, welche die Komponenten "CodeGenerator.Reader" und "CodeGenerator.Generator" erstellt und einen Datenaustausch simuliert. Dabei erhält der Controller, nach übergabe des Dateipfades, vom Reader ein Datamodel und gibt dieses, zusammen mit dem Ausgabepfad, dem Generator weiter. 

## Test: Reader

Die Reader-Komponente besitzt im ganzen zwei Test-Komponenten, und zwar einmal "CodeGenerator.ReaderTest" und "CodeGenerator.ReaderUnitTest". Dabei stellt das Projekt "CodeGenerator.ReaderTest" die Haupt-Testkomponente dar. Diese Komponente erzeugt lediglich ein valides Datenmodell aus einem Klassendiagramm, welche in Form einer .Graphml Datei vorliegt. Die Ergebnisse werden in einer Konsolen-Applikation dargestellt. Sinn und Zweck dieser Test-Komponente ist es, feststellen zu können ob ein Datenmodell erzeugt werden kann und ob sie für die weiter Verarbeitung eine korrekte Struktur annimmt.

Das Projekt "CodeGenerator.ReaderUnitTest" testet die Reader-Komponente auf spezielle Fälle. Aus einem Klassendiagramm werden Werte geladen und jeweilige Objekte erzeugt. Diese werden mit den korrekten Ergebnissen, die eigentlich aus dem Diagramm entnommen werden sollten, verglichen. 
Diese Unit-Test's können innerhalb des Visual-Studio Testexplorers ausgeführt werden.

## Test: Generator

Das Projekt "CodeGenerator.GeneratorTest" testet die Generator-Komponente. Zu Beginn wird ein Dummy-DataModel mit Attributen, Methoden, Interfaces und Klassen erstellt. 
Dieses wird dem Generator übergeben, zusammen mit einem Ausgabepfad. 
Das Ausgabepfad ist standardmäßig der Ordner "GeneratorTestFiles", welcher auf dem Desktop des aktuellen Nutzers erstellt wird, sollte er bisher nicht existieren.

## Authors

* **Emircan Yüksel** - GUI und Controller
* **Baris Tikir** - Reader
* **Yannik Sahl** - Generator und DataModel

## License

Dieses Projekt ist lizensiert unter der MIT-Lizenz.
