using System.Drawing;

namespace WaterTube.ObjectDetection
{
    public class ObjectDetectorOptions
    {
        public ObjectDetectorOptions()
        {
            this.CameraIndex = 0;
            this.CameraFrameRate = 25;
            this.CameraFrameSize = new Size(640, 480);

            this.HueRange = new Range<double>(0, 180);
            this.SaturationRange = new Range<double>(0, 255);
            this.BrightnessRange = new Range<double>(0, 255);

            this.MinimumObjectRadius = 0;
        }

        public int CameraIndex { get; set; }
        public int CameraFrameRate { get; set; }
        public Size CameraFrameSize { get; set; }

        public Range<double> HueRange { get; set; }
        public Range<double> SaturationRange { get; set; }
        public Range<double> BrightnessRange { get; set; }

        public double MinimumObjectRadius { get; set; }
    }
}
