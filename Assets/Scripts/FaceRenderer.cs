using UnityEngine;
using UnityEngine.UI;

public sealed class FaceRenderer : MonoBehaviour
{
    #region Editable attributes

    [SerializeField] VideoInput _webcam = null;
    [SerializeField] RawImage _previewUI = null;
    [Space]
    [SerializeField] ONNXModel _resources = null;
    [SerializeField] Mesh _template = null;
    [SerializeField] Shader _shader = null;

    #endregion

    #region Private members

    FaceDetector _detector;
    Material _material;

    #endregion

    #region MonoBehaviour implementation

    void Start()
    {
        _detector = new FaceDetector(_resources);
        _material = new Material(_shader);
    }


    void LateUpdate()
    {
        // Face landmark detection
        _detector.ProcessImage(_webcam.Texture);

        // UI update
        _previewUI.texture = _webcam.Texture;
    }

    void OnRenderObject()
    {
        // Wireframe mesh rendering
        _material.SetBuffer("_Vertices", _detector.VertexBuffer);
        _material.SetPass(0);
        Graphics.DrawMeshNow(_template, Matrix4x4.identity);

        // Keypoint marking
        _material.SetBuffer("_Vertices", _detector.VertexBuffer);
        _material.SetPass(1);
        Graphics.DrawProceduralNow(MeshTopology.Lines, 400, 1);
    }

    void OnDestroy()
    {
        _detector.Dispose();
        Destroy(_material);
    }

    #endregion
}
