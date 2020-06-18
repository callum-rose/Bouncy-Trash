using UnityEngine;
using BalsamicBits.BouncyTrash.Core;
using BalsamicBits.BouncyTrash.Game.Core;
using UnityEngine.Assertions;

namespace BalsamicBits.BouncyTrash.Game.UI
{
    internal class GameUiSceneManager : BaseSceneManager
	{
#pragma warning disable 0647
        [SerializeField] private IIntDisplayerContainer scoreDisplayer;
        [SerializeField] private IIntDisplayerContainer livesDisplayer;
        [SerializeField] private IIntDisplayerContainer coinsDisplayer;
#pragma warning restore 0647

        private IIntDisplayer ScoreDisplayer => scoreDisplayer.Result;
        private IIntDisplayer LivesDisplayer => livesDisplayer.Result;
        private IIntDisplayer CoinsDisplayer => coinsDisplayer.Result;

        private IGameStatsNotifier _gameStatsReader;

        #region Unity

        private void OnDestroy()
        {
            if (_gameStatsReader != null)
            {
                _gameStatsReader.ScoreChanged -= OnScoreChanged;
                _gameStatsReader.LivesChanged -= OnLivesChanged;
                _gameStatsReader.CoinsChanged -= OnCoinsChanged;
            }
        }

        #endregion

        #region API

        public override void Setup(IPassThroughData data)
        {
            GameUiPassThroughData inputData = (GameUiPassThroughData)data;

            _gameStatsReader = inputData.GameStatsReader;

            Assert.IsNotNull(_gameStatsReader);

            _gameStatsReader.ScoreChanged += OnScoreChanged;
            _gameStatsReader.LivesChanged += OnLivesChanged;
            _gameStatsReader.CoinsChanged += OnCoinsChanged;
        }

        #endregion

        #region Events

        private void OnScoreChanged(int score)
        {
            ScoreDisplayer.Set(score);
        }

        private void OnLivesChanged(int lives)
        {
            LivesDisplayer.Set(lives);
        }

        private void OnCoinsChanged(int coins)
        {
            CoinsDisplayer.Set(coins);
        }

        #endregion
    }
}