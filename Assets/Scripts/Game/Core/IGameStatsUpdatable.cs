namespace BalsamicBits.BouncyTrash.Game.Core
{
    public interface IGameStatsUpdatable
    {
        void UpdateLives(int livesDelta);
        void UpdateScore(int scoreDelta);
        void UpdateCoins(int coinsDelta);
    }
}