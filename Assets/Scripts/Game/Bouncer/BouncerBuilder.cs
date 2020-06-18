using BalsamicBits.BouncyTrash.Game.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace BalsamicBits.BouncyTrash.Game.Bouncer
{
    internal static class BouncerBuilder
    {
        public static Bouncer BuildBouncerGameObject(BouncerData data, Transform container)
        {
            GameObject bouncerContainer = new GameObject("BouncerContainer");

            GameObject model = UnityEngine.Object.Instantiate(data.ModelPrefab, bouncerContainer.transform);

            model.GetComponentInChildren<Offsetor>().Set(data.Offset);

            return bouncerContainer.AddComponent<Bouncer>();
        }
    }
}
