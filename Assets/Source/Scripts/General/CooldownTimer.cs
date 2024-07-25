using UnityEngine;

public class CooldownTimer
{
    private float _cooldown;
    private float _timeLeft;

    public bool IsReady => _timeLeft >= _cooldown;

    public CooldownTimer(float cooldown, bool isReadyOnAwake = true)
    {
        _cooldown = cooldown;

        if(isReadyOnAwake == true)
        {
            _timeLeft = _cooldown;
        }
    }

    public void Tick()
    {
        _timeLeft += Time.deltaTime;
    }

    public void Reset()
    {
        _timeLeft = 0;
    }
}