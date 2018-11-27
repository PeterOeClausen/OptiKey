using JuliusSweetland.OptiKey.Models;
using JuliusSweetland.OptiKey.Properties;
using System;
using System.IO;
using System.Text;
using System.Windows;
using TETCSharpClient.Data;
using Tobii.Research;

namespace JuliusSweetland.OptiKey.Services
{
    /// <summary>
    /// CSVLogService is a class made by Peter Øvergård Clausen, that is able to create a csv log file and write information to it based on OptiKey.
    /// </summary>
    public class CSVLogService
    {
        #region Fields
        private bool doLog = false;                 //Change to true to log

        public bool doLog_EyeTribeGazeData = Settings.Default.doLog_EyeTribeGazeData;
        public bool doLog_TobiiGazeData = Settings.Default.doLog_TobiiGazeData;
        public bool doLog_ScratchPadText = Settings.Default.doLog_ScratchPadText;
        public bool doLog_PhraseText = Settings.Default.doLog_PhraseText;
        public bool doLog_KeySelection = Settings.Default.doLog_KeySelection;
        public bool doLog_UserLooksAtKey = Settings.Default.doLog_UserLooksAtKey;
        public bool doLog_MultiKeySelection = Settings.Default.doLog_MultiKeySelection;
        
        private string logDirectoryForThisRun;
        private string fileFriendlyDate;
        
        StreamWriter gazeLogWriter;
        StreamWriter scratchPadLogWriter;
        StreamWriter phraseLogWriter;
        StreamWriter keySelectionLogWriter;
        StreamWriter userLooksAtKeyLogWriter;
        StreamWriter multiKeySelectionLogWriter;
        StreamWriter tobiiGazeLogWriter;
        #endregion

        #region Properties
        public string OptiKeyLogPath
        {
            get
            {
                string logPathFromSettings = Settings.Default.ExperimentMenu_OptiKeyLogPath;
                if (!Directory.Exists(logPathFromSettings))
                {
                    //Reset to desktop:
                    Settings.Default.ExperimentMenu_OptiKeyLogPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "OptiKeyLogs");
                    logPathFromSettings = Settings.Default.ExperimentMenu_OptiKeyLogPath;
                }
                return logPathFromSettings;
            }
            set
            {
                string path = value;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                Settings.Default.ExperimentMenu_OptiKeyLogPath = path;
            }
        }

        #endregion

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

            //Creates logs if they are needed:
            if (doLog_EyeTribeGazeData)
            {
                create_GazeLog();
            }
            if (doLog_ScratchPadText)
            {
                create_ScratchPadLog();
            }
            if (doLog_PhraseText)
            {
                create_PhraseLog();
            }
            if (doLog_KeySelection)
            {
                create_KeySelectionLog();
            }
            if (doLog_UserLooksAtKey)
            {
                create_userLooksAtKey_Log();
            }
            if (doLog_MultiKeySelection)
            {
                create_multiKeySelection_Log();
            }
            if (doLog_TobiiGazeData)
            {
                create_TobiiGazeLog();
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
            if (tobiiGazeLogWriter != null)
            {
                tobiiGazeLogWriter.Flush();
                tobiiGazeLogWriter.Close();
            }
        }
        #endregion

        #region Constructor
        private CSVLogService()  
        {
            Application.Current.Exit += (sender, args) => { StopLogging(); };
        }
        #endregion

