using System;
using UnityEngine;

namespace DashNDine.CoreSystem
{
    public class BaseInteraction : MonoBehaviour
    {
        public Action OnLookAtAction;
        public Action OnLookAwayAction;
        public Action OnInteractAction;

        protected bool _isInteracted = false;

        public void OnLookedAt()
        {
            if (_isInteracted)
                return;

            OnLookAtAction?.Invoke();
        }

        public void OnLookedAway()
        {
            if (_isInteracted)
                return;

            OnLookAwayAction?.Invoke();
        }

        public virtual void Interact()
        {
            if (_isInteracted)
                return;

            SetIsInteracted(true);
            OnInteractAction?.Invoke();
        }

        protected void SetIsInteracted(bool isInteracted)
            => _isInteracted = isInteracted;
    }
}