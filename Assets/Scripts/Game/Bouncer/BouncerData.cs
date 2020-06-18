using UnityEngine;
using Sirenix.OdinInspector;
using System;
using UnityEditor;
using System.IO;
using BalsamicBits.BouncyTrash.Core;

namespace BalsamicBits.BouncyTrash.Game.Bouncer
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/" + nameof(BouncerData), order = 1)]
    public class BouncerData : BaseModelScriptableObject
    {
        private const string generalTag = "General";

        [SerializeField]
        [BoxGroup(generalTag)]
        private bool isTheDefaultBouncer = false;

        private const string shopTag = "Shop";

        [SerializeField]
        [LabelWidth(labelWidth)]
        [TextArea]
        [BoxGroup(shopTag)]
        [ValidateInput(nameof(StringNotEmpty), "Must have a description", InfoMessageType.Error)]
        private string description;

        [SerializeField]
        [LabelWidth(labelWidth)]
        [BoxGroup(shopTag)]
        [MinValue(0)]
        [SuffixLabel("Coins")]
        private int cost;


        public string Description => description;
        public int Cost => cost;
        public bool IsTheDefaultBouncer => isTheDefaultBouncer;
    }
}