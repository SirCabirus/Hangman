using System;

namespace Hangman
{
    class Program
    {

        static void Main()
        {
            String guessWord;       // das zu erratende Wort
            String currPattern;     // das aktuelle Pattern mit '-' für noch zu erratende Buchstaben
            String retPattern;      // das Pattern nach der neusten Eingabe

            String input;           // nimmt die aktuelle Eingabe auf
            int cnt;                // die Anzahl der Versuche

            bool newgame = true;    // noch ein Spiel?

            // Hangman instanziieren
            // Hangman hm = new Hangman();
            // HangmanFile hm = new HangmanFile();
            HangmanFileCollection hm = new HangmanFileCollection();

            do
            {
                bool goahead = true;                     // weiter machen?
                bool solved  = false;                    // Wort erraten?
                bool canceld = false;                    // Spiel abgebrochen?

                byte maxTry = hm.MaxTry;                 // maximale Anzahl von Versuchen

                // Anleitung ausgeben
                Console.Clear();
                hm.PrintIntro();
                
                // zu erratendes Wort besorgen
                guessWord = hm.GetRandomWord();

                // Pattern für zu erratendes Wort besorgen
                currPattern = hm.GetInitWordPattern(guessWord);

                // Zähler zurücksetzen
                cnt = 1;

                // Pattern und Anzahl der Versuche ausgeben
                Console.WriteLine(currPattern + "      " + cnt);

                do
                {
                    // Eingabe vom Benutzer holen
                    input = hm.GetInput();

                    // wenn die Suche nach dem Wort nicht abgebrochen werden soll
                    if (!(input.Equals("#")))
                    {
                        // bei leerer Eingabe nichts machen - sonst gibt es bei der weiteren Verarbeitung eine Exception
                        if (!(input.Equals("")))
                        {
                            // überprüfen ob das zu suchende Wort als Ganzes eingegeben wurde
                            if (input.Equals(guessWord))
                            {
                                // Spiel beenden wenn Wort gefunden
                                solved = true;
                                goahead = false;
                            } else {
                                // neues Pattern ermitteln
                                retPattern = hm.Guess(guessWord, input, currPattern);

                                // kein Treffer  d e n n  altes und neues Pattern sind gleich!
                                if (retPattern.Equals(currPattern))
                                {
                                    // Anzahl Versuche erhöhen
                                    cnt++;
                                    if (cnt > maxTry)
                                    {
                                        // Spiel beenden wenn zuviel Versuche
                                        goahead = false;
                                    }
                                }
                                // gelöst  d e n n  neues Pattern gleich Suchwort
                                else if (retPattern.Equals(guessWord))
                                {
                                    // Spiel beenden wenn Wort gefunden
                                    solved = true;
                                    goahead = false;
                                }
                                // auf jeden Fall neues Pattern ins alte Pattern übertragen und auf Console ausgeben
                                currPattern = retPattern;
                                Console.WriteLine(currPattern + "      " + cnt);
                            }
                        }
                    }
                    // die Suche nach dem Wort wurde abgebrochen
                    else
                    {
                        canceld = true;
                        goahead = false;
                    }
                } while (goahead);

                // irgendwie ist unsere Suche nach dem Wort zu Ende - nur warum?
                if (solved)
                {
                    if (cnt == 1)
                        Console.WriteLine("\nDu hast das Wort in einem Versuch erraten.\n");
                    else
                        Console.WriteLine("\nDu hast das Wort in " + cnt + " Versuchen erraten.\n");
                }
                else if (canceld)
                {
                    Console.WriteLine("\nAbbruch. Das gesuchte Wort war: " + guessWord + "\n");
                }
                else
                {
                    Console.WriteLine("\nZu viele Versuche. Das gesuchte Wort war: " + guessWord + "\n");
                }

                // Abfragen ob wir noch eine Runde spielen wollen
                Console.WriteLine("Noch ein Spiel? [Return] für weiter, '#' für Abbruch");
                input = hm.GetInput();

                // nur das Lattenkreuz beendet das Spiel
                if (input.Equals("#"))
                {
                    newgame = false;
                } else if (input.ToUpper().Equals("D"))
                {
                    // Geheimbefehl zum Debuggen des Wortvorrates
                    hm.DumpWords();
                    Console.ReadKey();
                }
            } while (newgame);
        }
    }
}
