using UnityEngine;

[ExecuteInEditMode]

// Creates a post processing stack
public class PostProcess : MonoBehaviour
{
    [SerializeField] private Material material;

    private void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (material != null) Graphics.Blit(src, dest, material);
    }
}
