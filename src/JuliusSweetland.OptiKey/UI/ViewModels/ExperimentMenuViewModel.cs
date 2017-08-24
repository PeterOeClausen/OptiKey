using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuliusSweetland.OptiKey.UI.ViewModels
{
    public class ExperimentMenuViewModel
    {
        private int amountOfSentencesToType = 3;
        public int AmountOfSentencesToType
        {
            get
            {
                Console.WriteLine("AmountOfSentencesToType Get: " + amountOfSentencesToType);
                return amountOfSentencesToType;
            }
            set
            {
                //Validate input!
                Console.WriteLine("AmountOfSentencesToType Set: " + value);
                amountOfSentencesToType = value;
            }
        }

        private bool enableSuggestionsFeature = true;
        public bool EnableSuggestionsFeature
        {
            get {return enableSuggestionsFeature;}
            set {enableSuggestionsFeature = value;}
        }

        private bool enableMultikeySwipeFeature = false;
        public bool EnableMultikeySwipeFeature
        {
            get {return enableMultikeySwipeFeature;}
            set {enableMultikeySwipeFeature = value;}
        }

        private bool showShiftKey = true;
        public bool ShowShiftKey
        {
            get { return showShiftKey; }
            set { showShiftKey = value; }
        }

        private bool showBackspaceKey = true;
        public bool ShowBackspaceKey
        {
            get { return showBackspaceKey; }
            set { showBackspaceKey = value; }
        }

        private bool showPauseKey = true;
        public bool ShowPauseKey
        {
            get { return showPauseKey; }
            set { showPauseKey = value; }
        }

        private bool showQuitKey = true;
        public bool ShowQuitKey
        {
            get { return showQuitKey; }
            set { showQuitKey = value; }
        }
    }
}
