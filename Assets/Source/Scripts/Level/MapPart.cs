using UnityEngine;

public class MapPart : MonoBehaviour, IMapPart
{
    [SerializeField] private Transform _lengthTarget;
    public float Length => _lengthTarget.localScale.z;
}
