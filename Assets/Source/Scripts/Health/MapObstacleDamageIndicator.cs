public class MapObstacleDamageIndicator : HitView
{
    public override void Shake()
    {
        ShakeSize();
        ShakeRotation();
        ActivateSound();
    }
}
