using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Core
{
	internal class GameStats : IGameStatsReadable, IGameStatsUpdatable
    {
        public int Score { get; private set; }
        public int Lives { get; private set; }

		#region API

        public void UpdateScore(int scoreDelta)
        {
            Score += scoreDelta;

            UnityEngine.Debug.Log(Score);
        }

        public void UpdateLives(int livesDelta)
        {
            Lives += livesDelta;

            UnityEngine.Debug.Log(Lives);
        }

        #endregion

        #region Methods



        #endregion
    }
}