using BalsamicBits.BouncyTrash.Game.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BalsamicBits.BouncyTrash
{
	internal class SpinAnimator : MonoBehaviour, IAnimatable
	{
        private static float initialSpeed = 240;

        [Range(0, 1)]
        private static float dragCoefficient = 0.98f;

        private Vector3 _rotateVel;

        #region Unity

        private void Update()
        {
            transform.Rotate(_rotateVel * GameTimings.DeltaTime);
            _rotateVel -= _rotateVel * (1f - dragCoefficient) * GameTimings.TimeScale;
        }

        #endregion

        #region API

        public void Animate()
        {
            _rotateVel = initialSpeed * Random.onUnitSphere;
        }

		#endregion	

		#region Methods



		#endregion	
	}
}