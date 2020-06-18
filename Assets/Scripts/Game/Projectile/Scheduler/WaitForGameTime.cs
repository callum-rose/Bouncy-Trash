using BalsamicBits.BouncyTrash.Game.Core;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Projectile.Scheduler
{
    internal class WaitForGameTime : CustomYieldInstruction
    {
        public override bool keepWaiting => GameTimings.Time < _time;

        private float _time;

        public WaitForGameTime(float time)
        {
            _time = time;
        }
    }
}
