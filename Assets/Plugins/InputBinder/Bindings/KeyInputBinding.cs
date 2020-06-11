using UnityEngine;
using UnityEngine.Events;
using System;

namespace InputBinding
{
    [Serializable]
    public class KeyInputBinding : InputBinding
    {
        [SerializeField]
        private KeyCode key;

        [SerializeField]
        protected InputEvent inputEvent;

#if INPUT_BINDINGS_USE_UNITY_EVENTS
        [SerializeField]
        protected ActionEvent boundEvent = new ActionEvent();
#else
        protected event Action boundEvent;
#endif

        public KeyInputBinding(KeyCode key, InputEvent inputEvent,
#if INPUT_BINDINGS_USE_UNITY_EVENTS
            UnityAction action
#else
            Action action
#endif
            )
        {
            this.key = key;
            this.inputEvent = inputEvent;
#if INPUT_BINDINGS_USE_UNITY_EVENTS
            this.boundEvent.AddListener(action);
#else
            boundEvent += action;
#endif

            UpdateInspectorName();
        }

        protected bool ActionOccurred()
        {
            return (inputEvent == InputEvent.Pressed && Input.GetKeyDown(key)) || (inputEvent == InputEvent.Released && Input.GetKeyUp(key)) || (inputEvent == InputEvent.Held && Input.GetKey(key));
        }

        public override void Execute()
        {
            if (ActionOccurred())
            {
                boundEvent.Invoke();
            }
        }

        public void AddListener(
#if INPUT_BINDINGS_USE_UNITY_EVENTS
            UnityAction action
#else
            Action action
#endif
            )
        {
#if INPUT_BINDINGS_USE_UNITY_EVENTS
            boundEvent.AddListener(action);
#else
            boundEvent += action;
#endif
        }

        public bool IsEquivalent(KeyCode key, InputEvent inputEvent)
        {
            return this.key == key && this.inputEvent == inputEvent;
        }

        public override void UpdateInspectorName()
        {
            inspectorName = key.ToString() + " - " + inputEvent.ToString();
        }
    }
}
