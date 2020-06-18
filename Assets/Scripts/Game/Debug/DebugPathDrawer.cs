using BalsamicBits.BouncyTrash.Game.Core;
using BalsamicBits.BouncyTrash.Game.Projectile.Path;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Debug
{
    internal class DebugPathDrawer : MonoBehaviour
	{
#pragma warning disable 0647
        [SerializeField] private int resolution = 30;
#pragma warning restore 0647

        private IPath _path;

        private int _calculatedPathStorey;

        #region Unity

        private void Awake()
        {
        }

        private void OnDrawGizmos()
        {
            if (_path == null)
            {
                return;
            }

            switch (_calculatedPathStorey)
            {
                case 1:
                default:
                    Gizmos.color = new Color(1, 0, 0);
                    break;
                case 2:
                    Gizmos.color = new Color(1, 0.1f, 0.1f);
                    break;
                case 3:
                    Gizmos.color = new Color(1, 0.2f, 0.2f);
                    break;
            }

            float delta = 1f / resolution;
            for (float t = 0f; t < 1f; t += delta)
            {
                Vector3 startPos = _path.Evaluate(t);
                Vector3 endPos = _path.Evaluate(t + delta);
                Gizmos.DrawLine(startPos, endPos);
            }
        }

        #endregion

        #region API

        public void SetPath(IPath path)
        {
            _path = path;
            _calculatedPathStorey = CalcPathStorey(path);
        }

        #endregion

        #region Methods

        private int CalcPathStorey(IPath path)
        {
            float maxHeight = 0;
            float delta = 1f / resolution;
            for (float t = 0f; t < 1f; t += delta)
            {
                float y = path.Evaluate(t).y;
                if (y > maxHeight)
                {
                    maxHeight = y;
                }
            }

            return Mathf.RoundToInt(maxHeight / GameDimensions.StoreyHeight);
        }

        #endregion
    }
}