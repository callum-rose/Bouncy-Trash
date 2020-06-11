using BalsamicBits.BouncyTrash.Extensions;
using BalsamicBits.BouncyTrash.Game.Core;
using DG.Tweening;
using UnityEngine;

namespace BalsamicBits.BouncyTrash
{
    public class BouncerAnimator : MonoBehaviour, IAnimatable
    {
        [SerializeField] private Transform target;

        private float AnimationDuration => GameTimings.SmallestBounceDuration * 0.5f / GameTimings.TimeScale;

        private Vector3 _initialScale;

        private float _lastAnimateTime = float.MinValue;

        private void Awake()
        {
            _initialScale = target.localScale;
        }

        public void Animate()
        {
            float duration = AnimationDuration;
            if (GameTimings.Time - _lastAnimateTime < duration)
            {
                return;
            }

            target.DOPunchScale(_initialScale.SetY(0.5f), duration, 1);

            _lastAnimateTime = GameTimings.Time;
        }
    }
}
