using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Projectile
{
    internal abstract class ProjectileFactory : IProjectileFactory
    {
        protected abstract Func<ProjectileData, bool> Filter { get; set; }

        private List<ProjectileData> _projectileDatas;

        private Transform _container;

        private int NextIndex
        {
            get
            {
                _currentIndex++;

                if (_currentIndex >= _projectileDatas.Count)
                {
                    _currentIndex = 0;
                }

                return _currentIndex;
            }
        }

        private int _currentIndex;
        
        public ProjectileFactory(Transform container, string dataPath)
        {
            _container = container;

            ProjectileDataLoader loader = new ProjectileDataLoader(dataPath);
            var projectiles = loader.Load();

            if (Filter != null)
            {
                projectiles = projectiles.Where(Filter);
            }

            _projectileDatas = projectiles.ToList();

            _currentIndex = NextIndex;
        }

        public Projectile CreateInstance()
        {
            _currentIndex = NextIndex;

            ProjectileData data = _projectileDatas[_currentIndex];

            Projectile projectile = ProjectileBuilder.Build(data);
            projectile.transform.SetParent(_container);

            return projectile;
        }
    }
}