using BalsamicBits.BouncyTrash.Game.Core;

namespace BalsamicBits.BouncyTrash.Game.Projectile.Scheduler
{
    internal class AscendingStoreySelector : IStoreySelector
    {
        private int _lastStorey;

        #region API

        public int GetNext()
        {
            _lastStorey = (_lastStorey + 1) % GameDimensions.StoreyCount;
            return _lastStorey;
        }

		#endregion	
	}
}