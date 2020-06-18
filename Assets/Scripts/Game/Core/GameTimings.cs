using DG.Tweening;
using Sirenix.OdinInspector;
using static UnityEngine.Time;

namespace BalsamicBits.BouncyTrash.Game.Core
{
    public static class GameTimings
    {
        public static float Time => time;
        public static float DeltaTime => deltaTime;
        [PropertyRange(0, 1)]
        public static float TimeScale
        {
            get => timeScale;
            set
            {
                timeScale = DOTween.timeScale = value;
            }
        }

        public static float SmallestBounceDuration => 1;
        public static int FramesPerSmallBounce => 2;
        public static float FrameDuration => SmallestBounceDuration / FramesPerSmallBounce;

        public static float GetProjectileDuration(int storey)
        {
            return SmallestBounceDuration * storey;
        }
    }
}