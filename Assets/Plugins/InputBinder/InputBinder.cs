using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using System.Linq;
using System;

namespace InputBinding
{
    public class InputBinder : MonoBehaviour
    {
        [SerializeField]
        private List<AxisInputBinding> axisBindings = new List<AxisInputBinding>();

        [SerializeField]
        private List<ButtonInputBinding> buttonBindings = new List<ButtonInputBinding>();

        [SerializeField]
        private List<KeyInputBinding> keyBindings = new List<KeyInputBinding>();

        private InputBinder()
        {
            // must be added to a gameobject so prohibit use of constructor
        }

        // *************************************
        // API
        // *************************************

        public void BindAxis(string name,
#if INPUT_BINDINGS_USE_UNITY_EVENTS
            UnityAction<float> action
#else
            Action<float> action
#endif
            )
        {
            AxisInputBinding axisBinding = axisBindings.FirstOrDefault(binding => binding.IsEquivalent(name));

            if (axisBinding == null)
            {
                axisBindings.Add(new AxisInputBinding(name, action));
            }
            else
            {
                axisBinding.AddListener(action);
            }
        }

        public void BindAxis(IEnumerable<string> names,
#if INPUT_BINDINGS_USE_UNITY_EVENTS
            UnityAction<float> action
#else
            Action<float> action
#endif
            )
        {
            foreach (string n in names)
            {
                BindAxis(n, action);
            }
        }

        public void BindButton(string name, InputEvent inputEvent,
#if INPUT_BINDINGS_USE_UNITY_EVENTS
            UnityAction action
#else
            Action action
#endif
            )
        {
            ButtonInputBinding buttonBinding = buttonBindings.FirstOrDefault(binding => binding.IsEquivalent(name, inputEvent));

            if (buttonBinding == null)
            {
                buttonBindings.Add(new ButtonInputBinding(name, inputEvent, action));
            }
            else
            {
                buttonBinding.AddListener(action);
            }
        }

        public void BindButton(IEnumerable<string> names, InputEvent inputEvent,
#if INPUT_BINDINGS_USE_UNITY_EVENTS
            UnityAction action
#else
            Action action
#endif
            )
        {
            foreach (string n in names)
            {
                BindButton(n, inputEvent, action);
            }
        }

        public void BindKey(KeyCode key, InputEvent inputEvent,
#if INPUT_BINDINGS_USE_UNITY_EVENTS
            UnityAction action
#else
            Action action
#endif
            )
        {
            KeyInputBinding keyBinding = keyBindings.FirstOrDefault(binding => binding.IsEquivalent(key, inputEvent));

            if (keyBinding == null)
            {
                keyBindings.Add(new KeyInputBinding(key, inputEvent, action));
            }
            else
            {
                keyBinding.AddListener(action);
            }
        }

        public void BindKeys(IEnumerable<KeyCode> keys, InputEvent inputEvent,
#if INPUT_BINDINGS_USE_UNITY_EVENTS
            UnityAction action
#else
            Action action
#endif
            )
        {
            foreach (KeyCode k in keys)
            {
                BindKey(k, inputEvent, action);
            }
        }

        // *************************************
        // UNITY
        // *************************************

        private void Update()
        {
            UpdateAxisBindings();

            ExecuteAxisBindings();

            ExecuteButtonBindings();

            ExecuteKeyBindings();
        }

        private void OnValidate()
        {
            foreach (AxisInputBinding axisBinding in axisBindings)
            {
                axisBinding.UpdateInspectorName();
            }

            foreach (ButtonInputBinding buttonBinding in buttonBindings)
            {
                buttonBinding.UpdateInspectorName();
            }

            foreach (KeyInputBinding keyBinding in keyBindings)
            {
                keyBinding.UpdateInspectorName();
            }
        }

        // *************************************
        // METHODS
        // *************************************

        private void UpdateAxisBindings()
        {
            foreach (AxisInputBinding axisBinding in axisBindings)
            {
                axisBinding.UpdateValue();
            }
        }

        private void ExecuteAxisBindings()
        {
            foreach (AxisInputBinding axisBinding in axisBindings)
            {
                axisBinding.Execute();
            }
        }

        private void ExecuteButtonBindings()
        {
            foreach (ButtonInputBinding buttonBinding in buttonBindings)
            {
                buttonBinding.Execute();
            }
        }

        private void ExecuteKeyBindings()
        {
            foreach (KeyInputBinding keyBinding in keyBindings)
            {
                keyBinding.Execute();
            }
        }
    }
}
