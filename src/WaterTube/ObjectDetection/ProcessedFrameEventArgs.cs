using System;
using System.Drawing;

namespace WaterTube.ObjectDetection
{
    public class ProcessedFrameEventArgs : EventArgs
    {
        internal ProcessedFrameEventArgs(Bitmap originalFrame, Bitmap processedFrame)
        {
            this.OriginalFrame = originalFrame;
            this.ProcessedFrame = processedFrame;
        }

        public Bitmap OriginalFrame { get; private set; }
        public Bitmap ProcessedFrame { get; private set; }
    }
}
