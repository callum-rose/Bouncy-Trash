using BalsamicBits.BouncyTrash.Game.Core;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Core
{
    public abstract class BaseModelScriptableObject : NameAndIdScriptableObject
    {
        private const string modelTag = "Model";

        [ShowInInspector]
        [BoxGroup(modelTag)]
        [SerializeField]
        [AssetsOnly]
        [LabelWidth(labelWidth)]
        [SuffixLabel("Prefab")]
        [OnValueChanged(nameof(SetPrefabTransformAsOffset))]
        private GameObject model;

        [BoxGroup(modelTag)]
        [LabelWidth(labelWidth)]
        [SerializeField]
        [ReadOnly]
        private Vector3 offset;

        private bool ShowPreview => model != null;
        [BoxGroup(modelTag)]
        [ShowInInspector]
        [LabelWidth(labelWidth)]
        [PreviewField(200, Alignment = ObjectFieldAlignment.Right)]
        [HideLabel]
        [ShowIf(nameof(ShowPreview))]
        private GameObject PreviewGameObject => model ?? null;

        public GameObject ModelPrefab => model;
        public Vector3 Offset => offset;

        #region Unity

        protected void Awake()
        {
            if (id != Guid.Empty)
            {
                return;
            }

            id = Guid.NewGuid();
            Debug.Log($"Guid of {id} generated for new {GetType().FullName}");

            OnAwake();
        }

        protected virtual void OnAwake()
        {

        }

        #endregion

        #region Odin

        [Button("Open Prefab")]
        [BoxGroup(modelTag)]
        [ShowIf(nameof(ShowPreview))]
        private void OpenPrefab()
        {
            AssetDatabase.OpenAsset(model.GetInstanceID());
        }

        [Button("Set Prefab Transform Position As Offset")]
        [BoxGroup(modelTag)]
        [ShowIf(nameof(ShowPreview))]
        private void SetPrefabTransformAsOffset()
        {
            if (model == null)
            {
                return;
            }

            offset = model.GetComponentInChildren<Offsetor>().Offset;
        }

        #endregion
    }
}