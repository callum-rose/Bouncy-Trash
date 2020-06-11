using BalsamicBits.BouncyTrash.Game.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Debug
{
    internal class DebugStoreyVisualiser : MonoBehaviour
    {
#pragma warning disable 0647
        [SerializeField] private bool doDraw = true;
#pragma warning restore 0647

        #region Unity

        private void OnDrawGizmos()
        {
            if (!doDraw)
            {
                return;
            }

            Gizmos.color = Color.green;
            for (int i = 1; i <= GameDimensions.StoreyCount; i++)
            {
                Gizmos.DrawSphere(GameDimensions.GetStoreySpawnPoint(i), 0.1f);
            }

            // draw grid

            Gizmos.color = Color.green;
            for (int s = 0; s <= GameDimensions.StoreyCount; s++)
            {
                Gizmos.DrawLine(GameDimensions.GetPositionAtStorey(0, s), GameDimensions.GetPositionAtStorey(GameDimensions.PositionCountIncEnd, s));
            }
            for (int p = 0; p <= GameDimensions.PositionCountIncEnd; p++)
            {
                Gizmos.DrawLine(GameDimensions.GetPositionAtStorey(p, 0), GameDimensions.GetPositionAtStorey(p, GameDimensions.StoreyCount));
            }
        }

        #endregion
    }
}