using System;

namespace GameTutorial
{
    public interface ITutorialAction
    {
        public event Action<ITutorialAction> Happened;
    }
}