using BalsamicBits.BouncyTrash.Game.Core;
using UnityEngine;

namespace BalsamicBits.BouncyTrash
{
    public class DefaultBouncer : MonoBehaviour, IBouncer
    {
        public int CurrentPosition
        {
            get => _currentPosition;
            private set
            {
                if (value < 1)
                {
                    _currentPosition = 1;
                }
                else if (value > GameDimensions.PositionCount)
                {
                    _currentPosition = GameDimensions.PositionCount;
                }
                else
                {
                    _currentPosition = value;
                }
                transform.localPosition = GameDimensions.GetPosition(CurrentPosition);
            }
        }

        private int _currentPosition;

        private IBouncerMovementController _movementController;

        #region Unity

        private void Awake()
        {
            CurrentPosition = 1;
        }

        private void OnDestroy()
        {
            UnbindController(_movementController);
        }

        #endregion

        #region API

        public void SetMovementController(IBouncerMovementController movementController)
        {
            UnbindController(_movementController);
            _movementController?.Destruct();

            _movementController = movementController;

            BindMovementController(_movementController);
        }

        #endregion

        #region Methods

        private void BindMovementController(IBouncerMovementController movementController)
        {
            movementController.Moved += MoveBy;
        }

        private void UnbindController(IBouncerMovementController movementController)
        {
            if (_movementController != null)
            {
                movementController.Moved -= MoveBy;
            }
        }

        private void MoveBy(int positions)
        {
            MoveTo(CurrentPosition + positions);
        }

        private void MoveTo(int position)
        {
            CurrentPosition = position;
        }

        #endregion
    }
}