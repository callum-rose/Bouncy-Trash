using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Projectile
{
    internal class CoinFactory : ProjectileFactory
    {
        protected override Func<ProjectileData, bool> Filter { get; set; } = p => p.Kind == ProjectileKind.Coin;

        public CoinFactory(Transform container, string path) : base(container, path)
        {
        }
    }
}