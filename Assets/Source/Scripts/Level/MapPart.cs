using UnityEngine;

public class MapPart : MonoBehaviour
{
    [SerializeField] private BoxCollider _groundCollider;
    public float Length => _groundCollider.size.z;
}