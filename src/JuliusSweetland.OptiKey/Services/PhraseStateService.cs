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

        public void SetPhraseFile(string path)
        {
            // Initialisation of the phrases by setting the path and initialising the list of phrases 
            Console.WriteLine("SetPhraseFile called in PhraseStateService with: " + path);
            var random = new Random();
            phrases = File.ReadAllLines(path).OrderBy(s => random.Next()).ToList();
            phrases.Insert(0, "");
            phrases.Insert(0, "What is the complete name of your university? (Answer in English/Danish)");
            phraseNumber = 0; // initialise phraseNumber to 0
            OnPropertyChanged("phrases");
            OnPropertyChanged("phraseNumber");
        }
    }
}
