using JuliusSweetland.OptiKey.Models;
using JuliusSweetland.OptiKey.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
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

        public bool doLogGazeData = Settings.Default.doLogGazeData;
        public bool doLogScratchPadText = Settings.Default.doLogScratchPadText;
        public bool doLogPhraseText = Settings.Default.doLogPhraseText;
        public bool doLogKeySelection = Settings.Default.doLogKeySelection;
        public bool doLog_userLooksAtKey = Settings.Default.doLog_userLooksAtKey;
        public bool doLog_multiKeySelection = Settings.Default.doLog_multiKeySelection;
        
        //private string optiKeyLogPath = 
        public string OptiKeyLogPath {
            get {
                string logPathFromSettings = Settings.Default.ExperimentMenu_OptiKeyLogPath;
                if(!Directory.Exists(logPathFromSettings))
                {
                    //Reset to desktop:
                    Settings.Default.ExperimentMenu_OptiKeyLogPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "OptiKeyLogs");
                    logPathFromSettings = Settings.Default.ExperimentMenu_OptiKeyLogPath;
                }
                return logPathFromSettings;
            }
            set{
                string path = value;
                if(!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                
                Settings.Default.ExperimentMenu_OptiKeyLogPath = path;
            }
        }
        private string logDirectoryForThisRun;
        private string fileFriendlyDate;

        StreamWriter gazeLogWriter;
        StreamWriter scratchPadLogWriter;
        StreamWriter phraseLogWriter;
        StreamWriter keySelectionLogWriter;
        StreamWriter userLooksAtKeyLogWriter;
        StreamWriter multiKeySelectionLogWriter;

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

        public void StartLogging()
        {
            if (!Directory.Exists(OptiKeyLogPath))
            {
                Directory.CreateDirectory(OptiKeyLogPath);
                Console.WriteLine(OptiKeyLogPath + " does not exists. Creating folder.");
            }
            //Creates a directory for all logs created this run:
            DateTime now = DateTime.Now;
            fileFriendlyDate = now.Year + "-" + now.Month + "-" + now.Day + "-" + now.Hour + "-" + now.Minute + "-" + now.Second;
            logDirectoryForThisRun = OptiKeyLogPath + @"\" + fileFriendlyDate;
            Directory.CreateDirectory(logDirectoryForThisRun);

            // Initiate a list to acrue the data
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

        public void StopLogging()
        {
            Console.WriteLine("Stop logging function is entered");
            doLog = false;
            if (gazeLogWriter != null)
            {
                gazeLogWriter.Flush();
                gazeLogWriter.Close();
            }
            if (scratchPadLogWriter != null)
            {
                scratchPadLogWriter.Flush();
                scratchPadLogWriter.Close();
            }
            if (phraseLogWriter != null)
            {
                phraseLogWriter.Flush();
                phraseLogWriter.Close();
            }
            if (keySelectionLogWriter != null)
            {
                keySelectionLogWriter.Flush();
                keySelectionLogWriter.Close();
            }
            if (userLooksAtKeyLogWriter != null)
            {
                userLooksAtKeyLogWriter.Flush();
                userLooksAtKeyLogWriter.Close();
            }
            if (multiKeySelectionLogWriter != null)
            {
                multiKeySelectionLogWriter.Flush();
                multiKeySelectionLogWriter.Close();
            }
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
            string gazeLogFilePath = logDirectoryForThisRun + @"\GazeLog-" + fileFriendlyDate + ".csv";
            gazeLogWriter = new StreamWriter(gazeLogFilePath, false, Encoding.UTF8);

            //Writing first line:
            var firstLine = String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}",
                "systemTimeStamp", "dataTimeStamp", "dataIsFixated", "dataState", "dataSmoothedCoordinateX", "dataSmoothedCoordinateY", "dataRawCoordiateX", "dataRawCoordiateY",
                "leftPupilCenterCoordinateX", "leftPupilCenterCoordinateY", "leftPupilSize", "leftRawCoordinateX", "leftRawCoordinateY", "leftSmoothedCoordinateX", 
                "leftSmoothedCoordinateY", "rightPupilCenterCoordinateX", "rightPupilCenterCoordinateY", "rightPupilSize", "rightRawCoordinateX", "rightRawCoordinateY", 
                "rightSmoothedCoordinateX", "rightSmoothedCoordinateY");
            gazeLogWriter.WriteLine(firstLine);
        }

        private void create_ScratchPadLog()
        {
            //Create log file:
            string scratchPadLogFilePath = logDirectoryForThisRun + @"\ScratchPadLog-" + fileFriendlyDate + ".csv";
            scratchPadLogWriter = new StreamWriter(scratchPadLogFilePath, false, Encoding.UTF8);

            //Writing first line:
            var firstLine = String.Format("{0},{1}",
                "systemTimeStamp", "scratchPadText");
            scratchPadLogWriter.WriteLine(firstLine);
        }

        private void create_PhraseLog()
        {
            //Create log file:
            string phraseLogFilePath = logDirectoryForThisRun + @"\PhraseLog-" + fileFriendlyDate + ".csv";
            phraseLogWriter = new StreamWriter(phraseLogFilePath, false, Encoding.UTF8);

            //Writing first line:
            var firstLine = string.Format("{0},{1}",
                "systemTimeStamp", "phraseText");
            phraseLogWriter.WriteLine(firstLine);
        }

        private void create_KeySelectionLog()
        {
            //Create log file:
            string keySelectionLogFilePath = logDirectoryForThisRun + @"\key_selection_log-" + fileFriendlyDate + ".csv";
            keySelectionLogWriter = new StreamWriter(keySelectionLogFilePath, false, Encoding.UTF8);

            //Writing first line:
            var firstLine = string.Format("{0},{1},{2}", "systemTimeStamp", "key", "progressInPercent");
            keySelectionLogWriter.WriteLine(firstLine);
        }

        private void create_userLooksAtKey_Log()
        {
            //Create log file:
            string userLooksAtKeyLogFilePath = logDirectoryForThisRun + @"\user_looks_at_key_log-" + fileFriendlyDate + ".csv";
            userLooksAtKeyLogWriter = new StreamWriter(userLooksAtKeyLogFilePath, false, Encoding.UTF8);

            //Writing first line:
            var firstLine = string.Format("{0},{1},{2}", "systemTimeStamp", "key", "progressInPercent");
            userLooksAtKeyLogWriter.WriteLine(firstLine);
        }

        private void create_multiKeySelection_Log()
        {
            //Create log file:
            string multiKeySelectionLogFilePath = logDirectoryForThisRun + @"\multiKeySelectionLog-" + fileFriendlyDate + ".csv";
            multiKeySelectionLogWriter = new StreamWriter(multiKeySelectionLogFilePath, false, Encoding.UTF8);

            //Writing first line:
            var firstLine = string.Format("{0},{1}", "systemTimeStamp", "key(s)");
            multiKeySelectionLogWriter.WriteLine(firstLine);
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
                var newLine = String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}",
                    systemTimeStamp, dataTimeStamp, dataIsFixated, dataState, dataSmoothedCoordinateX, dataSmoothedCoordinateY, dataRawCoordiateX, dataRawCoordiateY,
                    leftPupilCenterCoordinateX, leftPupilCenterCoordinateY, leftPupilSize, leftRawCoordinateX, leftRawCoordinateY, leftSmoothedCoordinateX, leftSmoothedCoordinateY,
                    rightPupilCenterCoordinateX, rightPupilCenterCoordinateY, rightPupilSize, rightRawCoordinateX, rightRawCoordinateY, rightSmoothedCoordinateX, rightSmoothedCoordinateY);

                //Log data:
                gazeLogWriter.WriteLine(newLine);
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
                var newLine = string.Format("{0},{1}", getNowAsString(), value);
                //Log data:
                scratchPadLogWriter.WriteLine(newLine);
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
                var newLine = string.Format("{0},{1}", getNowAsString(), value);
                //Log data:
                phraseLogWriter.WriteLine(newLine);
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
                var newLine = string.Format("{0},{1}", getNowAsString(), keySelection);
                //Log data:
                keySelectionLogWriter.WriteLine(newLine);
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
                var newLine = string.Format("{0},{1},{2}", getNowAsString(), key, progress);
                //Log data:
                userLooksAtKeyLogWriter.WriteLine(newLine);
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
                var newLine = string.Format("{0},{1}", getNowAsString(), keySelection);
                //Log data:
                multiKeySelectionLogWriter.WriteLine(newLine);
            }
        }
        #endregion

        #region helperMethods

        private string getNowAsString()
        {
            return DateTime.Now.ToString("o");
        }

        #endregion
    }
}
