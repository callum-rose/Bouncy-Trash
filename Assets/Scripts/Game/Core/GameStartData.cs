using BalsamicBits.BouncyTrash.Core;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BalsamicBits.BouncyTrash.Game.Core
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/" + nameof(GameStartData))]
    public class GameStartData : SerializedScriptableObject
    {
        [BoxGroup("Scenes To Load")]
        [SerializeField]
        [LabelWidth(80)]
        private SceneField game, scenery, ui;

        public int GameSceneBuildIndex => game.BuildIndex;
        public int ScenerySceneBuildIndex => scenery.BuildIndex;
        public int UiSceneBuildIndex => ui.BuildIndex;
    }
}