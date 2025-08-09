using System;
using DashNDine.UISystem;
using UnityEngine;

namespace DashNDine.NPCSystem
{
    public class NPCInteraction : MonoBehaviour
    {
        public Action OnLookAtAction;
        public Action OnLookAwayAction;
        public Action OnInteractAction;

        [SerializeField] private NPC _npc;

        public void DetectPlayerEnter()
            => OnLookAtAction?.Invoke();

        public void DetectPlayerExit()
            => OnLookAwayAction?.Invoke();

        public void Interact()
        {
            DialogueUI.Instance.SetDialogueByQuestSO(_npc.GetQuestSO());
            OnInteractAction?.Invoke();
        }
    }
}