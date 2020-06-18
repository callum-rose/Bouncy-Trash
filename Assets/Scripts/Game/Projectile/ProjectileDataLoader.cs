using BalsamicBits.BouncyTrash.Core;
using System.Collections.Generic;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Projectile
{
    internal class ProjectileDataLoader : AssetLoader<ProjectileData>
    {
        public ProjectileDataLoader(string path) : base(path)
        {
        }
    }
}