using BalsamicBits.BouncyTrash.Game.Bouncer;
using BalsamicBits.BouncyTrash.Game.Projectile;
using BalsamicBits.BouncyTrash.Game.Scenery;

namespace BalsamicBits.BouncyTrash.Core
{
    public class GameStartPassThroughData : IPassThroughData
    {
        public GameStartPassThroughData(BouncerData bouncerData, SceneryData sceneryData, ProjectileThemeData projectileThemeData)
        {
            BouncerData = bouncerData;
            SceneryData = sceneryData;
            ProjectileThemeData = projectileThemeData;
        }

        public BouncerData BouncerData { get; }
        public SceneryData SceneryData { get; }
        public ProjectileThemeData ProjectileThemeData { get; }
    }
}