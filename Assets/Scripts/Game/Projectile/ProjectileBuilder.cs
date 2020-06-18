using BalsamicBits.BouncyTrash.Game.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Projectile
{
	internal static class ProjectileBuilder
	{
		#region API

        public static Projectile Build(ProjectileData data)
        {
            GameObject container = new GameObject(data.Name);

            GameObject modelObj = Object.Instantiate(data.ModelPrefab, container.transform);
            modelObj.transform.localPosition = Vector3.zero;

            modelObj.GetComponentInChildren<Offsetor>().Set(data.Offset);

            var projectile = container.AddComponent<Projectile>();
            projectile.SetType(data.Kind);

            return projectile;
        }

		#endregion	

		#region Methods



		#endregion	
	}
}