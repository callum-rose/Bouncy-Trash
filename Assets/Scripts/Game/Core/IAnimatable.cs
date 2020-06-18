using IUnified;
using System;

namespace BalsamicBits.BouncyTrash.Game.Core
{
    public interface IAnimatable
    {
        void Animate();
    }

    [Serializable]
    public class IAnimatableContainer : IUnifiedContainer<IAnimatable>
    {
    }
}
