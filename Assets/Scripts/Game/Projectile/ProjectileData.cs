using UnityEngine;
using Sirenix.OdinInspector;
using System;
using UnityEditor;
using BalsamicBits.BouncyTrash.Core;
using UnityEngine.Serialization;

namespace BalsamicBits.BouncyTrash.Game.Projectile
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/" + nameof(ProjectileData), order = 1)]
    public class ProjectileData : BaseModelScriptableObject
    {
        [BoxGroup("General")]
        [SerializeField]
        [FormerlySerializedAs("type")]
        [LabelWidth(labelWidth)]
        private ProjectileKind kind;

        public ProjectileKind Kind => kind;

        #region Odin


        #endregion
    }
}