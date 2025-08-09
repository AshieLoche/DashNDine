using DashNDine.CoreSystem;
using UnityEngine;

namespace DashNDine.NPCSystem
{
    public class InteractPromptUI : MonoBehaviour
    {
        [SerializeField] private BaseInteraction _baseInteraction;

        private void Awake()
        {
            _baseInteraction.OnLookAtAction
                += BaseInteraction_OnLookAtAction;
            _baseInteraction.OnLookAwayAction
                += BaseInteraction_OnLookAwayAction;
            _baseInteraction.OnInteractAction
                += BaseInteraction_OnInteractAction;

            SetVisibility(false);
        }

        private void OnDestroy()
        {
            if (_baseInteraction == null)
                return;
                
            _baseInteraction.OnLookAtAction
                -= BaseInteraction_OnLookAtAction;
            _baseInteraction.OnLookAwayAction
                -= BaseInteraction_OnLookAwayAction;
            _baseInteraction.OnInteractAction
                -= BaseInteraction_OnInteractAction;
        }

        private void BaseInteraction_OnInteractAction()
            => SetVisibility(false);

        private void BaseInteraction_OnLookAtAction()
            => OnPlayerDetection(true);

        private void BaseInteraction_OnLookAwayAction()
            => OnPlayerDetection(false);

        private void OnPlayerDetection(bool isDetected)
            => SetVisibility(isDetected);

        private void SetVisibility(bool isVisible)
            => gameObject.SetActive(isVisible);
    }
}