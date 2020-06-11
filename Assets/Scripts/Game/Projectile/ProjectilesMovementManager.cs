using BalsamicBits.BouncyTrash.Game.Core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Projectile
{
    internal class ProjectilesMovementManager : MonoBehaviour, IProjectileMovementHandler
    {
        public event Action<ProjectileMover> ProjectileBounced;
        public event Action<ProjectileMover> ProjectileCompleted;
        public event Action<ProjectileMover> ProjectileCrashed;
        public event Action<ProjectileMover> ProjectileOffScreen;

        public IHasPosition BouncerPosition { get; set; }
        public IAnimatable BouncerAnimator { get; set; }

        private readonly IList<ProjectileMover> _projectiles = new List<ProjectileMover>();
        private readonly Queue<ProjectileMover> _projectilesToRemove = new Queue<ProjectileMover>(1);

        #region Unity

        private void Update()
        {
            foreach (ProjectileMover p in _projectiles)
            {
                p.Tick(GameTimings.DeltaTime);
            }

            while (_projectilesToRemove.Count > 0)
            {
                ProjectileMover toRemove = _projectilesToRemove.Dequeue();
                Remove(toRemove);
            }
        }

        #endregion

        #region API

        public void Add(ProjectileMover projectileMover, int storey)
        {
            _projectiles.Add(projectileMover);

            projectileMover.ReachedPathEnd += OnProjectileReachedEndOfPath;
            projectileMover.SetNextPath();
        }

        #endregion

        #region Events

        private void OnProjectileReachedEndOfPath(ProjectileMover projectileMover)
        {
            int projectilePosition = (projectileMover as IHasPosition).CurrentPosition;

            if (projectilePosition == GameDimensions.PositionCountIncEnd)
            {
                // reached end
                CompleteProjectile(projectileMover);
            }
            else if (BouncerPosition.CurrentPosition == projectilePosition)
            {
                // bounced
                BounceProjectile(projectileMover);
            }
            else
            {
                // crashed
                CrashProjectile(projectileMover);
            }
        }

        private void OnReachedDeadZone(ProjectileMover projectileMover)
        {
            EnqueueRemoval(projectileMover);

            ProjectileOffScreen.Invoke(projectileMover);
        }

        #endregion

        #region Methods

        private void CompleteProjectile(ProjectileMover projectileMover)
        {
            _projectilesToRemove.Enqueue(projectileMover);

            ProjectileCompleted.Invoke(projectileMover);
        }

        private void CrashProjectile(ProjectileMover projectileMover)
        {
            ProjectileCrashed.Invoke(projectileMover);

            projectileMover.ReachedPathEnd -= OnProjectileReachedEndOfPath;

            projectileMover.ReachedDeadZone += OnReachedDeadZone;
        }

        private void BounceProjectile(ProjectileMover projectileMover)
        {
            projectileMover.SetNextPath();

            ProjectileBounced.Invoke(projectileMover);

            BouncerAnimator.Animate();
        }

        private void EnqueueRemoval(ProjectileMover projectileMover)
        {
            _projectilesToRemove.Enqueue(projectileMover);
        }

        private void Remove(ProjectileMover projectileMover)
        {
            _projectiles.Remove(projectileMover);
            projectileMover.Dispose();
        }

        #endregion
    }
}