using BalsamicBits.BouncyTrash.Core;
using BalsamicBits.BouncyTrash.Game.Projectile;
using BalsamicBits.BouncyTrash.Game.Projectile.Scheduler;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Core
{
    [CreateAssetMenu(fileName = "Stage", menuName = "ScriptableObjects/Game/" + nameof(StandardStageData))]
    internal abstract class BaseStageData : NameAndIdScriptableObject
    {
        private const string GeneralTag = "General";

        [BoxGroup(GeneralTag)]
        [SerializeField]
        [MinValue(0)]
        private float duration;

        public float Duration => duration;

        public abstract IProjectileSchedule ProjectileSchedule { get; }
        public abstract IProjectileMovementHandler ProjectileMovementHandler { get; }
        public abstract IProjectileKindSelector ProjectileKindSelector { get; }
    }
}