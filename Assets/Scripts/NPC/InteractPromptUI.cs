using System;
using UnityEngine;

namespace DashNDine.NPCSystem
{
    public class InteractPromptUI : MonoBehaviour
    {
        [SerializeField] private NPCInteraction _npcInteraction;

        private void Awake()
        {
            _npcInteraction.OnPlayerEnterAction
                += TownNPCInteraction_OnPlayerEnterAction;
            _npcInteraction.OnPlayerExitAction
                += TownNPCInteraction_OnPlayerExitAction;

            SetVisibility(false);
        }

        private void OnDestroy()
        {
            if (_npcInteraction == null)
                return;
                
            _npcInteraction.OnPlayerEnterAction
                -= TownNPCInteraction_OnPlayerEnterAction;
            _npcInteraction.OnPlayerExitAction
                -= TownNPCInteraction_OnPlayerExitAction;
        }

        private void TownNPCInteraction_OnPlayerEnterAction()
            => SetVisibility(true);
        
        private void TownNPCInteraction_OnPlayerExitAction()
            => SetVisibility(false);

        private void SetVisibility(bool isVisible)
            => gameObject.SetActive(isVisible);
    }
}