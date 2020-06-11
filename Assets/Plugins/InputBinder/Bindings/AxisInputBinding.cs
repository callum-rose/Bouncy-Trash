using UnityEngine;
using UnityEngine.Events;
using System;

namespace InputBinding
{
    [System.Serializable]
    public class AxisInputBinding : InputBinding
    {
        [SerializeField]
        private string name;

#if INPUT_BINDINGS_USE_UNITY_EVENTS
        [SerializeField]
        protected AxisEvent boundEvent = new AxisEvent();
#else
        protected event Action<float> boundEvent;
#endif

        private float value { get; set; }

        public AxisInputBinding(string name,
#if INPUT_BINDINGS_USE_UNITY_EVENTS
            UnityAction<float> action
#else
            Action<float> action
#endif            
            )
        {
            this.name = name;
#if INPUT_BINDINGS_USE_UNITY_EVENTS
            this.boundEvent.AddListener(action);
#else
            this.boundEvent += action;
#endif
            this.value = 0f;

            UpdateInspectorName();
        }

        public void UpdateValue()
        {
            value = Input.GetAxis(name);
        }

        public override void Execute()
        {
            boundEvent.Invoke(value);
        }

        public void AddListener(
#if INPUT_BINDINGS_USE_UNITY_EVENTS
            UnityAction<float> action
#else
            Action<float> action
#endif            
            )
        {
#if INPUT_BINDINGS_USE_UNITY_EVENTS
            boundEvent.AddListener(action);
#else
            boundEvent += action;
#endif
        }

        public bool IsEquivalent(string name)
        {
            return this.name == name;
        }

        public override void UpdateInspectorName()
        {
            inspectorName = name;
        }
    }
}
