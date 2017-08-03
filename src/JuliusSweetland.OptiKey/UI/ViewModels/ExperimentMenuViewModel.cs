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
                Console.WriteLine("EnableSuggestionsAndMultikeyFeature get: " + enableSuggestionsAndMultikeyFeature);
                return enableSuggestionsAndMultikeyFeature; }
            set{
                Console.WriteLine("EnableSuggestionsAndMultikeyFeature Set: " + value);
                enableSuggestionsAndMultikeyFeature = value;
            }
        }

        private int amountOfSentencesToType;
        public int AmountOfSentencesToType {
            get {
                Console.WriteLine("AmountOfSentencesToType Get: " + amountOfSentencesToType);
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
