using UnityEngine;

public class TutorialSaver : MonoBehaviour
{
    private ProgressHandler _progressHandler;

    public void Init(ProgressHandler progressHandler)
    {
        _progressHandler = progressHandler;
    }

    private void OnDisable()
    {
        _progressHandler.PassTutorial();
        _progressHandler.Save();
        Debug.Log("Tutorial passed");
    }
}
