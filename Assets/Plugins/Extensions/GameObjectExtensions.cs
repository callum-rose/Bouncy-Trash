using UnityEngine;

namespace BalsamicBits.BouncyTrash.Extensions
{
    public static class GameObjectExtensions
    {

        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();
            if (component == null)
                component = gameObject.AddComponent<T>();
            return component;
        }

        public static T FindComponentInChildrenRecursively<T>(this GameObject gameObject) where T : Component
        {
            foreach (Transform child in gameObject.transform)
            {
                var tempComp = child.gameObject.GetComponent<T>();
                if (tempComp != null)
                    return tempComp;

                tempComp = child.gameObject.FindComponentInChildrenRecursively<T>();
                if (tempComp != null)
                    return tempComp;
            }

            return null;
        }
    }
}