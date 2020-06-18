using BalsamicBits.BouncyTrash.Game.Core;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Projectile.Scheduler
{
    internal class RandomStoreySelector : IStoreySelector
    {
		#region API

        public int GetNext()
        {
            return Random.Range(1, GameDimensions.StoreyCount + 1);
        }

		#endregion	
	}
}