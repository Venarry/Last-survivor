using Assets;
using Configs;
using UnityEngine;

namespace Targets
{
    public class StoneFactory : TargetFactory
    {
        public StoneFactory(
            TargetsProvider<Target> targetsProvider,
            AssetsProvider assetsProvider,
            AudioSource audioSource)
            : base(targetsProvider, assetsProvider, audioSource)
        {
        }

        protected override string AssetKey => AssetsKeys.Stone;
        protected override TargetType TargetType => TargetType.Ore;
    }
}