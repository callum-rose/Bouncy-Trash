using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Projectile
{
	internal class ProjectileObjectLoader : IProjectileObjectLoader
    {
		#region API

        public IEnumerable<Projectile> Load()
        {
            return Resources.LoadAll<Projectile>(ResourcePaths.Projectiles);
        }

        #endregion

        #region Methods



        #endregion
    }
}