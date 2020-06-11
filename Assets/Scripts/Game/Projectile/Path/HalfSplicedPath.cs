using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Projectile.Path
{
    internal class HalfSplicedPath : IPath
    {
        private readonly IPath _firstPath;
        private readonly IPath _secondPath;

        public HalfSplicedPath(IPath firstPath, IPath secondPath)
        {
            _firstPath = firstPath;
            _secondPath = secondPath;
        }

        public Vector2 Evaluate(float t)
        {
            if (t < 0.5f)
            {
                return _firstPath.Evaluate(t);
            }

            return _secondPath.Evaluate(t);
        }
    }
}