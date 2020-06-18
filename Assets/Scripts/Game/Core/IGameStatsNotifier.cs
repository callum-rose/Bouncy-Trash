using System;

namespace BalsamicBits.BouncyTrash.Game.Core
{
    public interface IGameStatsNotifier
    {
        event Action<int> LivesChanged;
        event Action<int> ScoreChanged;
        event Action<int> CoinsChanged;
    }
}