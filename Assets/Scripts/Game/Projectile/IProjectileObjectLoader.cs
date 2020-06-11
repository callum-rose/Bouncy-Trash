using System.Collections.Generic;

namespace BalsamicBits.BouncyTrash.Game.Projectile
{
    internal interface IProjectileObjectLoader
    {
        IEnumerable<Projectile> Load();
    }
}