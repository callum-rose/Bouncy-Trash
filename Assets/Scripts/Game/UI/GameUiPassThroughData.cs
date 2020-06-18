using BalsamicBits.BouncyTrash.Core;
using BalsamicBits.BouncyTrash.Game.Core;

namespace BalsamicBits.BouncyTrash.Game.UI
{
    public class GameUiPassThroughData : IPassThroughData
    {
        public GameUiPassThroughData(IGameStatsNotifier gameStatsReader)
        {
            GameStatsReader = gameStatsReader;
        }

        public IGameStatsNotifier GameStatsReader { get; }
    }
}