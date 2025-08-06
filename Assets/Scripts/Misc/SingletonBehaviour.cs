using UnityEngine;

namespace DashNDine.MiscSystem
{
    public class SingletonBehaviour<T> : MonoBehaviour where T : Component
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogError($"An instance of {typeof(T).Name} already exists in {Instance.gameObject.name}. The new instance is in {gameObject.name}.");
                return;
            }

            Instance = this as T;
        }
    }
}