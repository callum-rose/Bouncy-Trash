using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Projectile
{
    internal class TrashFactory : ProjectileFactory
    {
        protected override Func<ProjectileData, bool> Filter { get; set; } = p => p.Kind == ProjectileKind.Trash;

        public TrashFactory(Transform container, string path) : base(container, path)
        {
        }
    }
}