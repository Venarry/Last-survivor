using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class EndLevelCongratulation : MonoBehaviour
{
    private const float ShowDelay = 0.7f;

    [SerializeField] private GameObject _menu;
    [SerializeField] private Button _okButton;

    private readonly WaitForSeconds _waitForSeconds = new(ShowDelay);
    private GameTimeScaler _timeScaler;

    private string TimeKeyName => nameof(EndLevelCongratulation);

    private void Awake()
    {
        _menu.SetActive(false);
    }

    public void Init(GameTimeScaler gameTimeScaler)
    {
        _timeScaler = gameTimeScaler;
    }

    private void OnEnable()
    {
        _okButton.onClick.AddListener(ShowReward);
    }

    private void OnDisable()
    {
        _okButton.onClick.RemoveListener(ShowReward);
    }

    public void ShowMenu()
    {
        StartCoroutine(ShowDelayedMenu());
    }

    private IEnumerator ShowDelayedMenu()
    {
        yield return _waitForSeconds;

        _menu.SetActive(true);
        _timeScaler.Add(TimeKeyName, timeScale: 0);
    }

    private void ShowReward()
    {
        YandexGame.FullscreenShow();
        _menu.SetActive(false);

        _timeScaler.Remove(TimeKeyName);
    }
}
