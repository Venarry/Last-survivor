using System;

public interface ITutorialAction
{
    public event Action<ITutorialAction> Happened;
}