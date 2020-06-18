using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Core
{
    public class Offsetor : MonoBehaviour
    {
        [ShowInInspector]
        public Vector3 Offset
        {
            get => transform.localPosition;
            set => transform.localPosition = value;
        }

		#region API

        public void Set(Vector3 offset)
        {
            transform.localPosition = offset;
        }

		#endregion	
	}
}