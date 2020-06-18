using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.Utilities.Editor;
using Sirenix.Utilities;
using Sirenix.OdinInspector;
using BalsamicBits.BouncyTrash.Game.Bouncer;
using System.Linq;
using System;
using BalsamicBits.BouncyTrash.Game.Scenery;
using BalsamicBits.BouncyTrash.Game.Projectile;
using BalsamicBits.BouncyTrash.Game.Core;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using BalsamicBits.BouncyTrash.Core;

namespace BalsamicBits.BouncyTrash
{
    [InitializeOnLoad]
    internal class GameStartWindow : OdinEditorWindow
    {
        private static DateTime test;

        #region Bouncer

        private const string BouncerTag = "Bouncer";

        private BouncerData _currentBouncerData;

        [ValueDropdown(nameof(BouncerDataNames))]
        [ShowInInspector]
        [Required]
        [BoxGroup(BouncerTag, Order = 1)]
        private string BouncerName
        {
            get => _currentBouncerData.Name;
            set => _currentBouncerData = _bouncerData.First(d => d.name == value);
        }

        private bool HasCurrentBouncer => _currentBouncerData != null;

        [ShowInInspector]
        [ReadOnly]
        [ShowIf(nameof(HasCurrentBouncer))]
        [BoxGroup(BouncerTag)]
        private Guid BouncerId => _currentBouncerData?.Id ?? Guid.Empty;

        private List<BouncerData> _bouncerData;
        private List<string> BouncerDataNames => _bouncerData?.Select(d => d.Name)
                .ToList() ?? new List<string>();

        [ShowInInspector]
        [ReadOnly]
        [ShowIf(nameof(HasCurrentBouncer))]
        [BoxGroup(BouncerTag)]
        [PreviewField(100)]
        private GameObject bouncerObjPreview => _currentBouncerData?.ModelPrefab ?? null;

        #endregion

        #region Projectile

        private const string ProjectileTag = "Projectile";

        private ProjectileThemeData _currentProjectileThemeData;

        private bool HasCurrentProjectileData => _currentProjectileThemeData != null;

        [ShowInInspector]
        [BoxGroup(ProjectileTag, Order = 2)]
        [ValueDropdown(nameof(ProjectileDataNames))]
        private string ProjectileTheme
        {
            get => _currentProjectileThemeData?.Name ?? null;
            set => _currentProjectileThemeData = _projectileDatas?.First(d => d.Name == value) ?? null;
        }

        private List<ProjectileThemeData> _projectileDatas;
        private List<string> ProjectileDataNames => _projectileDatas?.Select(d => d.Name)
                .ToList() ?? new List<string>();

        [ShowInInspector]
        [ReadOnly]
        [ShowIf(nameof(HasCurrentProjectileData))]
        [BoxGroup(ProjectileTag, Order = 2)]
        private Guid ProjectileThemeDataId => _currentProjectileThemeData?.Id ?? Guid.Empty;

        [ShowInInspector]
        [BoxGroup(ProjectileTag, Order = 2)]
        [ReadOnly]
        [ShowIf(nameof(HasCurrentProjectileData))]
        [LabelText("Resource Path")]
        private string ProjectileThemePath => _currentProjectileThemeData.ResourcePath;

        #endregion

        #region Scenery

        private const string SceneryTag = "Scenery";

        private SceneryData _currentSceneryData;

        private bool HasCurrentSceneryData => _currentSceneryData != null;

        [ShowInInspector]
        [BoxGroup(SceneryTag, Order = 3)]
        [ValueDropdown(nameof(ProjectileDataNames))]
        private string SceneryTheme
        {
            get => _currentSceneryData?.Name ?? null;
            set => _currentSceneryData = _sceneryDatas?.First(d => d.Name == value) ?? null;
        }

        private List<SceneryData> _sceneryDatas;
        private List<string> SceneryDataNames => _sceneryDatas?.Select(d => d.Name)
                .ToList() ?? new List<string>();

