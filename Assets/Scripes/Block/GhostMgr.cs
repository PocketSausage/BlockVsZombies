using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways] // 让它在编辑模式也能运行
public class GhostTransparencyManager : MonoBehaviour
{
    [Header("虚影透明度控制")]
    [Range(0f, 1f)]
    public float ghostAlpha = 0.3f;

    [Tooltip("拖入所有虚影预制体或实例")]
    public List<GameObject> ghostObjects = new List<GameObject>();

    private float lastAlpha = -1f;

    void Update()
    {
        // 避免每帧都更新，只有透明度改变时才更新
        if (Mathf.Abs(ghostAlpha - lastAlpha) > 0.001f)
        {
            UpdateGhostTransparency();
            lastAlpha = ghostAlpha;
        }
    }

    void UpdateGhostTransparency()
    {
        foreach (GameObject obj in ghostObjects)
        {
            if (obj == null) continue;

            Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();

            foreach (Renderer r in renderers)
            {
                if (r.sharedMaterial.HasProperty("_Color"))
                {
                    Color color = r.sharedMaterial.color;
                    color.a = ghostAlpha;
                    r.sharedMaterial.color = color;

                    // 设置材质为透明模式（避免不透明材质不显示透明度）
                    var mat = r.sharedMaterial;
                    mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    mat.SetInt("_ZWrite", 0);
                    mat.DisableKeyword("_ALPHATEST_ON");
                    mat.EnableKeyword("_ALPHABLEND_ON");
                    mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    mat.renderQueue = 3000;
                }
            }
        }
    }
}