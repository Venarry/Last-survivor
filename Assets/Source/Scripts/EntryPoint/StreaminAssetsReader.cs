using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class StreaminAssetsReader
{
    private CoroutineProvider _coroutineProvider;

    public StreaminAssetsReader(CoroutineProvider coroutineProvider)
    {
        _coroutineProvider = coroutineProvider;
    }

    private string BasePath => Application.streamingAssetsPath + "/";

    public async Task<T> ReadAsync<T>(string path)
    {
        Debug.Log($"start to get Language '{path}' 1.0.2");
        string endPath = BasePath + path;
        string file;

#if UNITY_WEBGL
        file = await CreateWebRequestAwait(endPath);
#else
        file = File.ReadAllText(endPath);
#endif

        T fromJson = JsonUtility.FromJson<T>(file);
        Debug.Log($"return {file}");
        return fromJson;
    }

    private IEnumerator CreateWebRequest(string path)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(path);

        yield return webRequest.SendWebRequest();

        if(webRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log($"Language get {webRequest.downloadHandler.text}");
        }
        else
        {
            Debug.Log("Language error");
        }
    }

    private async Task<string> CreateWebRequestAwait(string path)
    {
        Debug.Log($"Try get web Language");
        UnityWebRequest webRequest = UnityWebRequest.Get(path);
        webRequest.SendWebRequest();

        while (webRequest.isDone == false)
        {
            Debug.Log($"await");
            await Task.Yield();
        }

        if (webRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log($"Language get {webRequest.downloadHandler.text}");

            return webRequest.downloadHandler.text;
        }
        else
        {
            Debug.Log("Language error");
            throw new ArgumentNullException();
        }
    }
}