        [ShowInInspector]
        [ReadOnly]
        [ShowIf(nameof(HasCurrentSceneryData))]
        [BoxGroup(SceneryTag, Order = 3)]
        private Guid SceneryDataId => _currentSceneryData?.Id ?? Guid.Empty;

        #endregion

        private const string WasAppStartedHereKey = nameof(WasAppStartedHereKey);
        private const string BouncerIdKey = nameof(BouncerIdKey);
        private const string ProjectileThemeIdKey = nameof(ProjectileThemeIdKey);
        private const string SceneryIdKey = nameof(SceneryIdKey);

        static GameStartWindow()
        {
            EditorApplication.playModeStateChanged += EditorApplication_playModeStateChanged;
        }

        #region Unity

        private void Awake()
        {
            _bouncerData = new BouncerLoader().Load().ToList();
            _currentBouncerData = _bouncerData.First();

            _projectileDatas = new AssetLoader<ProjectileThemeData>(ResourcePaths.Projectiles).Load().ToList();
            _currentProjectileThemeData = _projectileDatas.First();

            _sceneryDatas = new AssetLoader<SceneryData>(ResourcePaths.Scenery).Load().ToList();
            _currentSceneryData = _sceneryDatas.First();
        }

        #endregion

        #region API

        [MenuItem("Callum/Game Starter")]
        private static void OpenWindow()
        {
            var window = GetWindow<GameStartWindow>();

            // Nifty little trick to quickly position the window in the middle of the editor.
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(500, 500);
        }

        #endregion

        #region Events

        private static void EditorApplication_playModeStateChanged(PlayModeStateChange state)
        {
            if (state != PlayModeStateChange.EnteredPlayMode)
            {
                return;
            }

            if (!EditorPrefs.GetBool(WasAppStartedHereKey, false))
            {
                return;
            }

            // reset flag
            EditorPrefs.SetBool(WasAppStartedHereKey, false);

            BouncerData bouncerData;
            {
                Guid idToUse = Guid.Parse(EditorPrefs.GetString(BouncerIdKey));
                BouncerLoader loader = new BouncerLoader();
                bouncerData = loader.Load().First(d => d.Id == idToUse);
            }

            ProjectileThemeData projectileThemeData;
            {
                Guid idToUse = Guid.Parse(EditorPrefs.GetString(ProjectileThemeIdKey));
                var loader = new AssetLoader<ProjectileThemeData>(ResourcePaths.Projectiles);
                projectileThemeData = loader.Load().First(d => d.Id == idToUse);
            }

            SceneryData sceneryData;
            {
                Guid idToUse = Guid.Parse(EditorPrefs.GetString(SceneryIdKey));
                var loader = new AssetLoader<SceneryData>(ResourcePaths.Scenery);
                sceneryData = loader.Load().First(d => d.Id == idToUse);
            }

            Scene startScene = SceneManager.CreateScene("Game Start");
            SceneManager.SetActiveScene(startScene);

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                Scene s = SceneManager.GetSceneAt(i);

                if (s.handle == startScene.handle)
                {
                    continue;
                }

                SceneManager.UnloadSceneAsync(s);
            }

            GameStarter gameStarter = new GameObject().AddComponent<GameStarter>();

            GameStartPassThroughData data = new GameStartPassThroughData(bouncerData, sceneryData, projectileThemeData);

            gameStarter.StartGame(data);

            SceneManager.UnloadSceneAsync(startScene);
        }

        #endregion

        #region Methods

        [Button(ButtonSizes.Gigantic, Name = "Start Game")]
        [EnableIf(nameof(HasCurrentBouncer))]
        [PropertyOrder(4)]
        private void StartGame()
        {
            test = DateTime.Now;

            EditorApplication.isPlaying = true;

            EditorPrefs.SetBool(WasAppStartedHereKey, true);

            EditorPrefs.SetString(BouncerIdKey, _currentBouncerData.Id.ToString());
            EditorPrefs.SetString(ProjectileThemeIdKey, _currentProjectileThemeData.Id.ToString());
            EditorPrefs.SetString(SceneryIdKey, _currentSceneryData.Id.ToString());

            Close();
        }

        #endregion
    }
}