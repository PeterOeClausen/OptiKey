using JuliusSweetland.OptiKey.Services;
using Prism.Mvvm;
using System;
using System.IO;

namespace JuliusSweetland.OptiKey.UI.ViewModels
{
    public class ExperimentMenuViewModel : BindableBase
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
        
        private string optiKeyLogPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "OptiKeyLogs");
        public string OptiKeyLogPath
        {
            get { return optiKeyLogPath; }
            set { SetProperty(ref optiKeyLogPath, value);} 
        }

        private string phrasesFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "phrases2.txt");
        public string PhrasesFilePath
        {
            get { return phrasesFilePath; }
            set { SetProperty(ref phrasesFilePath, value); }
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
