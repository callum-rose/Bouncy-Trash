using BalsamicBits.BouncyTrash.Game.Core;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Projectile.Path
{
    internal static class PathExtensions
	{
        private readonly static IPathFactory _flatPathFactory = new FlatPath.Factory();

        public static IPath GetFirstHalfFlattened(this IPath path, int storey)
        {
            IPath flatPath = _flatPathFactory.CreateInstance(storey).GetStartAboveOrigin();
            return new HalfSplicedPath(flatPath, path);
        }

        public static IPath GetStartAboveOrigin(this IPath path)
        {
            return path.GetOffset(GameDimensions.Origin);
        }

        public static IPath GetFollowOnFrom(this IPath path, IPath previousPath)
        {
            Vector2 prevPathEnd = previousPath.Evaluate(1f);
            return path.GetOffset(prevPathEnd);
        }

        private static IPath GetOffset(this IPath path, Vector2 offset)
        {
            return new OffsetPath(path, offset);
        }
    }
}