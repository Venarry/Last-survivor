using System;
using System.Collections.Generic;

public class CharacterBuffsModel
{
    private readonly List<IBuff> _buffs = new();

    public event Action<IBuff> Changed;

    public void Add(IBuff buff)
    {
        if(buff.CanRepeat == true)
        {
            _buffs.Add(buff);
        }
        else
        {
            if(Contains(buff, out IBuff foundedBuff) == true)
            {
                _buffs.Remove(foundedBuff);
                _buffs.Add(buff);
            }
        }

        buff.ParametersChanged += OnBuffParameterChange;
        Changed?.Invoke(buff);
    }

    private void OnBuffParameterChange(IBuff buff)
    {
        Changed?.Invoke(buff);
    }

    public void Remove(IBuff buff)
    {
        if (_buffs.Contains(buff) == false)
            return;

        _buffs.Remove(buff);
    }

    public T[] GetBuffs<T>()
    {
        List<T> buffs = new();

        foreach (IBuff buff in _buffs)
        {
            if(buff is T tBuff)
            {
                buffs.Add(tBuff);
            }
        }

        return buffs.ToArray();
    }

    private bool Contains(IBuff buff, out IBuff foundedBuff)
    {
        foundedBuff = null;

        foreach (IBuff currentBuff in _buffs)
        {
            if(buff.Type == currentBuff.Type)
            {
                foundedBuff = currentBuff;
                return true;
            }
        }

        return false;
    }
}
