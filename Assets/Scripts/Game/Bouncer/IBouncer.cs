namespace BalsamicBits.BouncyTrash
{
    public interface IBouncer : IHasPosition
    {
        void SetMovementController(IBouncerMovementController movementController);
    }
}