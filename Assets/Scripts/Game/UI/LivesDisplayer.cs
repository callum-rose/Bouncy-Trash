using TMPro;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.UI
{
    internal class LivesDisplayer : MonoBehaviour, IIntDisplayer
	{
#pragma warning disable 0647
        [SerializeField] private TextMeshProUGUI text;
#pragma warning restore 0647

        private int _lastLives = -1;

        #region Unity

        private void Awake()
        {
            Set(0);
        }

        #endregion

        #region API

        public void Set(int lives)
        {
            if (lives == _lastLives)
            {
                return;
            }

            text.text = lives.ToString();

            _lastLives = lives;
        }

        #endregion
    }
}