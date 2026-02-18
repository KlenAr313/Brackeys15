using UnityEngine;

[ExecuteAlways]
[RequireComponent(typeof(Renderer))]
public class TextureAutoTiling : MonoBehaviour
{
    public float tilesPerUnit = 1f;

    Renderer _r;
    MaterialPropertyBlock _mpb;

    static readonly int MainTexST = Shader.PropertyToID("_MainTex_ST");
    static readonly int BaseMapST = Shader.PropertyToID("_BaseMap_ST");

    void OnEnable()
    {
        _r = GetComponent<Renderer>();
        _mpb = new MaterialPropertyBlock();
        Apply();
    }

    void OnValidate() => Apply();
    void Update() => Apply();

    void Apply()
    {
        if (_r == null) _r = GetComponent<Renderer>();
        if (_mpb == null) _mpb = new MaterialPropertyBlock();

        Vector3 s = transform.lossyScale;
        float tx = Mathf.Max(0.0001f, s.x * tilesPerUnit);
        float ty = Mathf.Max(0.0001f, s.y * tilesPerUnit);

        _r.GetPropertyBlock(_mpb);

        Vector4 st = new Vector4(tx, ty, 0f, 0f);

        _mpb.SetVector(MainTexST, st);
        _mpb.SetVector(BaseMapST, st);

        _r.SetPropertyBlock(_mpb);
    }
}
