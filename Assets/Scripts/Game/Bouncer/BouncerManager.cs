using UnityEngine;

namespace BalsamicBits.BouncyTrash
{
    public class BouncerManager : MonoBehaviour
    {
        public IHasPosition Position => _bouncer as IHasPosition;
        public IAnimatable Animator { get; private set; }

        private IBouncer _bouncer;

        #region Unity

        private void Awake()
        {
            _bouncer = GetComponent<IBouncer>();
            _bouncer.SetMovementController(GetMovementController());

            Animator = GetComponent<IAnimatable>();
        }

        #endregion

        #region Methods

        private IBouncerMovementController GetMovementController()
        {
            return GetComponent<IBouncerMovementController>();
        }

        private void RemoveCurrentMovementController()
        {
            gameObject.GetComponent<IBouncerMovementController>()?.Destruct();
        }

        #endregion
    }
}