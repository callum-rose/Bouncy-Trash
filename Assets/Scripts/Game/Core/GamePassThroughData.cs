using BalsamicBits.BouncyTrash.Core;
using BalsamicBits.BouncyTrash.Game.Bouncer;
using BalsamicBits.BouncyTrash.Game.Core;
using BalsamicBits.BouncyTrash.Game.Projectile;

namespace BalsamicBits.BouncyTrash.Game.Core
{
    public class GamePassThroughData : IPassThroughData
    {
        public GamePassThroughData(BouncerData bouncerData, ProjectileThemeData projectileThemeData, IGameStatsUpdatable gameStatsUpdatable)
        {
            BouncerData = bouncerData;
            ProjectileThemeData = projectileThemeData;
            GameStatsUpdatable = gameStatsUpdatable;
        }

        public BouncerData BouncerData { get; }
        public ProjectileThemeData ProjectileThemeData { get; }
        public IGameStatsUpdatable GameStatsUpdatable { get; }
    }
}