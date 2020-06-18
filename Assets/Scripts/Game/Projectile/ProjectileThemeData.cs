using BalsamicBits.BouncyTrash.Core;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Projectile
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Projectile/" + nameof(ProjectileThemeData), order = 1)]
    public class ProjectileThemeData : NameAndIdScriptableObject
    {
        [BoxGroup("General")]
        [OdinSerialize]
        [FolderPath(RequireExistingPath = true, ParentFolder = "Assets/Resources")]
        private string resourcePath;

        public string ResourcePath => resourcePath;

#pragma warning disable 0647
#pragma warning restore 0647

        #region Unity



        #endregion

        #region API



        #endregion

        #region Methods



        #endregion
    }
}