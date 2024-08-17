using UnityEngine;

public class MapPart : MonoBehaviour
{
    [SerializeField] private Transform _lengthTarget;
    public float Length => _lengthTarget.localScale.z;
}