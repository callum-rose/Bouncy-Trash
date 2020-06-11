using System;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Projectile
{
    internal class Projectile : MonoBehaviour, IDisposable
    {
        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}