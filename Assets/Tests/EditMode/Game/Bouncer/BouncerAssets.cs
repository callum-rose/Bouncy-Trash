using System.Collections;
using System.Collections.Generic;
using BalsamicBits.BouncyTrash.Core;
using BalsamicBits.BouncyTrash.Game.Bouncer;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Linq;
using System;
using BalsamicBits.BouncyTrash.Game.Core;
using UnityEditor;
using System.Text;
using BalsamicBits.BouncyTrash.Game.Projectile;

namespace Tests.Game.Bouncer
{
    public class BouncerAssets
    {
        [Test]
        public void ThereIsOnlyOneBouncerDataAssetWithIsDefaultBouncerFlag()
        {
            IEnumerable<BouncerData> bouncerData = Resources.LoadAll<BouncerData>(ResourcePaths.Bouncers);

            StringBuilder sb = new StringBuilder($"File paths with {nameof(BouncerData.IsTheDefaultBouncer)} set as true:");
            sb.AppendLine();

            int count = 0;
            foreach (var data in bouncerData)
            {
                if (data.IsTheDefaultBouncer)
                {
                    count++;
                    sb.Append(AssetDatabase.GetAssetPath(data.GetInstanceID()));
                    sb.AppendLine();
                }
            }

            string message;
            if (count == 0)
            {
                message = $"There are no assets with {nameof(BouncerData.IsTheDefaultBouncer)} set as true";
            }
            else
            {
                message = sb.ToString();
            }

            Assert.AreEqual(1, count, message);
        }

        [Test]
        public void AllScriptableObjectBouncerPrefabsContainOneIAnimatable()
        {
            IEnumerable<BouncerData> bouncerData = Resources.LoadAll<BouncerData>(ResourcePaths.Bouncers);
            var modelPrefabs = bouncerData.Select(d => d.ModelPrefab);

            foreach (var prefab in modelPrefabs)
            {
                var animatables = prefab.GetComponentsInChildren<IAnimatable>();

                Assert.AreEqual(1, animatables.Length, $"Prefab {prefab.name} has {animatables.Length} {nameof(IAnimatable)}s");
            }
        }

        [Test]
        public void AllScriptableObjectBouncerPrefabsContainOneOffsetorComponent()
        {
            IEnumerable<BouncerData> bouncerData = Resources.LoadAll<BouncerData>(ResourcePaths.Bouncers);
            var modelPrefabs = bouncerData.Select(d => d.ModelPrefab);

            foreach (var prefab in modelPrefabs)
            {
                var offsetors = prefab.GetComponentsInChildren<Offsetor>();

                Assert.AreEqual(1, offsetors.Length, $"Prefab {prefab.name} has {offsetors.Length} {nameof(Offsetor)}s");
            }
        }

        [Test]
        public void AllScriptableObjectBouncerPrefabsDontHaveBouncerComponent()
        {
            IEnumerable<BouncerData> bouncerData = Resources.LoadAll<BouncerData>(ResourcePaths.Bouncers);
            var modelPrefabs = bouncerData.Select(d => d.ModelPrefab);

            foreach (var prefab in modelPrefabs)
            {
                var bouncers = prefab.GetComponentsInChildren<IBouncer>();

                Assert.AreEqual(0, bouncers.Length, $"Prefab {prefab.name} has {bouncers.Length} {nameof(IBouncer)}s");
            }
        }

        [Test]
        public void BouncerScriptableObjectsAllHaveUniqueGuid()
        {
            IEnumerable<BouncerData> bouncerData = Resources.LoadAll<BouncerData>(ResourcePaths.Bouncers);

            foreach (var data in bouncerData)
            {
                int count = bouncerData.Count(d => d.Id == data.Id);

                Assert.AreEqual(1, count, $"Id {data.Id} exists in {count} files");
            }
        }
    }
}
