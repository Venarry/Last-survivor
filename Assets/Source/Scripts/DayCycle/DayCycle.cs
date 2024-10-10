using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    [SerializeField] private Light _light;
    [SerializeField] private Color _targetColor;

    private GameObject _uiParent;
    private Transform _dayBar;
    private TMP_Text _timeLabel;
    private DayCycleParameters _dayCycleParameters;
    private Color _startLightColor;
    private Coroutine _activeLightTransition;
    private float _timeLeft = 0;
    private bool _isDayTimeStarted = false;

    //public event Action DayTimeStarted;
    public event Action TimeReset;
    public event Action NightCome;

    public void Init(DayCycleParameters dayCycleParameters, GameObject dayUIParent, Transform dayBar, TMP_Text timeLabel)
    {
        _dayCycleParameters = dayCycleParameters;
        _uiParent = dayUIParent;
        _dayBar = dayBar;
        _timeLabel = timeLabel;
        _startLightColor = _light.color;

        _uiParent.SetActive(false);
    }

    private void Update()
    {
        if (_isDayTimeStarted == false)
            return;

        _timeLeft += Time.deltaTime;
        RefreshBar();

        if (_timeLeft < _dayCycleParameters.DayDuration)
            return;

        _isDayTimeStarted = false;
        StartLightTransition(_targetColor);

        NightCome?.Invoke();
    }

    public void StartDayTimer()
    {
        _timeLeft = 0;
        _isDayTimeStarted = true;
        _uiParent.SetActive(true);

        //DayTimeStarted?.Invoke();
    }

    public void ResetTime()
    {
        _timeLeft = 0;
        _isDayTimeStarted = false;
        RefreshBar();
        _uiParent.SetActive(false);
        StartLightTransition(_startLightColor);

        TimeReset?.Invoke();
    }

    private void RefreshBar()
    {
        _dayBar.transform.localRotation = Quaternion.Euler(0, 0, _timeLeft / _dayCycleParameters.DayDuration * 180);

        float timerRemained = _dayCycleParameters.DayDuration - _timeLeft;

        int minutesCount = (int)Math.Floor(timerRemained / 60);
        string minutesLabel;

        int secondsCount;

        if (timerRemained % 60 <= 59)
        {
            secondsCount = (int)Math.Ceiling(timerRemained % 60);
        }
        else
        {
            secondsCount = 0;
            minutesCount++;
        }

        string secondsLabel = secondsCount >= 10 ? secondsCount.ToString() : $"0{secondsCount}";

        if (minutesCount > 0)
            minutesLabel = minutesCount >= 10 ? minutesCount.ToString() : $"0{minutesCount}";
        else
            minutesLabel = "00";

        _timeLabel.text = $"{minutesLabel}:{secondsLabel}";
    }

    private void StartLightTransition(Color targetColor)
    {
        if(_activeLightTransition != null)
        {
            StopCoroutine(_activeLightTransition);
        }

        _activeLightTransition = StartCoroutine(TransitionLightColor(targetColor));
    }

    private IEnumerator TransitionLightColor(Color targetColor)
    {
        float transitDuration = 1f;
        float timeLeft = 0;
        Color startColor = _light.color;

        while (_light.color != targetColor)
        {
            float progress = timeLeft / transitDuration;

            float r = Mathf.Lerp(startColor.r, targetColor.r, progress);
            float g = Mathf.Lerp(startColor.g, targetColor.g, progress);
            float b = Mathf.Lerp(startColor.b, targetColor.b, progress);

            _light.color = new(r, g, b);
            timeLeft += Time.deltaTime;
            

            yield return null;
        }

        _activeLightTransition = null;
    }
}