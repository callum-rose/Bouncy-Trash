using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Projectile.Path
{
    internal class PathTraverser
    {
        public readonly IPath Path;

        public Vector2 CurrentPosition => Path.Evaluate(NormalisedTime);
        public float Progress => NormalisedTime;

        private float NormalisedTime => _internalTime / _duration;

        private readonly float _duration;

        private float _internalTime;

        public PathTraverser(IPath path, float duration)
        {
            _duration = duration;
            Path = path;
        }

        public void Advance(float deltaTime)
        {
            _internalTime += deltaTime;
        } 
    }
}