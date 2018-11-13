using System;
using System.Collections.Generic;
using Prism.Mvvm;
using System.IO;
using System.Linq;
using JuliusSweetland.OptiKey.UI.ViewModels;
using JuliusSweetland.OptiKey.Properties;

namespace JuliusSweetland.OptiKey.Services
{
    public class PhraseStateService : BindableBase, IPhraseStateService
    {
        private int amountOfSentencesToType = Settings.Default.ExperimentMenu_NumberOfSentencesToType;

        private int phrasesShown = 0;
        public int PhrasesShown
        {
            get { return phrasesShown; }
            set { SetProperty(ref phrasesShown, value); }
        }

        private List<string> rawPhrases;
        public List<string> RawPhrases
        {
            get { return rawPhrases; }
            set { SetProperty(ref rawPhrases, value); }
        }

        private List<string> phrases;
        public List<string> Phrases {
            get { return phrases; }
            set { SetProperty(ref phrases, value); }
        }

        private int phraseNumber;
        public int PhraseNumber {
            get { return phraseNumber; }
            set { SetProperty(ref phraseNumber, value); }
        }

        public Random Random { get; set; }

        public void SetPhraseFile(string path)
        {
            // Initialisation of the phrases by setting the path and initialising the list of phrases 
            Console.WriteLine("SetPhraseFile called in PhraseStateService with: " + path);
            rawPhrases = File.ReadAllLines(path).ToList();
            phrases = RandomPhrases(amountOfSentencesToType, rawPhrases);
            phraseNumber = 0; // initialise phraseNumber to 0
            OnPropertyChanged("phrases");
            OnPropertyChanged("phraseNumber");
        }

        // list of phrases created, that are to be typed, in a random order without duplicates
        public List<string> RandomPhrases(int count, List<string> rawPhrases)
        {
            int MyNumber = 0;
            List<int> randomIndexList = new List<int>();
            phrases.Clear(); // Somehow the list contains the phrases from the default_phrase.txt file. Need to clear 

            while (randomIndexList.Count() < count)
            {
                MyNumber = Random.Next(0, count);
                if (randomIndexList != null && !randomIndexList.Contains(MyNumber))
                {
                    randomIndexList.Add(MyNumber);
                    phrases.Add(rawPhrases[MyNumber]);
                }

            }
            return phrases;

        }
    }
}
