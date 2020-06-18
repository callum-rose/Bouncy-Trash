using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Core
{
    public static class GameDimensions
    {
        public static Vector2 Origin => new Vector2(0, 2);

        public static int StoreyCount => 3;
        public static float StoreyHeight => 4f;
        public static Vector2 StoreyOffset => Vector2.up * StoreyHeight;

        public static int PositionCount => 3;
        public static int PositionCountIncEnd => PositionCount + 1;
        public static float PositionWidth => 2f;
        public static Vector2 PositionOffset => Vector2.right * PositionWidth;

        public static Vector2 BouncerBaseOffset => new Vector2(0, -2);

        public static float DestroyHeight => -5f;

        public static Vector2 GetPosition(int position)
        {
            return Origin + PositionOffset * position;
        }

        public static Vector2 GetBouncerPosition(int position)
        {
            return Origin + PositionOffset * position + BouncerBaseOffset;
        }

        public static Vector2 GetStoreySpawnPoint(int storey)
        {
            return Origin + StoreyOffset * storey;
        }

        public static Vector2 GetPositionAtStorey(int position, int storey)
        {
            return Origin + PositionOffset * position + StoreyOffset * storey;
        }

        public static int GetPositionIndexFrom(Vector3 worldPosition)
        {
            return Mathf.RoundToInt((worldPosition.x - Origin.x) / PositionWidth);
        }

        public static bool IsInDestroyZone(Vector3 position)
        {
            return position.y <= DestroyHeight;
        }
    }
}