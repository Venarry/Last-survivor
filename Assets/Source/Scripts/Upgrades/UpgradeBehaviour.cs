public abstract class UpgradeBehaviour
{
    protected int CurrentLevel = 0;

    public void Apply()
    {
        CurrentLevel++;
        OnApply();
    }

    public abstract void Cancel();

    protected abstract void OnApply();
}
