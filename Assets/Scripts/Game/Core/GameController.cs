using BalsamicBits.BouncyTrash.Game.Projectile;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Core
{
    internal class GameController : MonoBehaviour
    {
#pragma warning disable 0647
        [SerializeField] private ProjectileController projectileController;
        [SerializeField] private BouncerManager bouncerManager;
        [SerializeField, Range(0, 1)] private float testTimeScale;
#pragma warning restore 0647

        private GameStats _gameStats = new GameStats();

        #region Unity

        private void Start()
        {
            projectileController.SetDependancies(bouncerManager.Position, bouncerManager.Animator, _gameStats as IGameStatsUpdatable);
        }

        private void OnValidate()
        {
            GameTimings.TimeScale = testTimeScale;
        }

        #endregion

        #region API



        #endregion

        #region Events


        #endregion
    }
}