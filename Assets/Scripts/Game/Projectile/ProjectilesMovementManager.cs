using BalsamicBits.BouncyTrash.Game.Core;
using BalsamicBits.BouncyTrash.Game.Debug;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace BalsamicBits.BouncyTrash.Game.Projectile
{
    internal class ProjectilesMovementManager : MonoBehaviour, IProjectileMovementHandler
    {
        public event Action<ProjectileMover> ProjectileBounced;
        public event Action<ProjectileMover> ProjectileCompleted;
        public event Action<ProjectileMover> ProjectileHitGround;
        public event Action<ProjectileMover> ProjectileOffScreen;

        public IHasPosition BouncerPosition { get; set; }
        public IAnimatable BouncerAnimator { get; set; }

        private readonly IList<ProjectileMover> _projectiles = new List<ProjectileMover>();
        private readonly Queue<ProjectileMover> _projectilesToRemove = new Queue<ProjectileMover>(1);

        private float _debugLastReachPathEndTime = float.MinValue;
        private ProjectileMover _debugLastProjectileMoverToReachPathEnd;

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
                return;
            }
            else if (BouncerPosition.CurrentPosition == projectilePosition || DebugSettingsComponent.Instance.Settings.DoDisableCrashing)
            {
                // bounced
                BounceProjectile(projectileMover);
            }
            else
            {
                // crashed
                GroundProjectile(projectileMover);
            }


            if (GameTimings.Time - _debugLastReachPathEndTime < GameTimings.FrameDuration * 0.9f)
            {
                if (projectileMover.CurrentPosition != _debugLastProjectileMoverToReachPathEnd.CurrentPosition)
                {
                    LogUtil.WriteError($"2 projectiles bounced / crashed at position {projectileMover.CurrentPosition}" +
                        $" & {_debugLastProjectileMoverToReachPathEnd.CurrentPosition} within a period of " +
                        $"{GameTimings.Time - _debugLastReachPathEndTime}s. Period should be {GameTimings.FrameDuration}s");
                    UnityEngine.Debug.Break();
                }
            }

            _debugLastReachPathEndTime = GameTimings.Time;
            _debugLastProjectileMoverToReachPathEnd = projectileMover;
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

        private void GroundProjectile(ProjectileMover projectileMover)
        {
            ProjectileHitGround.Invoke(projectileMover);

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