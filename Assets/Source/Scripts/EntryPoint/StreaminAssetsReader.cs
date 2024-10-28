using System.IO;
using UnityEngine;

public class StreaminAssetsReader
{
    private static string BasePath => Application.streamingAssetsPath + "/";

    public static T Read<T>(string path)
    {
        string endPath = BasePath + path;

        string file = File.ReadAllText(endPath);
        T fromJson = JsonUtility.FromJson<T>(file);

        return fromJson;
    }
}
