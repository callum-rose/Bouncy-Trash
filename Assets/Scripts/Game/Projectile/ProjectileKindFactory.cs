using BalsamicBits.BouncyTrash.Extensions;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Projectile
{
    internal class ProjectileKindFactory
    {
        private Dictionary<ProjectileKind, IProjectileFactory> _kindFactories;

        public ProjectileKindFactory(params IProjectileFactory[] factories)
        {
            _kindFactories = new Dictionary<ProjectileKind, IProjectileFactory>(factories.Length);

            List<ProjectileKind> kinds = EnumExtensions.GetValues<ProjectileKind>().ToList();
            foreach (IProjectileFactory factory in factories)
            {
                ProjectileKind kind;
                using (Projectile projectile = factory.CreateInstance())
                {
                    kind = projectile.Kind;
                }

                if (_kindFactories.ContainsKey(kind))
                {
                    throw new ArgumentException($"Input factories has more than 1 definition of a {kind} factory");
                }

                _kindFactories.Add(kind, factory);

                kinds.Remove(kind);
            }

            if (kinds.Count > 0)
            {
                throw new ArgumentException($"Input factories does not container a definition for these types: {kinds.Stringify()}");
            }
        }

        public Projectile CreateInstance(ProjectileKind kind)
        {
            return _kindFactories[kind].CreateInstance();
        }
    }
}