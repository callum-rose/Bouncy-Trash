using System;

namespace BalsamicBits.BouncyTrash.Game.Core
{
    internal class GameStats : IGameStatsNotifier, IGameStatsUpdatable
    {
        public event Action<int> LivesChanged;
        public event Action<int> ScoreChanged;
        public event Action<int> CoinsChanged;

        private int _score;
        private int _lives;
        private int _coins;

        #region API

        public void UpdateScore(int scoreDelta)
        {
            _score += scoreDelta;

            ScoreChanged?.Invoke(_score);
        }

        public void UpdateLives(int livesDelta)
        {
            _lives += livesDelta;

            LivesChanged?.Invoke(_lives);
        }
        
        public void UpdateCoins(int coinsDelta)
        {
            _coins += coinsDelta;

            CoinsChanged?.Invoke(_coins);
        }

        #endregion

        #region Methods



        #endregion
    }
}