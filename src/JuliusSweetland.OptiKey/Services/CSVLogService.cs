using JuliusSweetland.OptiKey.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TETCSharpClient.Data;

namespace JuliusSweetland.OptiKey.Services
{
    /// <summary>
    /// CSVLogService is a class made by Peter Øvergård Clausen, that is able to create a csv log file and write information to it based on OptiKey.
    /// </summary>
    public class CSVLogService
    {
        private string gazeLogFilePath;
        private string scratchPadLogFilePath;

        private readonly bool doLogGazeData = false; //Change to true to log GazeData
        private readonly bool doLogScratchPadText = false; //Change to true to log ScratchPadText

        #region Singleton
        private static CSVLogService instance;

        public static CSVLogService Instance
        {
            get
            {
                if (instance == null)
                {
                    if (instance == null)
                    {
                        instance = new CSVLogService();
                    }
                }
                return instance;
            }
        }
        #endregion

        #region Constructor
        private CSVLogService()  
        {
            if(doLogGazeData)
            {
                createGazeLog();
            }       
            if(doLogScratchPadText)
            {
                createScratchPadLog();
            }
        }
        #endregion

        private void createGazeLog()
        {
            //Create log file:
            DateTime now = DateTime.Now;
            string fileFriendlyDate = now.Year + "-" + now.Month + "-" + now.Day + "-" + now.Hour + "-" + now.Minute + "-" + now.Second;
            gazeLogFilePath = @"C:\Users\PeterOeC\Desktop\loginhere\GazeLog-" + fileFriendlyDate + ".csv";
            var file = File.Create(gazeLogFilePath);
            file.Close();

            //Writing first line:
            var firstLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},\n",
                "dataTimeStamp", "dataIsFixated", "dataState", "dataSmoothedCoordinateX", "dataSmoothedCoordinateY", "dataRawCoordiateX", "dataRawCoordiateY",
                "leftPupilCenterCoordinateX", "leftPupilCenterCoordinateY", "leftPupilSize", "leftRawCoordinateX", "leftRawCoordinateY", "leftSmoothedCoordinateX", "leftSmoothedCoordinateY",
                "rightPupilCenterCoordinateX", "rightPupilCenterCoordinateY", "rightPupilSize", "rightRawCoordinateX", "rightRawCoordinateY", "rightSmoothedCoordinateX", "rightSmoothedCoordinateY");
            File.AppendAllText(gazeLogFilePath, firstLine);
        }

        private void createScratchPadLog()
        {
            //Create log file:
            DateTime now = DateTime.Now;
            string fileFriendlyDate = now.Year + "-" + now.Month + "-" + now.Day + "-" + now.Hour + "-" + now.Minute + "-" + now.Second;
            scratchPadLogFilePath = @"C:\Users\PeterOeC\Desktop\loginhere\ScratchPadLog-" + fileFriendlyDate + ".csv";
            var file = File.Create(scratchPadLogFilePath);
            file.Close();

            //Writing first line:
            var firstLine = string.Format("{0},{1}\n",
                "systemTimeStamp", "scratchPadText");
            File.AppendAllText(scratchPadLogFilePath, firstLine);
        }

        /// <summary>
        /// Logs GazeData from TheEyeTribePointService
        /// </summary>
        /// <param name="data"></param>
        public void logGazedata(GazeData data)
        {
            if (doLogGazeData)
            {
                //data data
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
                var newLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},\n",
                    dataTimeStamp, dataIsFixated, dataState, dataSmoothedCoordinateX, dataSmoothedCoordinateY, dataRawCoordiateX, dataRawCoordiateY,
                    leftPupilCenterCoordinateX, leftPupilCenterCoordinateY, leftPupilSize, leftRawCoordinateX, leftRawCoordinateY, leftSmoothedCoordinateX, leftSmoothedCoordinateY,
                    rightPupilCenterCoordinateX, rightPupilCenterCoordinateY, rightPupilSize, rightRawCoordinateX, rightRawCoordinateY, rightSmoothedCoordinateX, rightSmoothedCoordinateY);

                //Log data:
                File.AppendAllText(gazeLogFilePath, newLine);
            }
        }

        /// <summary>
        /// Logs value to scratchPadLog.
        /// </summary>
        /// <param name="value"></param>
        public void logScratchPadText(string value)
        {
            if(doLogScratchPadText)
            { 
                var newLine = string.Format("{0},{1}\n",
                    DateTime.Now.ToString(), value);
                File.AppendAllText(scratchPadLogFilePath, newLine);
            }
        }

        /// <summary>
        /// Logs the current position óf EyeTracker or mouse position.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="pointKeyValuePair"></param>
        public void logCurrentPosition(object o, Tuple<Point, KeyValue?> pointKeyValuePair)
        {
            var newLine = string.Format("{0},,,{1},{2},{3}\n", DateTime.Now.ToString(), pointKeyValuePair.Item1.X, pointKeyValuePair.Item1.Y, pointKeyValuePair.Item2.ToString());
            //Log data:
            File.AppendAllText(gazeLogFilePath, newLine);
        }

        /// <summary>
        /// Logs the selected key.
        /// </summary>
        /// <param name="o"></param>
        /// <param name="pakv"></param>
        public void logSelection(object o, PointAndKeyValue pakv)
        {
            if (pakv.KeyValue.HasValue)
            {
                var newLine = string.Format("{0},{1},{2}\n", DateTime.Now.ToString(), pakv.String, pakv.KeyValue.Value.FunctionKey);
                //Log data:
                File.AppendAllText(gazeLogFilePath, newLine);
            }
        }
    }
}
