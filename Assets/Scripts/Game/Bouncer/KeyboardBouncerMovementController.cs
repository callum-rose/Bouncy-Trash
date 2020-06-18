using System;
using InputBinding;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Bouncer
{
    public class KeyboardBouncerMovementController : MonoBehaviour, IBouncerMovementController
    {
        public event Action<int> Moved;
        
        private InputBinder _inputBinder;

        #region Unity

        private void Awake()
        {
            _inputBinder = gameObject.AddComponent<InputBinder>();
            _inputBinder.BindKeys(new[] {KeyCode.LeftArrow, KeyCode.A}, InputEvent.Pressed, MoveBouncerLeft);
            _inputBinder.BindKeys(new[] {KeyCode.RightArrow, KeyCode.D}, InputEvent.Pressed, MoveBouncerRight);
        }

        #endregion

        #region API

        public void Destruct()
        {
            Destroy(this);
        }

        #endregion

        #region Methods

        private void MoveBouncerLeft()
        {
            Moved?.Invoke(-1);
        }

        private void MoveBouncerRight()
        {
            Moved?.Invoke(1);
        }

        #endregion
    }
}