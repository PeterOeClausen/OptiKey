using System;
using System.Collections.Generic;
using Prism.Mvvm;
using System.IO;
using System.Linq;

namespace JuliusSweetland.OptiKey.Services
{
    public class PhraseStateService : BindableBase, IPhraseStateService
    {
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

        public Random Random { get; set; }

        public void SetPhraseFile(string path)
        {
            Console.WriteLine("SetPhraseFile called in PhraseStateService with: " + path);
            phrases = File.ReadAllLines(path).ToList();
            phraseNumber = Random.Next(0, Phrases.Count);
            OnPropertyChanged("phrases");
            OnPropertyChanged("phraseNumber");
        }
    }
}
