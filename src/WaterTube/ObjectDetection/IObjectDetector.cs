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
