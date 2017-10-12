using JuliusSweetland.OptiKey.Properties;
using JuliusSweetland.OptiKey.Services;
using Prism.Mvvm;
using System;
using System.IO;

namespace JuliusSweetland.OptiKey.UI.ViewModels
{
    public class ExperimentMenuViewModel : BindableBase
    {
        private string phrasesFilePath = Settings.Default.ExperimentMenu_PhraseFilePath;
        public string PhrasesFilePath
        {
            get { return phrasesFilePath; }
            set {
                SetProperty(ref phrasesFilePath, value);
                Settings.Default.ExperimentMenu_PhraseFilePath = value;
            }
        }

        private int amountOfSentencesToType = Settings.Default.ExperimentMenu_NumberOfSentencesToType;
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
                Settings.Default.ExperimentMenu_NumberOfSentencesToType = amountOfSentencesToType;
            }
        }

        private bool enableSuggestionsFeature = Settings.Default.ExperimentMenu_EnableSuggestionsFeature;
        public bool EnableSuggestionsFeature
        {
            get {return enableSuggestionsFeature;}
            set {
                enableSuggestionsFeature = value;
                Settings.Default.ExperimentMenu_EnableSuggestionsFeature = value;
            }
        }

        private bool showClearKey = Settings.Default.ExperimentMenu_ShowClearScratchPadKey;
        public bool ShowClearKey
        {
            get { return showClearKey; }
            set {
                showClearKey = value;
                Settings.Default.ExperimentMenu_ShowClearScratchPadKey = value;
            }
        }

        private bool showSpeakKey = Settings.Default.ExperimentMenu_ShowSpeakPronounceKey;
        public bool ShowSpeakKey
        {
            get { return showSpeakKey; }
            set {
                showSpeakKey = value;
                Settings.Default.ExperimentMenu_ShowSpeakPronounceKey = value;
            }
        }

        private bool showPauseKey = Settings.Default.ExperimentMenu_ShowPauseKey;
        public bool ShowPauseKey
        {
            get { return showPauseKey; }
            set {
                showPauseKey = value;
                Settings.Default.ExperimentMenu_ShowPauseKey = value;
            }
        }

        private bool showShiftKey = Settings.Default.ExperimentMenu_ShowShiftKey;
        public bool ShowShiftKey
        {
            get { return showShiftKey; }
            set {
                showShiftKey = value;
                Settings.Default.ExperimentMenu_ShowShiftKey = value;
            }
        }

        private bool showBackspaceKey = Settings.Default.ExperimentMenu_ShowBackspaceKey;
        public bool ShowBackspaceKey
        {
            get { return showBackspaceKey; }
            set {
                showBackspaceKey = value;
                Settings.Default.ExperimentMenu_ShowBackspaceKey = value;
            }
        }

        private bool showBackOneWordKey = Settings.Default.ExperimentMenu_ShowBackOneWordKey;
        public bool ShowBackOneWordKey
        {
            get { return showBackOneWordKey; }
            set {
                showBackOneWordKey = value;
                Settings.Default.ExperimentMenu_ShowBackOneWordKey = value;
            }
        }

        private bool showQuitKey = Settings.Default.ExperimentMenu_ShowQuitKey;
        public bool ShowQuitKey
        {
            get { return showQuitKey; }
            set {
                showQuitKey = value;
                Settings.Default.ExperimentMenu_ShowQuitKey = value;
            }
        }

        private bool enableMultikeySwipeFeature = Settings.Default.ExperimentMenu_EnableMultiKeySwipeFeature;
        public bool EnableMultikeySwipeFeature
        {
            get { return enableMultikeySwipeFeature; }
            set
            {
                enableMultikeySwipeFeature = value;
                Settings.Default.ExperimentMenu_EnableMultiKeySwipeFeature = value;
            }
        }

        private bool showDwelltimeAdjustments = Settings.Default.ExperimentMenu_ShowDwellTimeAdjustment;
        public bool ShowDwelltimeAdjustments
        {
            get { return showDwelltimeAdjustments; }
            set {
                showDwelltimeAdjustments = value;
                Settings.Default.ExperimentMenu_ShowDwellTimeAdjustment = value;
            }
        }

        private string optiKeyLogPath = CSVLogService.Instance.OptiKeyLogPath;
        public string OptiKeyLogPath
        {
            get { return optiKeyLogPath; }
            set {    
                SetProperty(ref optiKeyLogPath, value);
                CSVLogService.Instance.OptiKeyLogPath = value;
            }
        }

        #region Choose what to log booleans
        public bool DoLogGazeData
        {
            get { return CSVLogService.Instance.doLogGazeData; }
            set { CSVLogService.Instance.doLogGazeData = value; }
        }

        public bool DoLogScratchPadText
        {
            get { return CSVLogService.Instance.doLogScratchPadText; }
            set { CSVLogService.Instance.doLogScratchPadText = value; }
        }

        public bool DoLogPhraseText
        {
            get { return CSVLogService.Instance.doLogPhraseText; }
            set { CSVLogService.Instance.doLogPhraseText = value; }
        }

        public bool DoLogKeySelection
        {
            get { return CSVLogService.Instance.doLogKeySelection; }
            set { CSVLogService.Instance.doLogKeySelection = value; }
        }

        public bool DoLog_userLooksAtKey
        {
            get { return CSVLogService.Instance.doLog_userLooksAtKey; }
            set { CSVLogService.Instance.doLog_userLooksAtKey = value; }
        }

        public bool DoLog_multiKeySelection
        {
            get { return CSVLogService.Instance.doLog_multiKeySelection; }
            set { CSVLogService.Instance.doLog_multiKeySelection = value; }
        }
        #endregion
    }
}
