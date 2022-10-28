using System;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Hangman
{
    public class HangmanFile
    {
        // Anzahl der Versuche bis Wort gefunden werden muss
        public byte MaxTry { get; set; } = 6;

        /*
         * Die einzulesende Textdatei mit den zu erratenden Wörtern, 
         * die Datei darf keine Leerzeilen und keinen Kommentar enthalten
         */
        private readonly String wordFile = @"G:\design\Visual Studio\C#\Hangman\dat\hangmans.dat";

        // Enthält den Wortvorrat
        public String[] wordlibrary;

        /*
         * Konstruktor - füllt Wortvorrat
         */
        public HangmanFile()
        {
            FillWordlibrary();
        }

        /*
         * Liest die durch wordFile spezifizierte Wortdatei ein und überführt die
         * Wörter in ein String[], welches den Wortvorrat bildet. Kommata, Leerzeichen
         * und Zeilenvorschub werden dabei ausgeblendet
         */
        private void FillWordlibrary()
        {
            String words;
            try
            {
                StreamReader wordData = new StreamReader(wordFile, Encoding.UTF7, true);
                words = wordData.ReadToEnd();
                wordData.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine("Fehler: die Datei " + wordFile + " konnte nicht gelesen werden!");
                Console.WriteLine(e.Message);
                Console.WriteLine("[Return] beendet das Programm.");
                Console.ReadKey();
                return;
            }
            wordlibrary = Regex.Split(Regex.Replace(words, "^[,\r\n]+|[,\r\n]+$", ""), @"[,\r\n\s+]+");
        }

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
         * Gibt den Wortvorrat aus. Damit Änderungen an der Wortdatei dynamisch erfolgen können ohne
         * das Programm zu beenden wird die Wortdatei neu eingelesen
         */
        public void DumpWords()
        {
            FillWordlibrary();
            for (int i = 0; i < wordlibrary.Length; i++)
            {
                Console.WriteLine("Word[" + i + "]: " + wordlibrary[i]);
            }
        }
    }
}
