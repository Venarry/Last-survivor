using UnityEngine;

public class DayCycleParameters
{
    private readonly CharacterBuffsModel _characterBuffsModel;
    private readonly float _baseDayDuration = GameParameters.BaseDayDuration;
    private float _dayDurationWithBuffs;

    public DayCycleParameters(CharacterBuffsModel characterBuffsModel)
    {
        _characterBuffsModel = characterBuffsModel;

        _characterBuffsModel.Changed += OnBuffChange;
    }

    ~DayCycleParameters()
    {
        _characterBuffsModel.Changed -= OnBuffChange;
    }

    public float DayDuration => _dayDurationWithBuffs;

    private void OnBuffChange(IBuff buff)
    {
        if(buff is IDayDurationBuff)
        {
            ApplyBuffs();
        }
    }

    private float ApplyBuffs()
    {
        _dayDurationWithBuffs = _baseDayDuration;

        foreach (IDayDurationBuff buff in _characterBuffsModel.GetBuffs<IDayDurationBuff>())
        {
            _dayDurationWithBuffs = buff.Apply(_dayDurationWithBuffs);
        }

        return _dayDurationWithBuffs;
    }
}
