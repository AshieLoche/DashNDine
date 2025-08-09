using System;
using DashNDine.NPCSystem;
using UnityEngine;

namespace DashNDine.PlayerSystem
{
    public class PlayerInteraction : MonoBehaviour
    {
        public Action OnInteractAction;

        [SerializeField] private Player _player;
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _interactDistance;
        private Vector2 _moveDir;
        private Vector2 _lastInteractDir;
        private NPCInteraction _npcInteraction;

        private void Awake()
        {
            _playerInput.OnPlayerInteractAction
                += PlayerInput_OnPlayerInteractAction;
            _playerInput.OnPlayerMoveAction
                += PlayerInput_OnPlayerMoveAction;
        }

        private void OnDestroy()
        {
            if (_playerInput == null)
                return;

            _playerInput.OnPlayerInteractAction
                -= PlayerInput_OnPlayerInteractAction;
            _playerInput.OnPlayerMoveAction
                -= PlayerInput_OnPlayerMoveAction;
        }

        private void PlayerInput_OnPlayerInteractAction()
        {
            if (_npcInteraction == null)
                return;

            _npcInteraction.Interact(transform.position);
            OnInteractAction?.Invoke();
        }

        private void PlayerInput_OnPlayerMoveAction(Vector2 moveDir)
            => _moveDir = moveDir;

        private void Update()
            => HandleInteraction();
        
        private void HandleInteraction()
        {
            if (_moveDir != Vector2.zero)
                _lastInteractDir = _moveDir;

            RaycastHit2D raycastHit2D = Physics2D.Raycast(
                transform.position,
                _lastInteractDir,
                _interactDistance,
                _layerMask
            );

            if (raycastHit2D)
            {
                if (raycastHit2D.transform.TryGetComponent(out NPCInteraction npcInteraction))
                {
                    _npcInteraction = npcInteraction;
                    npcInteraction.OnLookedAt();
                }
            }
            else
            {
                if (_npcInteraction == null)
                    return;

                _npcInteraction.OnLookedAway();
                _npcInteraction = null;
            }
        }
    }
}