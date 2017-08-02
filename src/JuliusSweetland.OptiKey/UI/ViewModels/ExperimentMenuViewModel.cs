using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuliusSweetland.OptiKey.UI.ViewModels
{
    public class ExperimentMenuViewModel
    {
        private bool enableSuggestionsAndMultikeyFeature;
        public bool EnableSuggestionsAndMultikeyFeature {
            get {
                return enableSuggestionsAndMultikeyFeature; }
            set{
                enableSuggestionsAndMultikeyFeature = value;
            }
        }

        private int amountOfSentencesToType;
        public int AmountOfSentencesToType {
            get {
                return amountOfSentencesToType; }
            set
            {
                //Validate input!
                Console.WriteLine("AmountOfSentencesToType Set: " + value);
                amountOfSentencesToType = value;
            }
        }
    }
}
