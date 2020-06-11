using UnityEngine;

namespace InputBinding
{
    public abstract class InputBinding
    {
        [SerializeField, HideInInspector]
        protected string inspectorName;

        public abstract void Execute();

        public abstract void UpdateInspectorName();
    }
}
