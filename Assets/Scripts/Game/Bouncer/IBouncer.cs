using BalsamicBits.BouncyTrash.Game.Core;

namespace BalsamicBits.BouncyTrash.Game.Bouncer
{
    public interface IBouncer : IHasPosition
    {
        IAnimatable Animatable { get; }

        void SetMovementController(IBouncerMovementController movementController);
    }
}