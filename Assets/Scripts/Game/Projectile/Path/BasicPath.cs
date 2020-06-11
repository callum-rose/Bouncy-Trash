using BalsamicBits.BouncyTrash.Game.Core;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Projectile.Path
{
    internal class BasicPath : IPath
    {
        private BasicPath(float length, float height)
        {
            _length = length;
            _height = height;
        }

        private readonly Vector2 _start = Vector2.zero;

        private readonly float _length;
        private readonly float _height;

        public Vector2 Evaluate(float t)
        {
            return new Vector2
            {
                x = _start.x + _length * t,
                y = _start.y + _height * EvaluateParabola(t)
            };
        }

        private static float EvaluateParabola(float t)
        {
            return 4f * t * (1 - t);
        }

        public class Factory : IPathFactory
        {
            public IPath CreateInstance(int storey)
            {
                return new BasicPath(GameDimensions.PositionWidth, GameDimensions.StoreyHeight * storey);
            }
        }
    }
}