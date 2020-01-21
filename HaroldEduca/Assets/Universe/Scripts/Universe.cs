using UnityEngine;

using System;
using System.Collections.Generic;
using System.Reflection;

namespace Universe
{
    /// <summary>
    /// Entry point of all game-wide serialized data.
    /// The Universe is a self-regulating script.
    /// When awaken, it loads all the possible game Managers.
    /// </summary>
    [AddComponentMenu("")]
    public sealed class Universe : MonoBehaviour
    {
        public const string PATH = "Universe/";

        private static List<IManager> managers = new List<IManager>();

        public static IManager[] Managers
        {
            get { return managers.ToArray(); }
        }

        private static Universe instance;

        public static Universe Instance
        {
            get { return instance; }
        }

        private void Start()
        {
            if (instance != null) // Frankly, this should not happen. Someone made an error otherwise.
            {
                Destroy(gameObject);
                return;
            }
            else
                instance = this;

            Deserialize(this);
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Used on editor only, to load a manager.
        /// </summary>
        public static void EditorLoad(Type type)
        {
            if (Application.isPlaying || !typeof(IManager).IsAssignableFrom(type))
                return;

            GameObject go = Resources.Load(PATH + type.Name) as GameObject;
            go = Instantiate(go) as GameObject;
            IManager manager = go.GetComponent(type) as IManager;
            manager.Deserialize();
            go.hideFlags = HideFlags.HideAndDontSave;
        }

        /// <summary>
        /// Retrieve all the Manager and their data.
        /// If data is inexistant, create a new one.
        /// </summary>
        private static void Deserialize(Universe universe)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (typeof(IManager).IsAssignableFrom(type) && !type.IsAbstract)
                    {
                        GameObject go = Resources.Load(PATH + type.Name) as GameObject;
                        if (go != null)
                        {
                            GameObject clone = Instantiate(go) as GameObject;
                            clone.name = type.Name;
                            clone.transform.parent = Instance.gameObject.transform;

                            IManager manager = clone.GetComponent(type) as IManager;
                            if (manager != null)
                            {
                                RemoveExisting(type);
                                manager.Deserialize();
                                managers.Add(manager);
                            }
                        }
                    }
                }
            }
        }

        private static void RemoveExisting(Type type)
        {
            for (int i = 0; i < managers.Count; i++)
                if (managers[i].GetType() == type)
                    managers.RemoveAt(i);
        }
    }
}