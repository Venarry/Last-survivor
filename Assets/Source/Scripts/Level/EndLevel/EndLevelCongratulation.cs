using General;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Level.EndLevel
{
    public class EndLevelCongratulation : MonoBehaviour
    {
        private const float ShowDelay = 0.7f;

        private readonly WaitForSeconds _waitForSeconds = new (ShowDelay);

        [SerializeField] private GameObject _menu;
        [SerializeField] private Button _okButton;

        private string TimeKeyName => nameof(EndLevelCongratulation);

        private void Awake()
        {
            _menu.SetActive(false);
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
            GameTimeScaler.Add(TimeKeyName, timeScale: 0);
        }

        private void ShowReward()
        {
            _menu.SetActive(false);

            GameTimeScaler.Remove(TimeKeyName);

            if (YandexGame.SDKEnabled == true)
            {
                YandexGame.FullscreenShow();
            }
        }
    }
}