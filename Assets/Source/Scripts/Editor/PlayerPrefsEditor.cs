using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerPrefsEditor : MonoBehaviour
{
    [MenuItem("PlayerPrefs/Remove all")]
    public static void RemoveAll()
    {
        PlayerPrefs.DeleteAll();
    }

    [MenuItem("PlayerPrefs/Show save")]
    public static void ShowSave()
    {
        Debug.Log(PlayerPrefs.GetString("Save"));
    }
}
