using BalsamicBits.BouncyTrash.Game.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Debug
{
    [ExecuteInEditMode]
    internal class DebugPositioner : MonoBehaviour
	{
#pragma warning disable 0647
        [SerializeField, Range(0, GameDimensions.StoreyCount)] private int storey;
        [SerializeField, Range(0, GameDimensions.PositionCountIncEnd)] private int position;
#pragma warning restore 0647

        #region Unity

        private void Update()
        {
            OnValidate();
        }

        private void OnValidate()
        {
            transform.position = GameDimensions.GetPositionAtStorey(position, storey);
        }

        #endregion

        #region API



        #endregion

        #region Methods



        #endregion
    }
}