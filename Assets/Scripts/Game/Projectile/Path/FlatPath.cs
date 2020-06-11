using BalsamicBits.BouncyTrash.Game.Core;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Projectile.Path
{
    internal class FlatPath : IPath
    {
        private readonly Vector3 _startPos = Vector2.zero;

        private readonly float _length;
        private readonly float _height;

        private FlatPath(float length, float height)
        {
            _length = length;
            _height = height;
        }

        public Vector2 Evaluate(float t)
        {
            return _startPos + new Vector3(_length * t, _height);
        }

        public class Factory : IPathFactory
        {
            public IPath CreateInstance(int storey)
            {
                return new FlatPath(GameDimensions.PositionWidth, 
                    GameDimensions.StoreyHeight * storey);
            }
        }
    }
}