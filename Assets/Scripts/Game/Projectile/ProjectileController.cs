using System.Collections.Generic;
using UnityEngine;
using BalsamicBits.BouncyTrash.Game.Projectile.Path;
using System;
using BalsamicBits.BouncyTrash.Game.Core;

namespace BalsamicBits.BouncyTrash.Game.Projectile
{
    class ProjectileController : MonoBehaviour
    {
#pragma warning disable 0647
        [SerializeField] private Transform projectileContainer;
#pragma warning restore 0647

        public IGameStatsUpdatable StatsUpdator { get; set; }

        private IProjectileSchedule _schedule;
        private IProjectileFactory _factory;
        private IProjectileMovementHandler _movement;

        // TODO
        private IPathFactory _currentPathFactory = new BasicPath.Factory();

        private readonly Dictionary<ProjectileMover, ProjectileWithId> _moverAssociationDict = new Dictionary<ProjectileMover, ProjectileWithId>();

        private void Awake()
        {
            _schedule = GetComponent<IProjectileSchedule>();
            _schedule.Spawn += OnSchedule;

            // TODO
            _factory = GetProjectileFactory();

            _movement = GetComponent<IProjectileMovementHandler>();
            _movement.ProjectileBounced += OnProjectileBounced;
            _movement.ProjectileCompleted += OnProjectileCompleted;
            _movement.ProjectileCrashed += OnProjectileCrashed;
            _movement.ProjectileOffScreen += OnProjectileOffScreen;
        }

        private void Start()
        {
            _schedule.Begin();
        }

        public void SetDependancies(IHasPosition bouncerPosition, IAnimatable bouncerAnimator, IGameStatsUpdatable statsUpdator)
        {
            _movement.BouncerPosition = bouncerPosition;
            _movement.BouncerAnimator = bouncerAnimator;

            StatsUpdator = statsUpdator;
        }

        private void OnProjectileOffScreen(ProjectileMover mover)
        {
            RemoveAssociatedProjectile(mover);
        }

        private void OnProjectileBounced(ProjectileMover mover)
        {

        }

        private void OnProjectileCompleted(ProjectileMover mover)
        {
            RemoveAssociatedProjectile(mover);

            StatsUpdator.UpdateScore(1);  
        }

        private void OnProjectileCrashed(ProjectileMover mover)
        {
            ProjectileWithId pId = _moverAssociationDict[mover];

            // cancel this projectile in the scheduler
            _schedule.Cancel(pId.SchedulerId);

            StatsUpdator.UpdateLives(-1);
            // TODO animate crash
        }

        private void OnSchedule(int storey, int schedulerId)
        {
            Projectile newProjectile = _factory.CreateInstance();

            ProjectileMover projectileMover = new ProjectileMover(newProjectile.transform, _currentPathFactory, storey);
            _movement.Add(projectileMover, storey);

            ProjectileWithId idProj = new ProjectileWithId(newProjectile, schedulerId);
            _moverAssociationDict.Add(projectileMover, idProj);
        }

        private IProjectileFactory GetProjectileFactory()
        {
            return new ProjectileFactory(projectileContainer);
        }

        private void RemoveAssociatedProjectile(ProjectileMover mover)
        {
            ProjectileWithId projectileId = _moverAssociationDict[mover];

            _moverAssociationDict.Remove(mover);

            projectileId.Projectile.Dispose();
        }

        private class ProjectileWithId
        {
            public ProjectileWithId(Projectile projectile, int id)
            {
                Projectile = projectile;
                SchedulerId = id;
            }

            public Projectile Projectile { get; }
            public int SchedulerId { get; }
        }
    }
}