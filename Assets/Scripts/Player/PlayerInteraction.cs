using System;
using DashNDine.CoreSystem;
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
        private BaseInteraction _baseInteraction;

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
            if (_baseInteraction == null)
                return;

            switch (_baseInteraction)
            {
                case NPCInteraction npcInteraction:
                    npcInteraction.Interact(transform.position);
                    OnInteractAction?.Invoke();
                    break;
                default:
                    _baseInteraction.Interact();
                    break;
            }
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
                int layer = raycastHit2D.collider.gameObject.layer;
                LayerMask mask = LayerMask.GetMask("Ingredient", "NPC");

                if ((mask & (1 << layer)) != 0)
                {
                    if (raycastHit2D.transform.TryGetComponent(out BaseInteraction baseInteraction))
                    {
                        _baseInteraction = baseInteraction;
                        baseInteraction.OnLookedAt();
                    }
                }
                else
                {
                    if (_baseInteraction != null)
                    {
                        _baseInteraction.OnLookedAway();
                        _baseInteraction = null;
                    }
                }
            }
            else
            {
                if (_baseInteraction != null)
                {
                    _baseInteraction.OnLookedAway();
                    _baseInteraction = null;
                }
            }
        }
    }
}