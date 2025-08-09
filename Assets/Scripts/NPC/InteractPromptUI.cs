using System;
using UnityEngine;

namespace DashNDine.NPCSystem
{
    public class InteractPromptUI : MonoBehaviour
    {
        [SerializeField] private NPCInteraction _npcInteraction;
        private bool _isInteracted = false;

        private void Awake()
        {
            _npcInteraction.OnLookAtAction
                += NPCInteraction_OnLookAtAction;
            _npcInteraction.OnLookAwayAction
                += NPCInteraction_OnLookAwayAction;
            _npcInteraction.OnInteractAction
                += NPCInteraction_OnInteractAction;

            SetVisibility(false);
        }

        private void OnDestroy()
        {
            if (_npcInteraction == null)
                return;
                
            _npcInteraction.OnLookAtAction
                -= NPCInteraction_OnLookAtAction;
            _npcInteraction.OnLookAwayAction
                -= NPCInteraction_OnLookAwayAction;
            _npcInteraction.OnInteractAction
                -= NPCInteraction_OnInteractAction;
        }

        private void NPCInteraction_OnInteractAction(Vector3 playerPosition)
        {
            SetVisibility(false);
            _isInteracted = true;
        }

        private void NPCInteraction_OnLookAtAction()
            => OnPlayerDetection(true);

        private void NPCInteraction_OnLookAwayAction()
            => OnPlayerDetection(false);


        private void OnPlayerDetection(bool isDetected)
        {
            if (_isInteracted)
                return;

            SetVisibility(isDetected);
        }

        private void SetVisibility(bool isVisible)
            => gameObject.SetActive(isVisible);
    }
}