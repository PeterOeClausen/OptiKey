using System;
using System.Collections.Generic;
using Prism.Mvvm;
using System.IO;
using System.Linq;
using JuliusSweetland.OptiKey.UI.ViewModels;
using JuliusSweetland.OptiKey.Properties;
using JuliusSweetland.OptiKey.Models;

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
            if (Settings.Default.KeyboardAndDictionaryLanguage.ToString() == "DanishDenmark")
            {
                phrases.Insert(0, "Svar på følgende spørgsmål: Beskriv billedet på papiret foran dig");
            }
            else
            {
                phrases.Insert(0, "Answer the question: Describe the picture on the paper in front of you");
            }
            if (Settings.Default.KeyboardAndDictionaryLanguage.ToString() == "DanishDenmark")
            {
                phrases.Insert(0, "Svar på følgende spørgsmål: Hvad er det fulde navn på dit universitet?");
            }
            else
            {
                phrases.Insert(0, "Answer the question: What is the complete name of your university?");
            }
            for (int i = 2; i< Settings.Default.ExperimentMenu_NumberOfSentencesToType; i=i+2)
            {
                if (Settings.Default.KeyboardAndDictionaryLanguage.ToString() == "DanishDenmark")
                {
                    phrases.Insert(i, "Svar på følgende spørgsmål: Hvor hårdt skulle du arbejde for at opnå dit præsentations niveau i den foregående runde (giv en score mellem 1 og 7)?");
                }
                else
                {
                    phrases.Insert(i, "Answer the question: How hard did you have to work to accomplish your level of performance in the previous trial (give a score between 1 and 7)?");
                }
                
            }
            phraseNumber = 0; // initialise phraseNumber to 0
            OnPropertyChanged("phrases");
            OnPropertyChanged("phraseNumber");

            phrases.ForEach(Console.WriteLine);
        }
    }
}
