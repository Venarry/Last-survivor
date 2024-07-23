using UnityEngine;

public class AngleFixator : MonoBehaviour
{
    [SerializeField] private Vector3 _angle;

    private void Update()
    {
        transform.eulerAngles = _angle;
    }
}
