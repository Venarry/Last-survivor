public class PlayerHealthOverReaction : HealthOverReaction
{
    protected override void OnHealthOver()
    {
        gameObject.SetActive(false);
    }
}
