using System;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace Hangman
{
    public class HangmanFileCollection
    {
        // Anzahl der Versuche bis Wort gefunden werden muss
        public byte MaxTry { get; set; } = 6;

        // Enthält den Wortvorrat
        public List<String> wordlibrary = new List<String>();

        // Die einzulesende Textdatei mit den zu erratenden Wörtern
        private readonly String wordFile = @"G:\design\Visual Studio\C#\Hangman\dat\hangman.dat";

        /*
         * Konstruktor - füllt Wortvorrat
         */
        public HangmanFileCollection()
        {
            FillWordlibrary();
        }

        /*
         * Liest die durch wordFile spezifizierte Wortdatei ein und überführt die
         * Wörter in eine Collection List<String>, welche den Wortvorrat bildet. 
         * Komentarzeilen (#), Kommata, Leerzeichen und Zeilenvorschub werden dabei ausgeblendet
         */
        private void FillWordlibrary()
        {
            // Immer mit einer leeren Liste anfangen
            wordlibrary.Clear();

            // Wortvorrat mit dem Inhalt der Wortdatei füllen
            try
            {
                String line;    // speichert die aktuell eingelesen Zeile der Wortdatei

                // using sorgt dafür dass der Streamreader nach dem Einlesen geschlossen wird
                using (StreamReader wordData = new StreamReader(wordFile, Encoding.UTF7, true))
                {
                    // Datei zeilenweise bis zum Dateiende einlesen
                    while ((line = wordData.ReadLine()) != null)
                    {
                        // Kommentarzeilen überlesen
                        if (!line.Contains('#'))
                        {
                            // eingelesene Zeile in die einzelnen Bestandteile zerlegen
                            String[] currentWords = line.Split(',');
                            foreach (String word in currentWords)
                            {
                                // Wörter der Zeile in Liste einfügen, dabei Leerzeichen und Zeilenende abschneiden
                                String wordTrimmed = word.Trim(' ', '\n', '\r');
                                // Wenn es keine Leerzeile ist das Wort im Wortvorrat speichern
                                if (!wordTrimmed.Equals(""))
                                {
                                    wordlibrary.Add(wordTrimmed);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Fehler: die Datei " + wordFile + " konnte nicht gelesen werden!");
                Console.WriteLine(e.Message);
                Console.WriteLine("[Return] beendet das Programm.");
                Console.ReadKey();
                return;
            }
        }

        /*
         * Gibt ein zufälliges Wort aus dem Wortvorrat in Großbuchstaben zurück
         */
        public String GetRandomWord()
        {
            // Zufallsgenerator erzeugen
            Random rgen = new Random();

            // erzeugt eine Zufallszahl (Untergrenze (inklusiv), Obergrenze (exklusiv)) als Index aus dem Wortvorrat
            int rindex = rgen.Next(0, wordlibrary.Count);

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
            Console.WriteLine("Errate ein Wort aus einem Wortvorrat von " + wordlibrary.Count + " Wörtern.");
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
            for (int i = 0; i < wordlibrary.Count; i++)
            {
                Console.WriteLine("Word[" + i + "]: " + wordlibrary[i]);
            }
        }
    }
}
