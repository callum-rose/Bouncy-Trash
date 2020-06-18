using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BalsamicBits.BouncyTrash.Core;
using BalsamicBits.BouncyTrash.Game.Core;
using BalsamicBits.BouncyTrash.Game.Projectile;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.Game.Projectile
{
    public class ProjectilePrefabs
    {
        [Test]
        public void ProjectileScriptableObjectModelPrefabDoesNotHaveProjectileComponent()
        {
            IEnumerable<ProjectileData> projectileData = Resources.LoadAll<ProjectileData>(ResourcePaths.Projectiles);
            var modelPrefabs = projectileData.Select(d => d.ModelPrefab);

            foreach (var prefab in modelPrefabs)
            {
                var projectiles = prefab.GetComponentsInChildren<BalsamicBits.BouncyTrash.Game.Projectile.Projectile>();

                Assert.AreEqual(0, projectiles.Length, $"Prefab {prefab.name} has {projectiles.Length} {nameof(Projectile)}s");
            }
        }

        [Test]
        public void ProjectileScriptableObjectModelPrefabHasOneIAnimatableComponent()
        {
            IEnumerable<ProjectileData> projectileData = Resources.LoadAll<ProjectileData>(ResourcePaths.Projectiles);
            var modelPrefabs = projectileData.Select(d => d.ModelPrefab);

            foreach (var prefab in modelPrefabs)
            {
                var animatables = prefab.GetComponentsInChildren<IAnimatable>();

                Assert.AreEqual(1, animatables.Length, $"Prefab {prefab.name} has {animatables.Length} {nameof(IAnimatable)}s");
            }
        }

        [Test]
        public void ProjectileScriptableObjectModelPrefabHaveOffsetorComponent()
        {
            IEnumerable<ProjectileData> projectileData = Resources.LoadAll<ProjectileData>(ResourcePaths.Projectiles);
            var modelPrefabs = projectileData.Select(d => d.ModelPrefab);

            foreach (var prefab in modelPrefabs)
            {
                var offsetors = prefab.GetComponentsInChildren<Offsetor>();

                Assert.AreEqual(1, offsetors.Length, $"Prefab {prefab.name} has {offsetors.Length} {nameof(Offsetor)}s");
            }
        }

        [Test]
        public void ProjectileThemeScriptableObjectPathIsValid()
        {
            IEnumerable<ProjectileThemeData> themeData = Resources.LoadAll<ProjectileThemeData>(ResourcePaths.Projectiles);

            foreach (ProjectileThemeData data in themeData)
            {
                string fullPath = Path.Combine(Application.dataPath, "Resources", data.ResourcePath);

                bool exists = Directory.Exists(fullPath);

                Assert.True(exists, $"Directory {fullPath} does not exist");
            }
        }

        [Test]
        public void ProjectileScriptableObjectsAllHaveUniqueGuid()
        {
            IEnumerable<ProjectileData> projectileData = Resources.LoadAll<ProjectileData>(ResourcePaths.Projectiles);

            foreach (var data in projectileData)
            {
                int count = projectileData.Count(d => d.Id == data.Id);

                Assert.AreEqual(1, count, $"Id {data.Id} exists in {count} files");
            }
        }
    }
}
