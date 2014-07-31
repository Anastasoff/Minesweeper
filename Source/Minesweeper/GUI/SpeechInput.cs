namespace Minesweeper.GUI
{
    using System;
    using System.Linq;
    using System.Speech.Recognition;
    using Interfaces;

    public class SpeechInput : IInputDevice
    {
        private string phrase = string.Empty;
        private string partOfPhrase = string.Empty;

        private string[] allowedPhrases =
        {
            "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine",
            "restart", "hiscore", "keyboard", "exit", "placeflag", "yes", "no"
        };

        public SpeechInput()
        {
            SpeechRecognizer recognizer = new SpeechRecognizer();
            Choices numbers = new Choices();
            numbers.Add(this.allowedPhrases);
            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(numbers);
            Grammar g = new Grammar(gb);
            recognizer.LoadGrammar(g);
            recognizer.SpeechRecognized +=
                new EventHandler<SpeechRecognizedEventArgs>(this.Sre_SpeechRecognized);
        }

        public void Sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            this.partOfPhrase = e.Result.Text;
        }

        public string GetInput()
        {
            string word = this.GetWord();

            if (Array.IndexOf(this.allowedPhrases, word) > 9)
            {
                // this is a command, not a number
                if (word == "placeflag")
                {
                    word = this.GetInput();
                    if (Array.IndexOf(this.allowedPhrases, word) > 9)
                    {
                        return word;
                    }

                    word = "flag " + word;
                }

                return word;
            }
            //// this is a number
            this.phrase = Array.IndexOf(this.allowedPhrases, word).ToString() + " ";
            word = this.GetWord();
            if (Array.IndexOf(this.allowedPhrases, word) > 9)
            {
                //// second word is not a number, so pass it as a command
                return word;
            }

            this.phrase = this.phrase + Array.IndexOf(this.allowedPhrases, word).ToString();
            Console.WriteLine(this.phrase);
            return this.phrase;
        }
        
        private string GetWord()
        {
            this.partOfPhrase = string.Empty;
            while (this.partOfPhrase == string.Empty)
            {
                //// wait for event handler to recognize word
            }

            return this.partOfPhrase;
        }
    }
}