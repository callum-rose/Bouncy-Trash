using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Projectile
{
    internal class ProjectileFactory : IProjectileFactory
    {
        private List<Projectile> _projectilePrefabs;

        private Transform _container;

        private int NextIndex
        {
            get
            {
                _currentIndex++;

                if (_currentIndex >= _projectilePrefabs.Count)
                {
                    _currentIndex = 0;
                }

                return _currentIndex;
            }
        }

        private int _currentIndex;
        
        public ProjectileFactory(Transform container)
        {
            _container = container;

            IProjectileObjectLoader loader = new ProjectileObjectLoader();
            var projectiles = loader.Load();

            _projectilePrefabs = projectiles.ToList();

            _currentIndex = NextIndex;
        }

        public Projectile CreateInstance()
        {
            _currentIndex = NextIndex;
            return Object.Instantiate(_projectilePrefabs[_currentIndex], _container);
        }
    }
}