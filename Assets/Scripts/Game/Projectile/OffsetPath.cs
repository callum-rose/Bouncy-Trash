using BalsamicBits.BouncyTrash.Game.Projectile.Path;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Projectile
{
    internal class OffsetPath : IPath
    {
        private readonly IPath _refPath;
        private readonly Vector2 _offset;

        public OffsetPath(IPath refPath, Vector2 offset)
        {
            _refPath = refPath;
            _offset = offset;
        }

        public Vector2 Evaluate(float t)
        {
            return _refPath.Evaluate(t) + _offset;
        }
    }
}