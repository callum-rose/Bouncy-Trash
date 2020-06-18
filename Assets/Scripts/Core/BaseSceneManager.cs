using UnityEngine;

namespace BalsamicBits.BouncyTrash.Core
{
    public abstract class BaseSceneManager : MonoBehaviour
    {
        public abstract void Setup(IPassThroughData data);
    }
}