namespace BalsamicBits.BouncyTrash.Game.Core
{
    internal interface IGameStatsReadable
    {
        int Lives { get; }
        int Score { get; }
    }
}