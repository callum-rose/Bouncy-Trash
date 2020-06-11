using static UnityEngine.Time;

namespace BalsamicBits.BouncyTrash.Game.Core
{
    public static class GameTimings
    {
        public static float Time => time;
        public static float DeltaTime => deltaTime;
        public static float TimeScale
        {
            get => timeScale;
            set
            {
                timeScale = value;
            }
        }

        public const float SmallestBounceDuration = 1;

        public static float GetProjectileDuration(int storey)
        {
            return SmallestBounceDuration * storey;
        }
    }
}