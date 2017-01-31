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
        private string filePath;

        public CSVLogService()  
        {
            //Create log file:
            DateTime now = DateTime.Now;
            string fileFriendlyDate = now.Year + "-" + now.Month + "-" + now.Day + "-" + now.Hour + "-" + now.Minute + "-" + now.Second;
            filePath = @"C:\Users\PeterOeC\Desktop\loginhere\log-" + fileFriendlyDate + ".csv";
            var file = File.Create(filePath);
            file.Close();

            //Writing first line:
            var firstLine = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},\n",
                "dataTimeStamp", "dataIsFixated", "dataState", "dataSmoothedCoordinateX", "dataSmoothedCoordinateY", "dataRawCoordiateX", "dataRawCoordiateY",
                "leftPupilCenterCoordinateX", "leftPupilCenterCoordinateY", "leftPupilSize", "leftRawCoordinateX", "leftRawCoordinateY", "leftSmoothedCoordinateX", "leftSmoothedCoordinateY",
                "rightPupilCenterCoordinateX", "rightPupilCenterCoordinateY", "rightPupilSize", "rightRawCoordinateX", "rightRawCoordinateY", "rightSmoothedCoordinateX", "rightSmoothedCoordinateY");
            File.AppendAllText(filePath, firstLine);
        }

        /// <summary>
        /// Logs GazeData from TheEyeTribePointService
        /// </summary>
        /// <param name="data"></param>
        public void logGazedata(GazeData data)
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
            File.AppendAllText(filePath, newLine);
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
            File.AppendAllText(filePath, newLine);
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
                File.AppendAllText(filePath, newLine);
            }
        }
    }
}
