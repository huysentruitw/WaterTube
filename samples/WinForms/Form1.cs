using System;
using System.Windows.Forms;
using WaterTube.ObjectDetection;

namespace WinForms
{
    public partial class Form1 : Form
    {
        private IObjectDetector detector;

        public Form1()
        {
            InitializeComponent();

            this.detector = new ObjectDetector(new ObjectDetectorOptions
                {
                    HueRange = new Range<double>(0, 40),
                    SaturationRange = new Range<double>(80, 255),
                    BrightnessRange = new Range<double>(10, 255),
                    MinimumObjectRadius = 50
                });

            this.detector.ProcessedFrame += detector_ProcessedFrame;
            this.detector.ObjectDetected += detector_ObjectDetected;
        }

        void detector_ObjectDetected(object sender, ObjectDetectedEventArgs e)
        {
            areaToolStripStatusLabel.Text = e.ObjectRadius.ToString();
            positionToolStripStatusLabel.Text = string.Format("{0}x{1}", e.ObjectCenter.X, e.ObjectCenter.Y);
        }

        void detector_ProcessedFrame(object sender, ProcessedFrameEventArgs e)
        {
            using (var garbage = this.pictureBox1.Image)
                this.pictureBox1.Image = e.OriginalFrame;
            using (var garbage = this.pictureBox2.Image)
                this.pictureBox2.Image = e.ProcessedFrame;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.detector.Start();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.detector.Stop();
        }

        private void hueTrackBar_ValueChanged(object sender, EventArgs e)
        {
            this.detector.HueRange.Min = (sender as TrackBar).Value - 20;
            this.detector.HueRange.Max = (sender as TrackBar).Value + 20;
        }
    }
}
