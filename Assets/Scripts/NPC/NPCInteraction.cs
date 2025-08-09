using System;
using DashNDine.UISystem;
using UnityEngine;

namespace DashNDine.NPCSystem
{
    public class NPCInteraction : MonoBehaviour
    {
        public Action OnLookAtAction;
        public Action OnLookAwayAction;
        public Action<Vector3> OnInteractAction;

        [SerializeField] private NPC _npc;

        public void OnLookedAt()
            => OnLookAtAction?.Invoke();

        public void OnLookedAway()
            => OnLookAwayAction?.Invoke();

        public void Interact(Vector3 playerPosition)
        {
            DialogueUI.Instance.SetDialogueByQuestSO(_npc.GetQuestSO());
            OnInteractAction?.Invoke(playerPosition);
        }
    }
}