public class StoneFactory : TargetFactory
{
    public StoneFactory(TargetsProvider targetsProvider, AssetsProvider assetsProvider) : base(targetsProvider, assetsProvider)
    {
    }

    protected override string AssetKey => AssetsKeys.Stone;
    protected override TargetType TargetType => TargetType.Ore;
}
