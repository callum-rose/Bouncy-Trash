using BalsamicBits.BouncyTrash.Game.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Projectile
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
