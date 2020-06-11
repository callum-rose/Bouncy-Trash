using BalsamicBits.BouncyTrash.Game.Projectile;
using System;

namespace BalsamicBits.BouncyTrash
{
    internal interface IProjectileMovementHandler
    {
        event Action<ProjectileMover> ProjectileBounced, ProjectileCompleted, ProjectileCrashed, ProjectileOffScreen;

        IHasPosition BouncerPosition { get; set; }
        IAnimatable BouncerAnimator { get; set; }

        void Add(ProjectileMover projectileMover, int storey);
    }
}