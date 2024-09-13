using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerPrefsRemover : MonoBehaviour
{
    [MenuItem("PlayerPrefs/Remove all")]
    public static void RemoveAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
