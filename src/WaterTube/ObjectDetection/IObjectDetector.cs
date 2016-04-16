using System;
using System.Drawing;

namespace WaterTube.ObjectDetection
{
    public interface IObjectDetector : IDisposable
    {
        Range<double> HueRange { get; }
        Range<double> SaturationRange { get; }
        Range<double> BrightnessRange { get; }

        /// <summary>
        /// Invoked on each processed video frame.
        /// </summary>
        event EventHandler<ProcessedFrameEventArgs> ProcessedFrame;

        /// <summary>
        /// Invoked when an object has been detected in the processed video frame.
        /// </summary>
        event EventHandler<ObjectDetectedEventArgs> ObjectDetected;

        /// <summary>
        /// Start the detector.
        /// </summary>
        /// <param name="cameraIndex">The camera index to pass to OpenCV.</param>
        void Start(int cameraIndex = 0);

        /// <summary>
        /// Stop the detector.
        /// </summary>
        void Stop();
    }
}
