using System;

namespace BalsamicBits.BouncyTrash
{
    public interface IBouncerMovementController : IDestructable
    {
        event Action<int> Moved;
    }
}