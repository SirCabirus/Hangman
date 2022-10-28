using System;
using System.Text;

namespace Hangman
{
    public class Hangman
    {
        // Vorrat an zu erratenden Wörtern
        public String[] wordlibrary =
        {
          "Grünkohl", "Meisenring", "Computer", "Obstsalat", "Tastatur", "Münze", "Autobus", "Geisterstadt", "Ultima",
          "Grafikkarte", "Limonade", "Werkstatt", "Temperatur", "Kleiderschrank", "Rasenmäher", "Pilot", "Matrose",
          "Fahrrad", "Kühlschrank", "Stehlampe", "Schublade", "Zeugnis", "Kopfhörer", "Nudelsieb", "Weinglas",
          "Oberhemd", "Zaubermaus", "Garage", "Hagelschauer", "Rollenspiel", "Klobürste", "Laptop", "Automat",
          "Überfall", "Fotoapparat", "Gewitter", "Regenbogen", "Flugsimulator", "Programmiersprache", "Editor",
          "Buchhändler", "Rasierapparat", "Anzug", "Zeitungsständer", "Kronleuchter", "Kommode", "Schulheft",
          "Klopapier", "Sonnenblume", "Zitrone", "Kaleidoskop", "Urlaub", "Fernseher", "Gymnasium", "Freunde",
          "Plattenspieler", "Musik", "Autobahn", "Fernsehserie", "Tintenstrahldrucker", "Flugplatz", "Aperitif"
        };

        // Anzahl der Versuche bis Wort gefunden werden muss
        public byte MaxTry { get; set; } = 6;
        
        /*
         * Gibt ein zufälliges Wort aus dem Wortvorrat in Großbuchstaben zurück
         */
        public String GetRandomWord()
        {
            // Zufallsgenerator erzeugen
            Random rgen = new Random();
            
            // erzeugt eine Zufallszahl (Untergrenze (inklusiv), Obergrenze (exklusiv)) als Index aus dem Wortvorrat
            int rindex = rgen.Next(0, wordlibrary.Length);

            // Zufälliges Wort aus dem Wortvorrat in Großbuchstaben zurückgeben
            return wordlibrary[rindex].ToUpper();
        }

        /*
         * Gibt auf der Console aus, wie das Spiel funktioniert
         */
        public void PrintIntro()
        {
            Console.WriteLine("Wörterraten");
            Console.WriteLine("===========\n");
            Console.WriteLine("Errate ein Wort aus einem Wortvorrat von " + wordlibrary.Length + " Wörtern.");
            Console.WriteLine("Gib einen Buchstaben oder ein ganzes Wort ein. Wenn der Buchstabe im Wort");
            Console.WriteLine("nicht vorkommt oder das eingegebene Wort nicht richtig ist, hast Du einen");
            Console.WriteLine("Versuch veloren. Du hast " + MaxTry + " Versuche um das Wort zu ermitteln.\n");
            Console.WriteLine("Durch Eingabe von '#' (ohne Anführungsstriche) kannst Du Abbrechen und");
            Console.WriteLine("Dir das Wort anzeigen lassen. Und nun viel Spaß ;-) \n\n");
        }

        /*
         * Gibt für den übergebenen String einen gleichlangen String zurück 
         * welcher durchgehend mit einem '-'-Zeichen gefüllt ist
         * param word das Wort für das die '-'-Zeichenkette erzeugt werden soll 
         * return die erzeugte '-'-Zeichenkette     
         */  
        public String GetInitWordPattern(String word)
        {
            StringBuilder buff = new StringBuilder();

            for (int i = 0; i < word.Length; i++)
            {
                buff.Append('-');
            }
            return buff.ToString();
        }

        /*
         * Liest eine durch [Return] abgeschlossene Eingabe vom Keyboard
         * return die eingebene Buchstabenfolge in Großbuchstaben
         */ 
        public String GetInput()
        {
            return Console.ReadLine().ToUpper();
        }

        /*
         * Vergleicht den ersten Buchstaben der Zeichenkette input mit dem Vorkommen dieses
         * Buchstabens in der Zeichenkette word. Der Buchstabe wird an allen vorkommenden Stellen
         * in der Zeichenkette pattern eingetragen und zurückgegeben
         * 
         * param word das zu vergleichende Wort
         * param input der erste Charakter dieser Zeichenkette enthält den zu vergleichenden Buchstaben
         * param pattern eine Zeichenkette in der Länge des Parameters word. Dieser String kann bereits
         *        Zeichen die mit dem Parameter word übereinstimmen an der richtigen Position enthalten.
         * return die übergebene Zeichenkette pattern, bei Übereinstimmung ergänzt wie oben beschrieben
         */
        public String Guess(String word, String input, String pattern)
        {
            char[] linePattern = pattern.ToCharArray();

            for (int i = 0; i < word.Length; ++i)
            {
                if (word[i] == input[0])
                {
                    linePattern[i] = input[0];
                }
            }
            return new String(linePattern);
        }

        /*
         * Gibt den Wortvorrat aus
         */
        public void DumpWords()
        {
            for (int i = 0; i < wordlibrary.Length; i++)
            {
                Console.WriteLine("Word[" + i + "]: " + wordlibrary[i]);
            }
        }
    }
}
