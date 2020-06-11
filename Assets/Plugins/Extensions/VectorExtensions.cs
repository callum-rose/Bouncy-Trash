using UnityEngine;

namespace BalsamicBits.BouncyTrash.Extensions
{
    public static class VectorExtensions {

        /// <summary>
        /// Rotate this vector by angle in radians
        /// </summary>
        /// <param name="vec"></param>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static Vector2 Rotate(this Vector2 vec, float angle)
        {
            float c = Mathf.Cos(angle);
            float s = Mathf.Sin(angle);
            return new Vector2(vec.x * c - vec.y * s, vec.x * s + vec.y * c);
        }

        public static Vector2 SetX(this Vector2 vec, float x)
        {
            return new Vector2(x, vec.y);
        }

        public static Vector2 SetY(this Vector2 vec, float y)
        {
            return new Vector2(vec.x, y);
        }

        public static Vector3 SetX(this Vector3 vec, float x)
        {
            return new Vector3(x, vec.y, vec.z);
        }

        public static Vector3 SetY(this Vector3 vec, float y)
        {
            return new Vector3(vec.x, y, vec.z);
        }

        public static Vector3 SetZ(this Vector3 vec, float z)
        {
            return new Vector3(vec.x, vec.y, z);
        }

        public static Vector2 AddX(this Vector2 vec, float x)
        {
            return new Vector2(vec.x + x, vec.y);
        }

        public static Vector2 AddY(this Vector2 vec, float y)
        {
            return new Vector2(vec.x, vec.y + y);
        }
    }
}
