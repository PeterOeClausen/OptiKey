using System;
using System.Reactive;
using System.Windows;
using EyeXFramework;
using JuliusSweetland.OptiKey.Enums;
using log4net;
using Tobii.EyeX.Client;
using Tobii.EyeX.Framework;
using JuliusSweetland.OptiKey.Properties;
using Tobii.Research;
using System.Linq;

namespace JuliusSweetland.OptiKey.Services
{
    public class TobiiEyeXPointService : IPointService
    {
        #region Fields

        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private GazePointDataStream gazeDataStream;
        private FixationDataStream fixationDataStream;

        private event EventHandler<Timestamped<Point>> pointEvent;

        static readonly string TobiiLicense = @"C:\DTU\Github\TobiiLicense.txt";

        #endregion

        #region Ctor

        public TobiiEyeXPointService()
        {
            KalmanFilterSupported = true;
            EyeXHost = new EyeXHost();

            //Disconnect (deactivate) from the TET server on shutdown - otherwise the process can hang
            Application.Current.Exit += (sender, args) =>
            {
                if (EyeXHost != null)
                {
                    Log.Info("Disposing of the EyeXHost.");
                    EyeXHost.Dispose();
                    EyeXHost = null;
                }
            };

            //Initializing Tobii research framework in order to log gaze data:
            ResearchEyeTracker = EyeTrackingOperations.FindAllEyeTrackers().FirstOrDefault();

            // If the license is successfully applied:
            ResearchLicenseWasSucessfullyApplied = ApplyLicense(ResearchEyeTracker, TobiiLicense);
            if (ResearchLicenseWasSucessfullyApplied)
            {
                Console.WriteLine("Applied research license sucessfully.");
                ResearchEyeTracker.GazeDataReceived += OnGazeUpdate;
                ResearchEyeTracker.ConnectionLost += (s, e) => Console.WriteLine("Connection to the Tobii tracker was lost!");
                ResearchEyeTracker.ConnectionRestored += (s, e) => Console.WriteLine("Connection to the Tobii tracker restored!");
                ResearchEyeTracker.DeviceFaults += (s, e) => Console.WriteLine("There was a device fault with the Tobii tracker:\n" + e.Faults);
                ResearchEyeTracker.DeviceWarnings += (s, e) => Console.WriteLine("There was a device warning from the Tobii tracker:\n" + e.Warnings);
                Application.Current.Exit += (sender, args) =>
                {
                    if (ResearchEyeTracker != null)
                    {
                        Log.Info("Disposing of the ResearchEyeTracker.");
                        ResearchEyeTracker.Dispose();
                        ResearchEyeTracker = null;
                    }
                };
            }
            else
            {
                Console.WriteLine("Could not apply research license.");
            }
        }

        #endregion

        #region Properties

        public bool KalmanFilterSupported {get; private set; }
        public EyeXHost EyeXHost { get; private set; }
        public IEyeTracker ResearchEyeTracker { get; private set; }
        public bool ResearchLicenseWasSucessfullyApplied { get; private set; }

        #endregion

        #region Events

        public event EventHandler<Exception> Error;

