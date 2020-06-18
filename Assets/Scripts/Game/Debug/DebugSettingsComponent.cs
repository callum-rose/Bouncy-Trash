using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Debug
{
    [ExecuteInEditMode]
	public class DebugSettingsComponent : MonoBehaviour
	{
        [SerializeField] private DebugSettings settings;

        public static DebugSettingsComponent Instance { get; private set; }

        public DebugSettings Settings => settings;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }

            if (Instance != this)
            {
                if (Application.isPlaying)
                {
                    Destroy(this);
                }
                else
                {
                    DestroyImmediate(this);
                }

                UnityEngine.Debug.LogError($"Destroyed duplicate {GetType()}");

                return;
            }
        }
    }
}