using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace AudioMixerHomework
{
    public class VolumeHandler : MonoBehaviour
    {
        private const string VolumeMaster = "Master";

        [SerializeField] private Slider _generalVolume;
        [SerializeField] private AudioMixer _audioMixer;

        private void Awake()
        {
            if(_audioMixer.GetFloat(VolumeMaster, out float value))
            {
                //Debug.Log(value);
                //value = Mathf.Log10(value) * 20;
                //Debug.Log(value);
                _generalVolume.value = 1;
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
            float minValue = 0.0001f;
            float maxValue = 1f;
            value = Mathf.Clamp(value, minValue, maxValue);

            _audioMixer.SetFloat(volumeName, Mathf.Log10(value) * 20);
        }
    }
}
