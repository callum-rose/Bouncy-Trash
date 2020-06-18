using BalsamicBits.BouncyTrash.Game.Core;
using UnityEngine;
using Sirenix.OdinInspector;

namespace BalsamicBits.BouncyTrash.Game.Debug
{
    [ExecuteInEditMode]
    public class DebugPositioner : MonoBehaviour
    {
#pragma warning disable 0647
        [SerializeField] private bool usePredefinedLocation;

        [SerializeField]
        [ShowIf(nameof(usePredefinedLocation))]
        private Location definedLocation;

        [SerializeField]
        [HideIfGroup("Data", MemberName = nameof(usePredefinedLocation))]
        [PropertyRange(1, nameof(StoreyCount))]
        private int storey;
        [SerializeField]
        [HideIfGroup("Data", MemberName = nameof(usePredefinedLocation))]
        [PropertyRange(1, nameof(PositionCountIncEnd))]
        private int position;
#pragma warning restore 0647

        private int StoreyCount => GameDimensions.StoreyCount;
        private int PositionCountIncEnd => GameDimensions.PositionCountIncEnd;

        #region Unity

        private void Update()
        {
            SetPosition();
        }

        private void OnValidate()
        {
            SetPosition();
        }

        #endregion

        #region Methods

        private void SetPosition()
        {
            int tempPos, tempStorey;

            if (usePredefinedLocation)
            {
                switch (definedLocation)
                {
                    case Location.TopLeft:
                        tempPos = 0;
                        tempStorey = GameDimensions.StoreyCount;
                        break;
                    case Location.TopRight:
                        tempPos = GameDimensions.PositionCountIncEnd;
                        tempStorey = GameDimensions.StoreyCount;
                        break;
                    case Location.BottomRight:
                        tempPos = GameDimensions.PositionCountIncEnd;
                        tempStorey = 0;
                        break;
                    case Location.BottomLeft:
                        tempPos = 0;
                        tempStorey = 0;
                        break;
                    default:
                        throw new System.Exception();
                }
            }
            else
            {
                tempPos = position;
                tempStorey = storey;
            }

            transform.position = GameDimensions.GetPositionAtStorey(tempPos, tempStorey);
        }

        #endregion

        #region Enums

        [System.Serializable]
        public enum Location
        {
            TopLeft, TopRight, BottomRight, BottomLeft
        }

        #endregion

    }
}