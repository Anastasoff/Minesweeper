using System;
using System.Linq;
using System.Speech.Recognition;
using Minesweeper.Interfaces;


namespace Minesweeper.GUI
{
    class SpeechInput : IInputDevice
    {
        private string phrase = "";
        private string partOfPhrase = "";

        private string[] allowedPhrases = {"zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine",
                "restart",  "hiscore", "keyboard", "exit", "placeflag", "yes", "no"};
        public SpeechInput()
        {
            SpeechRecognizer recognizer = new SpeechRecognizer();
            Choices numbers = new Choices();
            numbers.Add(allowedPhrases);
            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(numbers);
            Grammar g = new Grammar(gb);
            recognizer.LoadGrammar(g);
            recognizer.SpeechRecognized +=
              new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);
            
        }

        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            partOfPhrase = e.Result.Text;
        }


        public string GetInput()
        {
            string word = GetWord();

            if (Array.IndexOf(allowedPhrases, word) > 9)
            {
                // this is a command, not a number
                if (word == "placeflag")
                {
                    word = GetInput();
                    if (Array.IndexOf(allowedPhrases, word) > 9) {
                        return word;
                    }

                    word = "flag " + word;
                }

                return word;
            }
            // this is a number
            phrase = Array.IndexOf(allowedPhrases, word).ToString() + " ";
            word = GetWord();
            if (Array.IndexOf(allowedPhrases, word) > 9)
            {
                //second word is not a number, so pass it as a command
                return word;
            }
            phrase = phrase + Array.IndexOf(allowedPhrases, word).ToString();
            Console.WriteLine(phrase);
            return phrase;
        }
        
        
        private string GetWord()
        {
            partOfPhrase = "";
            while (partOfPhrase == "")
            {
                //wait for event handler to recognize word
            }
            return partOfPhrase;
        }
    }

}