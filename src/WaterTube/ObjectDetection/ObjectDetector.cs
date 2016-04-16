using System;
using System.Drawing;
using System.Threading;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;

namespace WaterTube.ObjectDetection
{
    public class ObjectDetector : IObjectDetector
    {
        private readonly int cameraIndex;
        private readonly int cameraFrameRate;
        private readonly Size cameraFrameSize;
        private double minimumObjectRadius;
        private object syncRoot = new object();

        private Thread detectionThread;
        private ManualResetEvent stopDetectionThread = new ManualResetEvent(false);

        public ObjectDetector(ObjectDetectorOptions options)
        {
            this.cameraIndex = options.CameraIndex;
            this.cameraFrameRate = options.CameraFrameRate;
            this.cameraFrameSize = options.CameraFrameSize;
            this.HueRange = options.HueRange;
            this.SaturationRange = options.SaturationRange;
            this.BrightnessRange = options.BrightnessRange;
            this.MinimumObjectRadius = options.MinimumObjectRadius;
        }

        ~ObjectDetector()
        {
            this.Dispose();
        }

        public Range<double> HueRange { get; private set; }
        public Range<double> SaturationRange { get; private set; }
        public Range<double> BrightnessRange { get; private set; }
        public double MinimumObjectRadius
        {
            get { lock (this.syncRoot) return this.minimumObjectRadius; }
            set { lock (this.syncRoot) this.minimumObjectRadius = value; }
        }

        public event EventHandler<ProcessedFrameEventArgs> ProcessedFrame;
        public event EventHandler<ObjectDetectedEventArgs> ObjectDetected;

        public void Start(int cameraIndex = 0)
        {
            if (this.detectionThread != null) throw new InvalidOperationException("Already started");
            this.stopDetectionThread.Reset();
            this.detectionThread = new Thread(this.DetectionLoop);
            this.detectionThread.Start();
        }

        public void Stop()
        {
            if (this.detectionThread != null)
            {
                this.stopDetectionThread.Set();
                this.detectionThread.Join();
                this.detectionThread = null;
            }
        }

        public void Dispose()
        {
            this.Stop();
        }

        private void DetectionLoop()
        {
            using (var capture = new Capture(this.cameraIndex))
            {
                capture.SetCaptureProperty(CapProp.FrameWidth, this.cameraFrameSize.Width);
                capture.SetCaptureProperty(CapProp.FrameHeight, this.cameraFrameSize.Height);

                while (!this.stopDetectionThread.WaitOne(1000 / this.cameraFrameRate, false))
                {
                    using (var frame = capture.QueryFrame())
                    {
                        if (frame == null) continue;

                        var lower = new Hsv(this.HueRange.Min, this.SaturationRange.Min, this.BrightnessRange.Min);
                        var higher = new Hsv(this.HueRange.Max, this.SaturationRange.Max, this.BrightnessRange.Max);

                        using (var originalImage = frame.ToImage<Bgr, Byte>())
                        using (var hsv = originalImage.Convert<Hsv, Byte>())
                        using (var colorFilteredImage = hsv.InRange(lower, higher))
                        using (var filteredImage = colorFilteredImage.Erode(2).Dilate(2))
                        {
                            using (var hierarchy = new Mat())
                            using (var contours = new VectorOfVectorOfPoint())
                            {
                                CvInvoke.FindContours(filteredImage, contours, hierarchy, RetrType.Tree, ChainApproxMethod.ChainApproxNone);
                                var indexOfContourWithLargestArea = contours.GetIndexOfContourWithLargestArea();

                                if (indexOfContourWithLargestArea.HasValue)
                                {
                                    var contour = contours[indexOfContourWithLargestArea.Value];
                                    var circle = CvInvoke.MinEnclosingCircle(contour);
                                    var center = circle.Center.ToPoint();
                                    var radius = (int)circle.Radius;

                                    if (radius > this.MinimumObjectRadius)
                                    {
                                        //CvInvoke.DrawContours(filteredImage, contours, largestContourIndex.Value, new MCvScalar(255, 0, 0), 3, LineType.EightConnected, hierarchy);
                                        CvInvoke.Circle(filteredImage, circle.Center.ToPoint(), radius, new MCvScalar(255, 0, 0), 3, LineType.EightConnected);
                                        CvInvoke.Circle(originalImage, circle.Center.ToPoint(), radius, new MCvScalar(255, 0, 0), 3, LineType.EightConnected);

                                        //var m = CvInvoke.Moments(contour);
                                        //var center = new Point((int)(m.M10 / m.M00), (int)(m.M01 / m.M00));
                                        this.OnObjectDetected(center, radius);
                                    }
                                }
                            }

                            this.OnProceesedFrame(originalImage.ToBitmap(), filteredImage.ToBitmap());
                        }
                    }
                }
            }
        }

        private void OnProceesedFrame(Bitmap originalFrame, Bitmap processedFrame)
        {
            var processedFrameEvent = this.ProcessedFrame;
            if (processedFrameEvent != null) processedFrameEvent(this, new ProcessedFrameEventArgs(originalFrame, processedFrame));
        }

        private void OnObjectDetected(Point objectCenter, int objectRadius)
        {
            var objectDetectedEvent = this.ObjectDetected;
            if (objectDetectedEvent != null) objectDetectedEvent(this, new ObjectDetectedEventArgs(objectCenter, objectRadius));
        }
    }
}
