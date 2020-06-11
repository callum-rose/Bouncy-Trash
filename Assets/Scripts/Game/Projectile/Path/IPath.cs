using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Projectile.Path
{
    internal interface IPath
    {
        Vector2 Evaluate(float t);
    }
}