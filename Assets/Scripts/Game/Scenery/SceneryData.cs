using BalsamicBits.BouncyTrash.Core;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BalsamicBits.BouncyTrash.Game.Scenery
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/" + nameof(SceneryData))]
    public class SceneryData : NameAndIdScriptableObject
	{
        [SerializeField]
        private SceneField scene;

        public int SceneBuildIndex => scene.BuildIndex;
	}
}