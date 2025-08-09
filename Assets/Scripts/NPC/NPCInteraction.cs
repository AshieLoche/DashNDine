using System;
using DashNDine.CoreSystem;
using DashNDine.UISystem;
using UnityEngine;

namespace DashNDine.NPCSystem
{
    public class NPCInteraction : BaseInteraction
    {
        public Action<Vector3> OnInteractWithParamAction;

        [SerializeField] private NPC _npc;
        private ChoicesUI _choicesUI;

        private void Start()
        {
            _choicesUI = ChoicesUI.Instance;

            _choicesUI.OnAcceptAction
                += ChoicesUI_OnAcceptAction;
            _choicesUI.OnGiveAction
                += ChoicesUI_OnGiveAction;
            _choicesUI.OnLeaveAction
                += ChoicesUI_OnLeaveAction;
        }

        private void OnDestroy()
        {
            if (_choicesUI == null)
                return;

            _choicesUI.OnAcceptAction
                -= ChoicesUI_OnAcceptAction;
            _choicesUI.OnGiveAction
                -= ChoicesUI_OnGiveAction;
            _choicesUI.OnLeaveAction
                -= ChoicesUI_OnLeaveAction;
        }

        private void ChoicesUI_OnGiveAction()
            => SetIsInteracted(false);

        private void ChoicesUI_OnAcceptAction()
            => SetIsInteracted(false);

        private void ChoicesUI_OnLeaveAction()
            => SetIsInteracted(false);

        public void Interact(Vector3 playerPosition)
        {
            if (_isInteracted)
                return; 

            Interact();
            DialogueUI.Instance.SetDialogueByQuestSO(_npc.GetQuestSO());
            OnInteractWithParamAction?.Invoke(playerPosition);
        }
    }
}