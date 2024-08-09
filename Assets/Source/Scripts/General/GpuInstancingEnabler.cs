using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class GpuInstancingEnabler : MonoBehaviour
{
    private void Awake()
    {
        MaterialPropertyBlock materialPropertyBlock = new();
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.SetPropertyBlock(materialPropertyBlock);
    }
}
