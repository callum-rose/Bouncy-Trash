using BalsamicBits.BouncyTrash.Game.Core;
using UnityEngine;
using UnityEngine.Assertions;

namespace BalsamicBits.BouncyTrash.Game.Bouncer
{
    public class Bouncer : MonoBehaviour, IBouncer
    {
        public IAnimatable Animatable { get; private set; }

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
                transform.localPosition = GameDimensions.GetBouncerPosition(CurrentPosition);
            }
        }
        private int _currentPosition;

        private IBouncerMovementController _movementController;

        #region Unity

        private void Awake()
        {
            Animatable = GetComponentInChildren<IAnimatable>();

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