        #region Create log files methods:
        private void create_GazeLog()
        {
            //Create log file:
            string eyeTribeGazeLog_FilePath = logDirectoryForThisRun + @"\GazeLog-" + fileFriendlyDate + ".csv";
            gazeLogWriter = new StreamWriter(eyeTribeGazeLog_FilePath, false, Encoding.UTF8);

            //Writing first line:
            var firstLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}",
                "systemTimeStamp", "dataTimeStamp", "dataIsFixated", "dataState", "dataSmoothedCoordinateX", "dataSmoothedCoordinateY", "dataRawCoordiateX", "dataRawCoordiateY",
                "leftPupilCenterCoordinateX", "leftPupilCenterCoordinateY", "leftPupilSize", "leftRawCoordinateX", "leftRawCoordinateY", "leftSmoothedCoordinateX", 
                "leftSmoothedCoordinateY", "rightPupilCenterCoordinateX", "rightPupilCenterCoordinateY", "rightPupilSize", "rightRawCoordinateX", "rightRawCoordinateY", 
                "rightSmoothedCoordinateX", "rightSmoothedCoordinateY");
            gazeLogWriter.WriteLine(firstLine);
        }

        private void create_ScratchPadLog()
        {
            //Create log file:
            string scratchPadLog_FilePath = logDirectoryForThisRun + @"\ScratchPadLog-" + fileFriendlyDate + ".csv";
            scratchPadLogWriter = new StreamWriter(scratchPadLog_FilePath, false, Encoding.UTF8);

            //Writing first line:
            var firstLine = string.Format("{0},{1}",
                "systemTimeStamp", "scratchPadText");
            scratchPadLogWriter.WriteLine(firstLine);
        }

        private void create_PhraseLog()
        {
            //Create log file:
            string phraseLog_FilePath = logDirectoryForThisRun + @"\PhraseLog-" + fileFriendlyDate + ".csv";
            phraseLogWriter = new StreamWriter(phraseLog_FilePath, false, Encoding.UTF8);

            //Writing first line:
            var firstLine = string.Format("{0},{1}",
                "systemTimeStamp", "phraseText");
            phraseLogWriter.WriteLine(firstLine);
        }

        private void create_KeySelectionLog()
        {
            //Create log file:
            string keySelectionLog_FilePath = logDirectoryForThisRun + @"\KeySelectionLog-" + fileFriendlyDate + ".csv";
            keySelectionLogWriter = new StreamWriter(keySelectionLog_FilePath, false, Encoding.UTF8);

            //Writing first line:
            var firstLine = string.Format("{0},{1}","systemTimeStamp", "keySelected");
            keySelectionLogWriter.WriteLine(firstLine);
        }

        private void create_userLooksAtKey_Log()
        {
            //Create log file:
            string userLooksAtKeyLog_FilePath = logDirectoryForThisRun + @"\user_looks_at_key_log-" + fileFriendlyDate + ".csv";
            userLooksAtKeyLogWriter = new StreamWriter(userLooksAtKeyLog_FilePath, false, Encoding.UTF8);

            //Writing first line:
            var firstLine = string.Format("{0},{1},{2}", "systemTimeStamp", "key", "progressInPercent");
            userLooksAtKeyLogWriter.WriteLine(firstLine);
        }

        private void create_multiKeySelection_Log()
        {
            //Create log file:
            string multiKeySelectionLog_FilePath = logDirectoryForThisRun + @"\multiKeySelectionLog-" + fileFriendlyDate + ".csv";
            multiKeySelectionLogWriter = new StreamWriter(multiKeySelectionLog_FilePath, false, Encoding.UTF8);

            //Writing first line:
            var firstLine = string.Format("{0},{1}", "systemTimeStamp", "key(s)");
            multiKeySelectionLogWriter.WriteLine(firstLine);
        }

        private void create_TobiiGazeLog()
        {
            //Create log file:
            string tobiiGazeLog_FilePath = logDirectoryForThisRun + @"\tobiiGazeLog-" + fileFriendlyDate + ".csv";
            tobiiGazeLogWriter = new StreamWriter(tobiiGazeLog_FilePath, false, Encoding.UTF8);

            //Writing first line:

            var firstLine = String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31}",
                "SystemTimeStamp", "DeviceTimeStamp", "LeftGazeOriginValidity", "LeftGazeOriginUCSx", "LeftGazeOriginUCSy", "LeftGazeOriginUCSz", "LeftGazeOriginTBCSx", "LeftGazeOriginTBCSy", "LeftGazeOriginTBCSz",
                "RightGazeOriginValidity", "RightGazeOriginUCSx", "RightGazeOriginUCSy", "RightGazeOriginUCSz", "RightGazeOriginTBCSx", "RightGazeOriginTBCSy", "RightGazeOriginTBCSz",
                "LeftGazePointValidity", "LeftGazePointUCSx", "LeftGazePointUCSy", "LeftGazePointUCSz", "LeftGazePointADCSx", "LeftGazePointADCSy",
                "RightGazePointValidity", "RightGazePointUCSx", "RightGazePointUCSy", "RightGazePointUCSz", "RightGazePointADCSx", "RightGazePointADCSy",
                "LeftPupilValidity", "LeftPupilDiameter", "RightPupilValidity", "RightPupilDiameter");
            tobiiGazeLogWriter.WriteLine(firstLine);
        }

        #endregion

        #region Logging methods:
        /// <summary>
        /// Logs GazeData from Services/TheEyeTribePointService.cs
        /// </summary>
        /// <param name="data"></param>
        public void Log_EyeTribeGazedata(GazeData data)
        {
            if (doLog && doLog_EyeTribeGazeData)
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
                var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}",
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
            if (doLog && doLog_ScratchPadText)
            {
                var newLine = string.Format("{0},{1}", getNowAsString(), value);
                scratchPadLogWriter.WriteLine(newLine);
            }
        }

        /// <summary>
        /// Logs value to PhraseLog from ui/ValueConverters/PhreaseIndexed.cs
        /// </summary>
        /// <param name="value"></param>
        public void Log_PhraseText(string value)
        {   
            if (doLog && doLog_PhraseText)
            {
                var newLine = string.Format("{0},{1}", getNowAsString(), value);
                phraseLogWriter.WriteLine(newLine);
            }  
        }

        /// <summary>
        /// Logs key selections from UI/ViewModels/MainViewModel.ServiceEventHandlers
        /// </summary>
        /// <param name="keySelection"></param>
        public void Log_KeySelection(string keySelection)
        {
            if (doLog && doLog_KeySelection)
            {
                var newLine = string.Format("{0},{1}", getNowAsString(), keySelection);
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
            if(doLog && doLog_UserLooksAtKey)
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
            if(doLog && doLog_MultiKeySelection)
            {
                //Log data:
                var newLine = string.Format("{0},{1}", getNowAsString(), keySelection);
                multiKeySelectionLogWriter.WriteLine(newLine);
            }
        }

        public void Log_TobiiGazeData(GazeDataEventArgs data)
        {
            if (doLog && doLog_TobiiGazeData)
            {
                var newline = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30},{31}",
                    DateTime.Now.ToString("o"),
                    data.DeviceTimeStamp,
                    data.LeftEye.GazeOrigin.Validity,
                    data.LeftEye.GazeOrigin.PositionInUserCoordinates.X,
                    data.LeftEye.GazeOrigin.PositionInUserCoordinates.Y,
                    data.LeftEye.GazeOrigin.PositionInUserCoordinates.Z,
                    data.LeftEye.GazeOrigin.PositionInTrackBoxCoordinates.X,
                    data.LeftEye.GazeOrigin.PositionInTrackBoxCoordinates.Y,
                    data.LeftEye.GazeOrigin.PositionInTrackBoxCoordinates.Z,
                    data.RightEye.GazeOrigin.Validity,
                    data.RightEye.GazeOrigin.PositionInUserCoordinates.X,
                    data.RightEye.GazeOrigin.PositionInUserCoordinates.Y,
                    data.RightEye.GazeOrigin.PositionInUserCoordinates.Z,
                    data.RightEye.GazeOrigin.PositionInTrackBoxCoordinates.X,
                    data.RightEye.GazeOrigin.PositionInTrackBoxCoordinates.Y,
                    data.RightEye.GazeOrigin.PositionInTrackBoxCoordinates.Z,
                    data.LeftEye.GazePoint.Validity,
                    data.LeftEye.GazePoint.PositionInUserCoordinates.X,
                    data.LeftEye.GazePoint.PositionInUserCoordinates.Y,
                    data.LeftEye.GazePoint.PositionInUserCoordinates.Z,
                    data.LeftEye.GazePoint.PositionOnDisplayArea.X,
                    data.LeftEye.GazePoint.PositionOnDisplayArea.Y,
                    data.RightEye.GazePoint.Validity,
                    data.RightEye.GazePoint.PositionInUserCoordinates.X,
                    data.RightEye.GazePoint.PositionInUserCoordinates.Y,
                    data.RightEye.GazePoint.PositionInUserCoordinates.Z,
                    data.RightEye.GazePoint.PositionOnDisplayArea.X,
                    data.RightEye.GazePoint.PositionOnDisplayArea.Y,
                    data.LeftEye.Pupil.Validity,
                    data.LeftEye.Pupil.PupilDiameter,
                    data.RightEye.Pupil.Validity,
                    data.RightEye.Pupil.PupilDiameter);

                tobiiGazeLogWriter.WriteLine(newline);
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
