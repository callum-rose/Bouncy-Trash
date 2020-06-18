using TMPro;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.UI
{
    internal class CoinsDisplayer : MonoBehaviour, IIntDisplayer
    {
#pragma warning disable 0647
        [SerializeField] private TextMeshProUGUI text;
#pragma warning restore 0647

        private int _lastCoins = -1;

        #region Unity

        private void Awake()
        {
            Set(0);
        }

        #endregion

        #region API

        public void Set(int coins)
        {
            if (coins == _lastCoins)
            {
                return;
            }

            text.text = coins.ToString();

            _lastCoins = coins;
        }

        #endregion
    }
}