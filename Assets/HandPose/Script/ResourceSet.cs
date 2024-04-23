using UnityEngine;
using Unity.Sentis;

namespace MediaPipe.HandPose {

//
// ScriptableObject class used to hold references to internal assets
//
[CreateAssetMenu(fileName = "ResourceSet",
                 menuName = "ScriptableObjects/MediaPipe/HandPose Resource Set")]
public sealed class ResourceSet : ScriptableObject
{
    public MediaPipe.BlazePalm.ResourceSet blazePalm;
    public MediaPipe.BlazePalm.ResourceSet blazePalm2;
    public MediaPipe.HandLandmark.ResourceSet handLandmark;
    public MediaPipe.HandLandmark.ResourceSet handLandmark2;
    public ComputeShader compute;
}

} // namespace MediaPipe.HandPose
