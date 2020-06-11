using BalsamicBits.BouncyTrash.Game.Projectile;

namespace BalsamicBits.BouncyTrash
{
    internal interface IProjectileFactory
    {
        Projectile CreateInstance();
    }
}