using JuliusSweetland.OptiKey.Models;
using System;
using System.IO;
using System.Windows;
using TETCSharpClient.Data;

namespace JuliusSweetland.OptiKey.Services
{
    /// <summary>
    /// CSVLogService is a class made by Peter Øvergård Clausen, that is able to create a csv log file and write information to it based on OptiKey.
    /// </summary>
    public class CSVLogService
    {
        private bool doLog = false;                 //Change to true to log

        private readonly bool doLogGazeData = true;        //Change to true to log GazeData
        private readonly bool doLogScratchPadText = true;  //Change to true to log ScratchPadText
        private readonly bool doLogPhraseText = true;  //Change to true to log ScratchPadText
        private readonly bool doLogKeySelection = true;      //Change to true to log every key selection
        private readonly bool doLog_userLooksAtKey = true;  //Change to true to log when user looks in ScratchPad.
        private readonly bool doLog_multiKeySelection = false;

        private string optiKeyLogPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "OptiKeyLogs");
        private string logDirectoryForThisRun;
        private string fileFriendlyDate;

        private string gazeLogFilePath;             //File path for GazeLog-YYMMDDHHMMSS.csv
        private string scratchPadLogFilePath;       //File path for ScratchPadLog-YYMMDDHHMMSS.csv
        private string phraseLogFilePath;           //File path for PhraseLog-YYMMDDHHMMSS.csv
        private string keySelectionLog_FilePath;    //File path for KeyStrokesLog-YYMMDDHHMMSS.csv
        private string userLooksAtKey_LogFilePath;  //File path for UserLooksInScratchpadLog-YYMMDDHHMMSS.csv
        private string multiKeySelection_LogFilePath;

        #region Singleton pattern
        private static CSVLogService instance;

