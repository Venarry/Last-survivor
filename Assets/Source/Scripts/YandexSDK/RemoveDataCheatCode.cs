using System;
using UnityEngine;
using YG;

public class RemoveDataCheatCode : MonoBehaviour
{
    private const string CheatCode = "deleteall";

    private readonly Array _keyCodes = Enum.GetValues(typeof(KeyCode));
    private string _currentCode = string.Empty;

    private void Update()
    {
        if (Input.anyKeyDown == true)
        {
            foreach (KeyCode keyCode in _keyCodes)
            {
                if(Input.GetKeyDown(keyCode) == true)
                {
                    _currentCode += keyCode.ToString().ToLower();

                    if(_currentCode.Length > CheatCode.Length)
                    {
                        _currentCode = string.Empty;
                        return;
                    }

                    if (_currentCode[_currentCode.Length - 1] != CheatCode[_currentCode.Length - 1])
                    {
                        _currentCode = string.Empty;
                    }

                    if(_currentCode.Length == CheatCode.Length)
                    {
                        PlayerPrefs.DeleteKey(ProgressHandler.SaveName);
                        _currentCode = string.Empty;

                        if(YandexGame.SDKEnabled == true)
                        {
                            YandexGame.ResetSaveProgress();
                            YandexGame.SaveProgress();
                        }
                    }
                }
            }
        }
    }
}
