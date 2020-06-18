using BalsamicBits.BouncyTrash.Core;
using BalsamicBits.BouncyTrash.Game.Projectile;
using BalsamicBits.BouncyTrash.Game.Projectile.Scheduler;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Core
{
    [CreateAssetMenu(fileName = "Stage", menuName = "ScriptableObjects/Game/" + nameof(StandardStageData))]
    internal class StandardStageData : BaseStageData
    {
        private const string ComponentsTag = "Components";

        [SerializeField]
        [BoxGroup(ComponentsTag)]
        [InlineEditor(Expanded = true)]
        [InfoBox("Must be non null", nameof(SchedulerNull), InfoMessageType = InfoMessageType.Error)]
        [GUIColor(1, 0.75f, 0.75f)]
        [AssetsOnly]
        private ProjectileSchedule scheduler;

        private bool SchedulerNull => scheduler == null;

        [Space]

        [SerializeField]
        [BoxGroup(ComponentsTag)]
        [InlineEditor(Expanded = true)]
        [InfoBox("Must be non null", nameof(MovementNull), InfoMessageType = InfoMessageType.Error)]
        [GUIColor(0.75f, 1, 0.75f)]
        [AssetsOnly]
        private ProjectilesMovementManager movement;
        private bool MovementNull => movement == null;

        [Space]

        [SerializeField]
        [BoxGroup(ComponentsTag)]
        [InlineEditor(Expanded = true)]
        [InfoBox("Must be non null", nameof(KindSelectorNull), InfoMessageType = InfoMessageType.Error)]
        [GUIColor(0.75f, 0.75f, 1)]
        [AssetsOnly]
        private ProjectileKindSelector kindSelector;
        private bool KindSelectorNull => kindSelector == null;

        public override IProjectileSchedule ProjectileSchedule => scheduler as IProjectileSchedule;
        public override IProjectileMovementHandler ProjectileMovementHandler => movement as IProjectileMovementHandler;
        public override IProjectileKindSelector ProjectileKindSelector => kindSelector as IProjectileKindSelector;
    }
}