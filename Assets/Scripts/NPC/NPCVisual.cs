using System;
using UnityEngine;

namespace DashNDine.NPCSystem
{
    public class NPCVisual : MonoBehaviour
    {
        [SerializeField] private NPCInteraction _npcInteraction;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();

            _npcInteraction.OnInteractWithParamAction
                += NPCInteraction_OnInteractWithParamAction;
        }

        private void OnDestroy()
        {
            if (_npcInteraction == null)
                return;

            _npcInteraction.OnInteractWithParamAction
                -= NPCInteraction_OnInteractWithParamAction;
        }

        private void NPCInteraction_OnInteractWithParamAction(Vector3 playerPosition)
        {
            float dir = Vector2.Dot(transform.right, playerPosition - transform.position);

            _spriteRenderer.flipX = dir < 0;
        }
    }
}