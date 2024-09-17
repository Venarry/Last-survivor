using UnityEngine;

public class DayCycleParameters
{
    private readonly CharacterBuffsModel _characterBuffsModel;
    private readonly float _baseDayDuration = GameParamenters.BaseDayDuration;

    public DayCycleParameters(CharacterBuffsModel characterBuffsModel)
    {
        _characterBuffsModel = characterBuffsModel;
    }

    ~DayCycleParameters()
    {

    }

    public float DayDuration => ApplyBuffs();

    private float ApplyBuffs()
    {
        float dayDuration = _baseDayDuration;

        foreach (IDayDurationBuff buff in _characterBuffsModel.GetBuffs<IDayDurationBuff>())
        {
            dayDuration = buff.Apply(dayDuration);
        }

        Debug.Log(dayDuration);
        return dayDuration;
    }
}
