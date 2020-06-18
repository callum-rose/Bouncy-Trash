using BalsamicBits.BouncyTrash.Game.Core;
using System;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Projectile
{
    public class Projectile : MonoBehaviour, IDisposable
    {
        public ProjectileKind Kind { get; private set; }
        public IAnimatable Animatable { get; private set; }

        private void Awake()
        {
            Animatable = GetComponentInChildren<IAnimatable>();
        }

        public void SetType(ProjectileKind type)
        {
            Kind = type;
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}