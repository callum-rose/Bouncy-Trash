using System.Collections.Generic;
using UnityEngine;
using BalsamicBits.BouncyTrash.Game.Projectile.Path;
using BalsamicBits.BouncyTrash.Game.Core;
using BalsamicBits.BouncyTrash.Game.Projectile.Scheduler;

namespace BalsamicBits.BouncyTrash.Game.Projectile
{
    class ProjectileManager : MonoBehaviour
    {
        public IGameStatsUpdatable StatsUpdator { get; set; }

        private ProjectileKindFactory _factory;

        private IProjectileKindSelector _kindSelector;
        private IProjectileSchedule _schedule;
        private IProjectileMovementHandler _movement;

        // TODO
        private IPathFactory _currentPathFactory = new BasicPath.Factory();

        private readonly Dictionary<ProjectileMover, ProjectileAndAssociations> _moverAssociationDict = new Dictionary<ProjectileMover, ProjectileAndAssociations>();

        #region Unity

        #endregion

        #region API

        public void SetStaticDependancies(IHasPosition bouncerPosition, IAnimatable bouncerAnimator, IGameStatsUpdatable statsUpdator)
        {
            _movement.BouncerPosition = bouncerPosition;
            _movement.BouncerAnimator = bouncerAnimator;

            StatsUpdator = statsUpdator;
        }

        public void Begin(ProjectileKindFactory factory, IProjectileSchedule schedule, IProjectileMovementHandler projectileMovement, IProjectileKindSelector kindSelector)
        {
            _factory = factory;

            _schedule = schedule;
            _schedule.Spawn += OnSchedule;

            _movement = projectileMovement;
            _movement.ProjectileBounced += OnProjectileBounced;
            _movement.ProjectileCompleted += OnProjectileCompleted;
            _movement.ProjectileHitGround += OnProjectileGrounded;
            _movement.ProjectileOffScreen += OnProjectileOffScreen;

            _kindSelector = kindSelector;

            _schedule.Begin();
        }

        public void End()
        {

        }

        #endregion

        #region Events

        private void OnProjectileOffScreen(ProjectileMover mover)
        {
            RemoveAssociatedProjectile(mover);
        }

        private void OnProjectileBounced(ProjectileMover mover)
        {
            ProjectileAndAssociations p = _moverAssociationDict[mover];

            p.Animatable.Animate();
        }

        private void OnProjectileCompleted(ProjectileMover mover)
        {
            RemoveAssociatedProjectile(mover);

            StatsUpdator.UpdateScore(1);
        }

        private void OnProjectileGrounded(ProjectileMover mover)
        {
            ProjectileAndAssociations p = _moverAssociationDict[mover];

            // cancel this projectile in the scheduler
            _schedule.Cancel(p.SchedulerId);

            switch (p.Kind)
            {
                case ProjectileKind.Trash:
                    StatsUpdator.UpdateLives(-1);
                    // TODO animate crash
                    break;

                case ProjectileKind.Coin:
                    StatsUpdator.UpdateCoins(1);
                    // TODO aniamte coin
                    break;
            }
        }

        private void OnSchedule(int storey, int schedulerId)
        {
            ProjectileKind nextKind = _kindSelector.GetNext();

            Projectile newProjectile = _factory.CreateInstance(nextKind);

            ProjectileMover projectileMover = new ProjectileMover(newProjectile.transform, _currentPathFactory, storey);
            _movement.Add(projectileMover, storey);

            ProjectileAndAssociations idProj = new ProjectileAndAssociations(newProjectile, schedulerId);
            _moverAssociationDict.Add(projectileMover, idProj);
        }

        #endregion

        #region Methods

        private void RemoveAssociatedProjectile(ProjectileMover mover)
        {
            ProjectileAndAssociations projectileId = _moverAssociationDict[mover];

            _moverAssociationDict.Remove(mover);

            projectileId.Projectile.Dispose();
        }

        #endregion

        #region Classes

        private class ProjectileAndAssociations
        {
            public ProjectileAndAssociations(Projectile projectile, int schedulerId)
            {
                Projectile = projectile;
                SchedulerId = schedulerId;
            }

            public Projectile Projectile { get; }
            public int SchedulerId { get; }

            public IAnimatable Animatable => Projectile.Animatable;
            public ProjectileKind Kind => Projectile.Kind;
        }

        #endregion
    }
}