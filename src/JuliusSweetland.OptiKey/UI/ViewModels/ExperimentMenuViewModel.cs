using JuliusSweetland.OptiKey.Enums;
using JuliusSweetland.OptiKey.Models;
using JuliusSweetland.OptiKey.Properties;
using JuliusSweetland.OptiKey.Services;
using Prism.Mvvm;
using System;
using System.Collections.Generic;

namespace JuliusSweetland.OptiKey.UI.ViewModels
{
    public class ExperimentMenuViewModel : BindableBase
    {
        private Enums.ExperimentalKeybordLanguages selectedExperimentalKeyboardLanguage = Settings.Default.selectedExperimentalKeyboardLanguage;
        public Enums.ExperimentalKeybordLanguages SelectedExperimentalKeyboardLanguage
        {
            get { return selectedExperimentalKeyboardLanguage; }
            set
            {
                selectedExperimentalKeyboardLanguage = value;
                Settings.Default.selectedExperimentalKeyboardLanguage = value;
                Enums.ExperimentalKeybordLanguages switchValue = value;
                //Hoping this will change experimental keyboard:
                switch (switchValue)
                {
                    case Enums.ExperimentalKeybordLanguages.English:
                        InstanceGetter.Instance.MainViewModel.HandleFunctionKeySelectionResult(new KeyValue(FunctionKeys.EnglishUK));
                        break;
                    case Enums.ExperimentalKeybordLanguages.Danish:
                        InstanceGetter.Instance.MainViewModel.HandleFunctionKeySelectionResult(new KeyValue(FunctionKeys.DanishDenmark));
                        break;
                }
            }
        }

        public IEnumerable<Enums.ExperimentalKeybordLanguages> ExperimentalKeyboardLanguages
        {
            get
            {
                return Enum.GetValues(typeof(Enums.ExperimentalKeybordLanguages)) as IEnumerable<Enums.ExperimentalKeybordLanguages>;
            }
        }

        public IEnumerable<Enums.ExperimentalKeyboardTypes> ExperimentKeyboardTypes
        {
            get
            {
                return Enum.GetValues(typeof(Enums.ExperimentalKeyboardTypes)) as IEnumerable<Enums.ExperimentalKeyboardTypes>;
            }
        }

        private ExperimentalKeyboardTypes selectedExperimentKeyboardType = Settings.Default.SelectedExperimentKeyboardType;
        public ExperimentalKeyboardTypes SelectedExperimentKeyboardType
        {
            get { return selectedExperimentKeyboardType; }
            set
            {
                selectedExperimentKeyboardType = value;
                Settings.Default.SelectedExperimentKeyboardType = value;
            }
        }
        
        public IEnumerable<Enums.ScreenStates> ScreenStates
        {
            get
            {
                return Enum.GetValues(typeof(ScreenStates)) as IEnumerable<ScreenStates>;
            }
        }

        private ScreenStates selectedScreenState = Settings.Default.SelectedScreenState;
        public ScreenStates SelectedScreenState
        {
            get { return selectedScreenState; }
            set
            {
                selectedScreenState = value;
                Settings.Default.SelectedScreenState = value;
            }
        }

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
        
        private bool enableDotCommaApostrophe = Settings.Default.ExperimentMenu_EnableDotCommaApostrophe;
        public bool EnableDotCommaApostrophe
        {
            get { return enableDotCommaApostrophe; }
            set
            {
                enableDotCommaApostrophe = value;
                Settings.Default.ExperimentMenu_EnableDotCommaApostrophe = value;
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

        private Enums.PointsSources selectedPointSource = Settings.Default.PointsSource;
        public Enums.PointsSources SelectedPointSource
        {
            get { return selectedPointSource; }
            set
            {
                Console.WriteLine("SelectedPointSource set!");
                selectedPointSource = value;
                Settings.Default.PointsSource = value;
                //Restart application:
                System.Windows.Forms.Application.Restart();
                System.Windows.Application.Current.Shutdown();
            }
        }

        public IEnumerable<Enums.PointsSources> PointSources
        {
            get
            {
                return Enum.GetValues(typeof(Enums.PointsSources)) as IEnumerable<Enums.PointsSources>;
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
        public bool DoLog_TobiiGazeData
        {
            get { return CSVLogService.Instance.doLog_TobiiGazeData; }
            set { CSVLogService.Instance.doLog_TobiiGazeData = value; }
        }

        public bool DoLog_EyeTribeGazeData
        {
            get { return CSVLogService.Instance.doLog_EyeTribeGazeData; }
            set { CSVLogService.Instance.doLog_EyeTribeGazeData = value; }
        }

        public bool DoLog_ScratchPadText
        {
            get { return CSVLogService.Instance.doLog_ScratchPadText; }
            set { CSVLogService.Instance.doLog_ScratchPadText = value; }
        }

        public bool DoLog_PhraseText
        {
            get { return CSVLogService.Instance.doLog_PhraseText; }
            set { CSVLogService.Instance.doLog_PhraseText = value; }
        }

        public bool DoLog_KeySelection
        {
            get { return CSVLogService.Instance.doLog_KeySelection; }
            set { CSVLogService.Instance.doLog_KeySelection = value; }
        }

        public bool DoLog_UserLooksAtKey
        {
            get { return CSVLogService.Instance.doLog_UserLooksAtKey; }
            set { CSVLogService.Instance.doLog_UserLooksAtKey = value; }
        }

        public bool DoLog_MultiKeySelection
        {
            get { return CSVLogService.Instance.doLog_MultiKeySelection; }
            set { CSVLogService.Instance.doLog_MultiKeySelection = value; }
        }
        #endregion
    }
}
