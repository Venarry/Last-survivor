using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private LevelSpawner _levelSpawner;
    [SerializeField] private GameObject _safeZonePrefab;
    [SerializeField] private GameObject _checkpointPrefab;
}
