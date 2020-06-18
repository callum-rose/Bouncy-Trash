using BalsamicBits.BouncyTrash.Game.Core;
using System;

namespace BalsamicBits.BouncyTrash.Game.Projectile
{
    internal interface IProjectileMovementHandler
    {
        event Action<ProjectileMover> ProjectileBounced, ProjectileCompleted, ProjectileHitGround, ProjectileOffScreen;

        IHasPosition BouncerPosition { get; set; }
        IAnimatable BouncerAnimator { get; set; }

        void Add(ProjectileMover projectileMover, int storey);
    }
}