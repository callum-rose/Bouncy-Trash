using UnityEngine;
using UnityEngine.Events;
using System;

namespace InputBinding
{
    [System.Serializable]
    public class ButtonInputBinding : InputBinding
    {
        [SerializeField]
        private string name;

        [SerializeField]
        protected InputEvent inputEvent;

#if INPUT_BINDINGS_USE_UNITY_EVENTS
        [SerializeField]
        protected ActionEvent boundEvent = new ActionEvent();
#else
        protected event Action boundEvent;
#endif

        public ButtonInputBinding(string name, InputEvent inputEvent,
#if INPUT_BINDINGS_USE_UNITY_EVENTS
            UnityAction action
#else
            Action action
#endif
            )
        {
            this.name = name;
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
            return (inputEvent == InputEvent.Pressed && Input.GetButtonDown(name)) || (inputEvent == InputEvent.Released && Input.GetButtonUp(name)) || (inputEvent == InputEvent.Held && Input.GetButton(name));
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

        public bool IsEquivalent(string name, InputEvent inputEvent)
        {
            return this.name == name && this.inputEvent == inputEvent;
        }

        public override void UpdateInspectorName()
        {
            inspectorName = name + " - " + inputEvent.ToString();
        }
    }
}