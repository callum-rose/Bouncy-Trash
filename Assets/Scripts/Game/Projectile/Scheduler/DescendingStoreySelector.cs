using BalsamicBits.BouncyTrash.Game.Core;

namespace BalsamicBits.BouncyTrash.Game.Projectile.Scheduler
{
    internal class DescendingStoreySelector : IStoreySelector
    {
        private int _lastStorey;

        #region API

        public int GetNext()
        {
            _lastStorey--;

            if (_lastStorey < 1)
            {
                _lastStorey = GameDimensions.StoreyCount;
            }

            return _lastStorey;
        }

		#endregion	
	}
}