        public static CSVLogService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CSVLogService();
                }
                return instance;
            }
        }

        internal void SetOptiKeyLogPath(string path)
        {
            optiKeyLogPath = path;
        }

        public void StartLogging()
        {
            //Checks if Desktop directory exists:
            if (!Directory.Exists(optiKeyLogPath))
            {
                Directory.CreateDirectory(optiKeyLogPath);
                Console.WriteLine(optiKeyLogPath + " does not exists. Creating folder.");
                
            }
            //Creates a directory for all logs created this run:
            DateTime now = DateTime.Now;
            fileFriendlyDate = now.Year + "-" + now.Month + "-" + now.Day + "-" + now.Hour + "-" + now.Minute + "-" + now.Second;
            logDirectoryForThisRun = optiKeyLogPath + @"\" + fileFriendlyDate;
            Directory.CreateDirectory(logDirectoryForThisRun);

            if (doLogGazeData)
            {
                create_GazeLog();
            }
            if (doLogScratchPadText)
            {
                create_ScratchPadLog();
            }
            if (doLogPhraseText)
            {
                create_PhraseLog();
            }
            if (doLogKeySelection)
            {
                create_KeySelectionLog();
            }
            if (doLog_userLooksAtKey)
            {
                create_userLooksAtKey_Log();
            }
            if (doLog_multiKeySelection)
            {
                create_multiKeySelection_Log();
            }

            //Start logging:
            doLog = true;
        }
        #endregion

        #region Constructor
        private CSVLogService()  
        {

        }
        #endregion

        #region Create log files methods:
        private void create_GazeLog()
        {
            //Create log file:
            gazeLogFilePath = logDirectoryForThisRun + @"\GazeLog-" + fileFriendlyDate + ".csv";
            var file = File.Create(gazeLogFilePath);
            file.Close();

            //Writing first line:
            var firstLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},\n",
                "dataTimeStamp", "dataIsFixated", "dataState", "dataSmoothedCoordinateX", "dataSmoothedCoordinateY", "dataRawCoordiateX", "dataRawCoordiateY",
                "leftPupilCenterCoordinateX", "leftPupilCenterCoordinateY", "leftPupilSize", "leftRawCoordinateX", "leftRawCoordinateY", "leftSmoothedCoordinateX", "leftSmoothedCoordinateY",
                "rightPupilCenterCoordinateX", "rightPupilCenterCoordinateY", "rightPupilSize", "rightRawCoordinateX", "rightRawCoordinateY", "rightSmoothedCoordinateX", "rightSmoothedCoordinateY");
            File.AppendAllText(gazeLogFilePath, firstLine);
        }

        private void create_ScratchPadLog()
        {
            //Create log file:
            scratchPadLogFilePath = logDirectoryForThisRun + @"\ScratchPadLog-" + fileFriendlyDate + ".csv";
            var file = File.Create(scratchPadLogFilePath);
            file.Close();

            //Writing first line:
            var firstLine = string.Format("{0},{1}\n",
                "systemTimeStamp", "scratchPadText");
            File.AppendAllText(scratchPadLogFilePath, firstLine);
        }

        private void create_PhraseLog()
        {
            //Create log file:
            phraseLogFilePath = logDirectoryForThisRun + @"\PhraseLog-" + fileFriendlyDate + ".csv";
            var file = File.Create(phraseLogFilePath);
            file.Close();

            //Writing first line:
            var firstLine = string.Format("{0},{1}\n",
                "systemTimeStamp", "phraseText");
            File.AppendAllText(phraseLogFilePath, firstLine);
        }

        private void create_KeySelectionLog()
        {
            //Create log file:
            keySelectionLog_FilePath = logDirectoryForThisRun + @"\KeySelectionLog-" + fileFriendlyDate + ".csv";
            var file = File.Create(keySelectionLog_FilePath);
            file.Close();

            //Writing first line:
            var firstLine = string.Format("{0},{1}\n","systemTimeStamp", "keySelected");
            File.AppendAllText(keySelectionLog_FilePath, firstLine);
        }

        private void create_userLooksAtKey_Log()
        {
            //Create log file:
            userLooksAtKey_LogFilePath = logDirectoryForThisRun + @"\user_looks_at_key_log-" + fileFriendlyDate + ".csv";
            var file = File.Create(userLooksAtKey_LogFilePath);
            file.Close();

            //Writing first line:
            var firstLine = string.Format("{0},{1},{2}\n", "systemTimeStamp", "key", "progressInPercent");
            File.AppendAllText(userLooksAtKey_LogFilePath, firstLine);
        }

        private void create_multiKeySelection_Log()
        {
            //Create log file:
            multiKeySelection_LogFilePath = logDirectoryForThisRun + @"\multiKeySelectionLog-" + fileFriendlyDate + ".csv";
            var file = File.Create(multiKeySelection_LogFilePath);
            file.Close();

            //Writing first line:
            var firstLine = string.Format("{0},{1}\n", "systemTimeStamp", "key(s)");
            File.AppendAllText(multiKeySelection_LogFilePath, firstLine);
        }

        #endregion

        #region Logging methods:
        /// <summary>
        /// Logs GazeData from Services/TheEyeTribePointService.cs
        /// </summary>
        /// <param name="data"></param>
        public void Log_Gazedata(GazeData data)
        {
            if (doLog && doLogGazeData)
            {
                //Getting system datetime:
                string systemTimeStamp = getNowAsString();

                //data data:
                string dataTimeStamp = data.TimeStampString;
                bool dataIsFixated = data.IsFixated;
                int dataState = data.State;
                double dataSmoothedCoordinateX = data.SmoothedCoordinates.X;
                double dataSmoothedCoordinateY = data.SmoothedCoordinates.Y;
                double dataRawCoordiateX = data.RawCoordinates.X;
                double dataRawCoordiateY = data.RawCoordinates.Y;

                //data.LeftEye data:
                double leftPupilCenterCoordinateX = data.LeftEye.PupilCenterCoordinates.X;
                double leftPupilCenterCoordinateY = data.LeftEye.PupilCenterCoordinates.Y;
                double leftPupilSize = data.LeftEye.PupilSize;
                double leftRawCoordinateX = data.LeftEye.RawCoordinates.X;
                double leftRawCoordinateY = data.LeftEye.RawCoordinates.Y;
                double leftSmoothedCoordinateX = data.LeftEye.SmoothedCoordinates.X;
                double leftSmoothedCoordinateY = data.LeftEye.SmoothedCoordinates.Y;

                //data.RightEye data:
                double rightPupilCenterCoordinateX = data.RightEye.PupilCenterCoordinates.X;
                double rightPupilCenterCoordinateY = data.RightEye.PupilCenterCoordinates.Y;
                double rightPupilSize = data.RightEye.PupilSize;
                double rightRawCoordinateX = data.RightEye.RawCoordinates.X;
                double rightRawCoordinateY = data.RightEye.RawCoordinates.Y;
                double rightSmoothedCoordinateX = data.RightEye.SmoothedCoordinates.X;
                double rightSmoothedCoordinateY = data.RightEye.SmoothedCoordinates.Y;

                //Creating new line:
                var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}\n",
                    systemTimeStamp, dataTimeStamp, dataIsFixated, dataState, dataSmoothedCoordinateX, dataSmoothedCoordinateY, dataRawCoordiateX, dataRawCoordiateY,
                    leftPupilCenterCoordinateX, leftPupilCenterCoordinateY, leftPupilSize, leftRawCoordinateX, leftRawCoordinateY, leftSmoothedCoordinateX, leftSmoothedCoordinateY,
                    rightPupilCenterCoordinateX, rightPupilCenterCoordinateY, rightPupilSize, rightRawCoordinateX, rightRawCoordinateY, rightSmoothedCoordinateX, rightSmoothedCoordinateY);

                //Log data:
                File.AppendAllText(gazeLogFilePath, newLine);
            }
        }

        /// <summary>
        /// Logs value to scratchPadLog from Services/KeyboardOutputService.cs
        /// </summary>
        /// <param name="value"></param>
        public void Log_ScratchPadText(string value)
        {
            if (doLog && doLogScratchPadText)
            {
                var newLine = string.Format("{0},{1}\n", getNowAsString(), value);
                File.AppendAllText(scratchPadLogFilePath, newLine);
            }
        }

        /// <summary>
        /// Logs value to PhraseLog from ui/ValueConverters/PhreaseIndexed.cs
        /// </summary>
        /// <param name="value"></param>
        public void Log_PhraseText(string value)
        {   
            if (doLog && doLogPhraseText)
            {
                var newLine = string.Format("{0},{1}\n", getNowAsString(), value);
                File.AppendAllText(phraseLogFilePath, newLine);
            }  
        }

        /// <summary>
        /// Logs key selections from UI/ViewModels/MainViewModel.ServiceEventHandlers
        /// </summary>
        /// <param name="keySelection"></param>
        public void Log_KeySelection(string keySelection)
        {
            if (doLog && doLogKeySelection)
            {
                var newLine = string.Format("{0},{1}\n", getNowAsString(), keySelection);
                File.AppendAllText(keySelectionLog_FilePath, newLine);
            }   
        }

        /// <summary>
        /// Logs key progression (When user looks at a key, and the progression counts up).
        /// Called from UI/ViewModels/MainViewModel.ServiceEventHandlers.cs
        /// </summary>
        /// <param name="key"></param>
        /// <param name="progress"></param>
        public void Log_KeyProgression(string key, double progress)
        {
            if(doLog && doLog_userLooksAtKey)
            {
                var newLine = string.Format("{0},{1},{2}\n", getNowAsString(), key, progress);
                //Log data:
                File.AppendAllText(userLooksAtKey_LogFilePath, newLine);
            }
        }

        /// <summary>
        /// Logs MultiKey selections.
        /// Called from UI/ViewModels/MainViewModel.ServiceEventHandlers.cs and Services/KeyboardOutputService.cs
        /// </summary>
        /// <param name="keySelection"></param>
        public void Log_MultiKeySelection(string keySelection)
        {
            if(doLog && doLog_multiKeySelection)
            {
                //Log data:
                var newLine = string.Format("{0},{1}\n", getNowAsString(), keySelection);
                File.AppendAllText(multiKeySelection_LogFilePath, newLine);
            }
        }
        #endregion

        #region helperMethods

        private string getNowAsString()
        {
            DateTime now = DateTime.Now;
            return "" +
                now.Year + "-" +
                now.Month + "-" +
                now.Day + "-" +
                now.Hour + "-" +
                now.Minute + "-" +
                now.Second + "-" +
                now.Millisecond;
        }

        #endregion
    }
}
