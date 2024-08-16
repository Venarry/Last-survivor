using System;
using UnityEngine;

public class DayCycleView : MonoBehaviour
{
    private GameObject _dayBar;
    private DayCycleParameters _dayCycleParameters;
    private float _timeLeft = 0;
    private bool _isDayTimeStarted = false;

    public event Action DayTimeStarted;
    public event Action NightEnded;
    public event Action NightStarted;

    public void Init(DayCycleParameters dayCycleParameters, GameObject dayBar)
    {
        _dayCycleParameters = dayCycleParameters;
        _dayBar = dayBar;

        _dayBar.SetActive(false);
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
        NightStarted?.Invoke();
    }

    public void StartDayTimer()
    {
        _timeLeft = 0;
        _isDayTimeStarted = true;
        _dayBar.SetActive(true);

        DayTimeStarted?.Invoke();
    }

    public void EndNight()
    {
        _timeLeft = 0;
        _isDayTimeStarted = false;
        RefreshBar();
        _dayBar.SetActive(false);

        NightEnded?.Invoke();
    }

    private void RefreshBar()
    {
        _dayBar.transform.localRotation = Quaternion.Euler(0, 0, _timeLeft / _dayCycleParameters.DayDuration * 180);
    }
}