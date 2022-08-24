using UnityEngine;
using Unity.Barracuda;

//
// ScriptableObject class used to hold references to internal assets
//
[CreateAssetMenu(fileName = "ONNX Model",
                 menuName = "ScriptableObjects/ONNX Model")]
public sealed class ONNXModel : ScriptableObject
{
    public NNModel model;
    public ComputeShader preprocess;
    public ComputeShader postprocess;
}

