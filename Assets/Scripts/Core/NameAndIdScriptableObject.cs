using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Core
{
    public abstract class NameAndIdScriptableObject : SerializedScriptableObject
    {
        protected const float labelWidth = 70;

        private const string baseTag = "Base";

        [BoxGroup(baseTag)]
        [SerializeField, LabelWidth(labelWidth)]
        [ValidateInput(nameof(StringNotEmpty), "Must have a name", InfoMessageType.Error)]
        [OnValueChanged(nameof(OnNameChanged))]
        [Delayed]
        private new string name;

        [BoxGroup(baseTag)]
        [SerializeField]
        [ShowInInspector]
        [LabelWidth(labelWidth)]
        [DisplayAsString]
        protected Guid id;

        public Guid Id => id;
        public string Name => name;

        #region Unity

        protected void Awake()
        {
            //ValidateId();

            OnAwake();
        }

        protected virtual void OnAwake()
        {

        }

        protected void OnEnable()
        {
            ValidateId();

            OnOnEnable();
        }

        protected virtual void OnOnEnable()
        {

        }

        #endregion

        #region Odin

        protected bool StringNotEmpty(string s) => !string.IsNullOrEmpty(s);

        private void OnNameChanged(string name)
        {
            string prevName = System.IO.Path.GetFileNameWithoutExtension(AssetDatabase.GetAssetPath(GetInstanceID()));

            string path = AssetDatabase.GetAssetPath(GetInstanceID());

            string error = AssetDatabase.RenameAsset(path, name);

            if (!string.IsNullOrEmpty(error))
            {
                this.name = prevName;

                Debug.LogError(error);
            }

            AssetDatabase.Refresh();
        }

        #endregion

        #region Methods

        [Button("New Id")]
        [BoxGroup(baseTag)]
        private void ChangeId()
        {
            Guid prevId = Id;

            if (EditorUtility.DisplayDialog("Are you sure?", "Changing the Id of this asset could mess things up", "Okay", "Cancel"))
            {
                id = Guid.NewGuid();
                Debug.Log($"Changed id of {Name} from {prevId} to {id}");
            }
        }

        private void OnIdChanged(Guid id)
        {

        }

        private void ValidateId()
        {
            // load all ids objects
            int idCount = new AssetLoader<NameAndIdScriptableObject>("").Load().Count(bso => bso.Id == Id);

            // more than one so generate a new one
            if (idCount > 1)
            {
                Guid prevId = id;
                id = Guid.NewGuid();
                Debug.Log($"Guid of {id} generated for {Name} of type {GetType()}, as there were {idCount} uses of {prevId}");
                return;
            }
            else if (id != Guid.Empty)
            {
                // id exists once and is non-empty do nothing
                return;
            }

            id = Guid.NewGuid();
            Debug.Log($"Guid of {id} generated for new {GetType()}");
        }

        #endregion
    }
}