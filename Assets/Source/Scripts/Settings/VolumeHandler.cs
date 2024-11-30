using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Settings
{
    public class VolumeHandler : MonoBehaviour
    {
        private const string VolumeMaster = "Master";

        private readonly float _minValue = 0.0001f;
        private readonly float _maxValue = 1f;

        [SerializeField] private Slider _generalVolume;
        [SerializeField] private AudioMixer _audioMixer;

        private void Awake()
        {
            if (_audioMixer.GetFloat(VolumeMaster, out float value))
            {
                value = Mathf.Pow(10, value / 20);
                _generalVolume.value = value;
            }
        }

        private void OnEnable()
        {
            _generalVolume.onValueChanged.AddListener(OnGeneralVolumeSliderValueChange);
        }

        private void OnDisable()
        {
            _generalVolume.onValueChanged.RemoveListener(OnGeneralVolumeSliderValueChange);
        }

        private void OnGeneralVolumeSliderValueChange(float value)
        {
            SetVolume(VolumeMaster, value);
        }

        private void SetVolume(string volumeName, float value)
        {
            value = Mathf.Clamp(value, _minValue, _maxValue);

            AudioListener.volume = value;
        }
    }
}
