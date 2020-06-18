using BalsamicBits.BouncyTrash.Extensions;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Projectile
{
    internal class ProjectileKindSelector : SerializedMonoBehaviour, IProjectileKindSelector
    {
#pragma warning disable 0647
        [OdinSerialize]
        [OnValueChanged(nameof(UpdateNormalisedWeights))]
        private Dictionary<ProjectileKind, float> _kindWeightDict = EnumExtensions.GetValues<ProjectileKind>().ToDictionary(k => k, k => 1f);
#pragma warning restore 0647

        [ShowInInspector]
        [ReadOnly]
        private Dictionary<ProjectileKind, float> _kindNormalisedWeightDict = EnumExtensions.GetValues<ProjectileKind>().ToDictionary(k => k, k => 1f);

        private void OnValidate()
        {
            UpdateNormalisedWeights();
        }

        public ProjectileKind GetNext()
        {
            float random = UnityEngine.Random.value;

            float cumulativeWeight = 0;
            foreach (var kind in EnumExtensions.GetValues<ProjectileKind>())
            {
                if (random < _kindNormalisedWeightDict[kind] + cumulativeWeight)
                {
                    return kind;
                }

                cumulativeWeight += _kindNormalisedWeightDict[kind];
            }

            throw new Exception();
        }

        private void UpdateNormalisedWeights()
        {
            float totalWeight = _kindWeightDict.Values.Sum();

            foreach (var kind in EnumExtensions.GetValues<ProjectileKind>())
            {
                _kindNormalisedWeightDict[kind] = _kindWeightDict[kind] / totalWeight;
            }
        }
    }
}
