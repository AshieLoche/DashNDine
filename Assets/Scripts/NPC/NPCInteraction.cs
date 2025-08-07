using System;
using UnityEngine;

namespace DashNDine.NPCSystem
{
    public class NPCInteraction : MonoBehaviour
    {
        public Action OnPlayerEnterAction;
        public Action OnPlayerExitAction;

        public void DetectPlayerEnter()
            => OnPlayerEnterAction?.Invoke();

        public void DetectPlayerExit()
            => OnPlayerExitAction?.Invoke();

        public void Interaction()
        {
            Debug.Log($"Player interacted with {name}");
        }
    }
}