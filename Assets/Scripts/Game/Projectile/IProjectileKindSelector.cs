namespace BalsamicBits.BouncyTrash.Game.Projectile
{
    internal interface IProjectileKindSelector
    {
        ProjectileKind GetNext();
    }
}