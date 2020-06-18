using BalsamicBits.BouncyTrash.Extensions;
using BalsamicBits.BouncyTrash.Game.Core;
using DG.Tweening;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Bouncer
{
    public class BouncerAnimator : MonoBehaviour, IAnimatable
    {
        [SerializeField] private Transform target;

        private float AnimationDuration => GameTimings.FrameDuration * 0.9f / GameTimings.TimeScale;

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

            target.DOPunchScale(new Vector3(0, -0.2f, 0), duration, 1);

            _lastAnimateTime = GameTimings.Time;
        }
    }
}
