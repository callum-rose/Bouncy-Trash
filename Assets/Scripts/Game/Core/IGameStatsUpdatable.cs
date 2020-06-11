namespace BalsamicBits.BouncyTrash.Game.Core
{
    internal interface IGameStatsUpdatable
    {
        void UpdateLives(int livesDelta);
        void UpdateScore(int scoreDelta);
    }
}