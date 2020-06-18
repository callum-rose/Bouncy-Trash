using BalsamicBits.BouncyTrash.Core;
using BalsamicBits.BouncyTrash.Game.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BalsamicBits.BouncyTrash.Game.Core
{
    public class GameStarter : MonoBehaviour
    {
        private GameStartData _startData;

        private GameStats _gameStats = new GameStats();

        #region Unity

        public void Awake()
        {
            AssetLoader<GameStartData> loader = new AssetLoader<GameStartData>(ResourcePaths.Game);
            _startData = loader.Load().First();
        }

        #endregion

        #region API

        public void StartGame(GameStartPassThroughData data)
        {
            // load game scene
            StartCoroutine(
                LoadGameSceneRoutine(
                    _startData.GameSceneBuildIndex, 
                    new GamePassThroughData(data.BouncerData, data.ProjectileThemeData, _gameStats)));

            // load scenery scene
            StartCoroutine(LoadGameSceneRoutine(_startData.ScenerySceneBuildIndex, null));

            // load ui scene
            StartCoroutine(
                LoadGameSceneRoutine(
                    _startData.UiSceneBuildIndex, 
                    new GameUiPassThroughData(_gameStats)));
        }

        #endregion

        #region Routines

        private IEnumerator LoadGameSceneRoutine(int buildIndex, IPassThroughData data)
        {
            var async = SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Additive);
            async.allowSceneActivation = true;

            yield return async;

            Scene loadedScene = GetLoadedScene(buildIndex);

            foreach (GameObject root in loadedScene.GetRootGameObjects())
            {
                var sceneManager = root.GetComponentInChildren<BaseSceneManager>();
                if (sceneManager == null)
                {
                    continue;
                }

                sceneManager.Setup(data);
                break;
            }
        }

        #endregion

        #region Methods

        private static Scene GetLoadedScene(int buildIndex)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene scene = SceneManager.GetSceneAt(i);
                if (scene.buildIndex == buildIndex)
                {
                    return scene;
                }
            }

            throw new System.Exception($"Scene with build index {buildIndex} hasn't been loaded");
        }

        #endregion
    }
}