        public event EventHandler<Timestamped<Point>> Point
        {
            add
            {
                if (pointEvent == null)
                {
                    Log.Info("Checking the state of the Tobii service...");

                    switch (EyeXHost.EyeXAvailability)
                    {
                        case EyeXAvailability.NotAvailable:
                            PublishError(this, new ApplicationException(Resources.TOBII_EYEX_ENGINE_NOT_FOUND));
                            return;

                        case EyeXAvailability.NotRunning:
                            PublishError(this, new ApplicationException(Resources.TOBII_EYEX_ENGINE_NOT_RUNNING));
                            return;
                    }

                    Log.Info("Attaching eye tracking device status changed listener to the Tobii service.");

                    EyeXHost.EyeTrackingDeviceStatusChanged += (s, e) => Log.InfoFormat("Tobii EyeX tracking device status changed to {0} (IsValid={1})", e, e.IsValid);

                    if (Settings.Default.TobiiEyeXProcessingLevel == DataStreamProcessingLevels.None ||
                       Settings.Default.TobiiEyeXProcessingLevel == DataStreamProcessingLevels.Low)
                    {
                        gazeDataStream = EyeXHost.CreateGazePointDataStream(
                            Settings.Default.TobiiEyeXProcessingLevel == DataStreamProcessingLevels.None
                                ? GazePointDataMode.Unfiltered //None
                                : GazePointDataMode.LightlyFiltered); //Low

                        if (!EyeXHost.IsStarted)
                        {
                            EyeXHost.Start(); // Start the EyeX host
                        }

                        gazeDataStream.Next += (s, data) =>
                        {
                            if (pointEvent != null
                                && !double.IsNaN(data.X)
                                && !double.IsNaN(data.Y))
                            {
                                pointEvent(this, new Timestamped<Point>(new Point(data.X, data.Y),
                                    new DateTimeOffset(DateTime.UtcNow).ToUniversalTime())); //EyeX does not publish a useable timestamp
                            }
                        };
                    }
                    else
                    {
                        fixationDataStream = EyeXHost.CreateFixationDataStream(
                            Settings.Default.TobiiEyeXProcessingLevel == DataStreamProcessingLevels.Medium
                                ? FixationDataMode.Sensitive //Medium
                                : FixationDataMode.Slow); //Hight

                        if(!EyeXHost.IsStarted)
                        {
                            EyeXHost.Start(); // Start the EyeX host
                        }

                        fixationDataStream.Next += (s, data) =>
                        {
                            if (pointEvent != null
                                && !double.IsNaN(data.X)
                                && !double.IsNaN(data.Y))
                            {
                                pointEvent(this, new Timestamped<Point>(new Point(data.X, data.Y),
                                    new DateTimeOffset(DateTime.UtcNow).ToUniversalTime())); //EyeX does not publish a useable timestamp
                            }
                        };
                    }
                }

                pointEvent += value;
            }
            remove
            {
                pointEvent -= value;

                if (pointEvent == null)
                {
                    Log.Info("Last listener of Point event has unsubscribed. Disposing gaze data & fixation data streams.");

                    if (gazeDataStream != null)
                    {
                        gazeDataStream.Dispose();
                        gazeDataStream = null;
                    }

                    if (fixationDataStream != null)
                    {
                        fixationDataStream.Dispose();
                        fixationDataStream = null;
                    }
                }
            }
        }

        #endregion

        #region Publish Error

        private void PublishError(object sender, Exception ex)
        {
            Log.Error("Publishing Error event (if there are any listeners)", ex);
            if (Error != null)
            {
                Error(sender, ex);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Event handler for the GazeDataReceived event. Called for every new gaze point received from the tracker.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnGazeUpdate(object sender, GazeDataEventArgs e)
        {
            CSVLogService.Instance.Log_TobiiGazeData(e);
            
            //gazeDataWriter.WriteLine(line);
        }

        /// <summary>
        /// Applies license to the EyeTracker.
        /// Will return true if application of license was sucessfull, and false if not.
        /// </summary>
        /// <param name="eyeTracker"></param>
        /// <param name="licensePath"></param>
        private bool ApplyLicense(IEyeTracker eyeTracker, string licensePath)
        {
            try
            {
                // Create a collection with the license.
                var licenseCollection = new LicenseCollection(
                    new System.Collections.Generic.List<LicenseKey>
                    {
                   new LicenseKey(System.IO.File.ReadAllBytes(licensePath))
                    });

                // See if we can apply the license. Write to console if it fails.
                FailedLicenseCollection failedLicenses;
                if (!eyeTracker.TryApplyLicenses(licenseCollection, out failedLicenses))
                {
                    string errorMessage = String.Format("Failed to apply license from {0} on eye tracker with serial number {1}.\n" +
                            "The validation result is {2}.",
                            licensePath, eyeTracker.SerialNumber, failedLicenses[0].ValidationResult);

                    Console.WriteLine(errorMessage);
                    return false;
                }
                return true;
            }catch(Exception e)
            {
                Console.WriteLine("Warning, could not apply license!");
                return false;
            }
        }
        #endregion
    }
}