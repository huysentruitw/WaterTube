/*
 * Copyright 2016 Huysentruit Wouter
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

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
