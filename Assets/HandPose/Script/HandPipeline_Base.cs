using MediaPipe.BlazePalm;
using MediaPipe.HandLandmark;
using UnityEngine;
using UnityEngine.Rendering;

namespace MediaPipe.HandPose {

//
// Basic implementation of the hand pipeline class
//

sealed partial class HandPipeline : System.IDisposable
{
    #region Private objects

    const int CropSize = HandLandmarkDetector.ImageSize;
    int InputWidth => _detector.palm.ImageSize;

    ResourceSet _resources;
    (PalmDetector palm, HandLandmarkDetector landmark) _detector;
    (PalmDetector palm, HandLandmarkDetector landmark) _detector2;
    (ComputeBuffer region, ComputeBuffer filter) _buffer;
    (ComputeBuffer region, ComputeBuffer filter) _buffer2;
    GlobalKeyword _keywordNchw;

    #endregion

    #region Object allocation/deallocation

    void AllocateObjects(ResourceSet resources)
    {
        _resources = resources;

        _detector = (new PalmDetector(_resources.blazePalm), new HandLandmarkDetector(_resources.handLandmark));
        _detector2 = (new PalmDetector(_resources.blazePalm), new HandLandmarkDetector(_resources.handLandmark));

        var regionStructSize = sizeof(float) * 24;
        var filterBufferLength = HandLandmarkDetector.VertexCount * 2;

        _buffer = (new ComputeBuffer(1, regionStructSize),
            new ComputeBuffer(filterBufferLength, sizeof(float) * 4));
                   
        _buffer2 = (new ComputeBuffer(1, regionStructSize),
            new ComputeBuffer(filterBufferLength, sizeof(float) * 4));
        _keywordNchw = GlobalKeyword.Create("NCHW_INPUT");
        Shader.SetKeyword(_keywordNchw, _detector.palm.InputIsNCHW);
        Shader.SetKeyword(_keywordNchw, _detector2.palm.InputIsNCHW);
    }

    void DeallocateObjects()
    {
        _detector.palm.Dispose();
        _detector2.palm.Dispose();
        _detector.landmark.Dispose();
        _detector2.landmark.Dispose();
        _buffer.region.Dispose();
        _buffer2.region.Dispose();
        _buffer.filter.Dispose();
        _buffer2.filter.Dispose();
    }

    #endregion
}

} // namespace MediaPipe.HandPose
