using BalsamicBits.BouncyTrash.Core;
using BalsamicBits.BouncyTrash.Game.Bouncer;
using BalsamicBits.BouncyTrash.Game.Projectile;
using UnityEngine;
using UnityEngine.Serialization;

namespace BalsamicBits.BouncyTrash.Game.Core
{
    internal class GameSceneManager : BaseSceneManager
    {
#pragma warning disable 0647
        [SerializeField, FormerlySerializedAs("projectileController")] private ProjectileManager projectileManager;
        [SerializeField] private GameController gameController;
        [SerializeField] private Transform bouncerContainer;
        [SerializeField] private Transform projectileContainer;
#pragma warning restore 0647

        #region Unity

        #endregion

        #region API

        public override void Setup(IPassThroughData data)
        {
            GamePassThroughData inputData = (GamePassThroughData)data;

            IBouncer bouncer = SetupBouncer(inputData);

            SetupProjectileController(bouncer, inputData.GameStatsUpdatable);

            SetupGameController(inputData.ProjectileThemeData);
        }

        #endregion

        #region Methods

        private IBouncer SetupBouncer(GamePassThroughData inputData)
        {
            BouncerFactory factory = new BouncerFactory(bouncerContainer);
            IBouncer bouncer = factory.GetBouncer(inputData.BouncerData.Id);
            return bouncer;
        }

        private void SetupProjectileController(IBouncer bouncer, IGameStatsUpdatable gameStatsUpdatable)
        {
            projectileManager.SetStaticDependancies(
                bouncer as IHasPosition,
                bouncer.Animatable,
                gameStatsUpdatable);
        }

        private void SetupGameController(ProjectileThemeData projectileThemeData)
        {
            TrashFactory trashFactory = new TrashFactory(projectileContainer, projectileThemeData.ResourcePath);
            CoinFactory coinFactory = new CoinFactory(projectileContainer, projectileThemeData.ResourcePath);

            ProjectileKindFactory factory = new ProjectileKindFactory(trashFactory, coinFactory);

            gameController.SetStaticDependancies(projectileManager, factory);
        }

        #endregion
    }
}