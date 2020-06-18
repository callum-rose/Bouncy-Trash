using System;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Debug
{
    [Serializable]
	public struct DebugSettings
	{
        [SerializeField] private bool doDisableCrashing;

        public bool DoDisableCrashing => doDisableCrashing;
    }
}