using TMPro;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.UI
{
    internal class ScoreDisplayer : MonoBehaviour, IIntDisplayer
	{
#pragma warning disable 0647
        [SerializeField] private TextMeshProUGUI text;
#pragma warning restore 0647

        private int _lastScore = -1;

        #region Unity

        private void Awake()
        {
            Set(0);
        }

        #endregion

        #region API

        public void Set(int score)
        {
            if (score == _lastScore)
            {
                return;
            }

            text.text = score.ToString();

            _lastScore = score;
        }

        #endregion
    }
}