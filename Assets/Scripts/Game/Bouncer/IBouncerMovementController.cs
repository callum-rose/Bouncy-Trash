using BalsamicBits.BouncyTrash.Game.Core;
using System;

namespace BalsamicBits.BouncyTrash.Game.Bouncer
{
    public interface IBouncerMovementController : IDestructable
    {
        event Action<int> Moved;
    }
}