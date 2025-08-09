using DashNDine.NPCSystem;
using UnityEngine;

namespace DashNDine.PlayerSystem
{
    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _interactDistance;
        private Vector2 _moveDir;
        private Vector2 _lastInteractDir;

        // TODO: CHANGE WHEN NPC IS INTEGRATED
        private NPCInteraction _npcInteraction;

        private void Awake()
        {
            _playerInput.OnPlayerInteractAction
                += PlayerInteraction_OnPlayerInteractAction;
            _playerInput.OnPlayerMoveAction
                += PlayerInteraction_OnPlayerMoveAction;
        }

        private void OnDestroy()
        {
            if (_playerInput == null)
                return;

            _playerInput.OnPlayerInteractAction
                -= PlayerInteraction_OnPlayerInteractAction;
            _playerInput.OnPlayerMoveAction
                -= PlayerInteraction_OnPlayerMoveAction;
        }

        private void PlayerInteraction_OnPlayerInteractAction()
        {
            // TODO: CHANGE WHEN NPC IS INTEGRATED
            if (_npcInteraction == null)
                return;

            _npcInteraction.Interaction();
        }

        private void PlayerInteraction_OnPlayerMoveAction(Vector2 moveDir)
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
                // TODO: Interact with NPC
                if (raycastHit2D.transform.TryGetComponent(out NPCInteraction npcInteraction))
                {
                    _npcInteraction = npcInteraction;
                    npcInteraction.DetectPlayerEnter();
                }
            }
            else
            {
                if (_npcInteraction == null)
                    return;

                _npcInteraction.DetectPlayerExit();
                _npcInteraction = null;
            }
        }
    }
}