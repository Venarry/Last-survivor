using System.Collections.Generic;

public class TargetsProvider
{
    private readonly List<ITarget> _targets = new();

    public ITarget[] Targets => _targets.ToArray();

    public void Add(ITarget target)
    {
        if (_targets.Contains(target) == true)
            return;

        _targets.Add(target);
    }

    public void Remove(ITarget target)
    {
        if (_targets.Contains(target) == false)
            return;

        _targets.Remove(target);
    }
}
