using System;
using DashNDine.CoreSystem;
using DashNDine.UISystem;
using UnityEngine;

namespace DashNDine.PlayerSystem
{
    public class PlayerMovement : MonoBehaviour
    {
        public Action<float> OnPlayerRotateAction;
        public Action OnPlayerMoveAction;
        public Action OnPlayerIdleAction;
        public Action OnPlayerInFrontAction;
        public Action OnPlayerBehindAction;
        
        [SerializeField] private PlayerInput _playerInput;
        [SerializeField] private PlayerInteraction _playerInteraction;
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private Vector3 _originOffsetMove;
        [SerializeField] private Vector2 _boxSizeMove;
        [SerializeField] private Vector3 _originOffsetDepth;
        [SerializeField] private Vector2 _boxSizeDepth;
        private Vector2 _moveDir;
        private bool _canMove = true;
        private ChoicesUI _choicesUI;
        private BaseInteraction _baseInteraction;

        private void Awake()
        {
            _playerInput.OnPlayerMoveAction
                += PlayerInteraction_OnPlayerMoveAction;
            _playerInteraction.OnInteractAction
                += PlayerInteraction_OnPlayerInteractAction;
        }

        private void Start()
        {
            _choicesUI = ChoicesUI.Instance;

            _choicesUI.OnAcceptAction
                += ChoicesUI_OnAcceptAction;
            _choicesUI.OnGiveAction
                += ChoicesUI_OnGiveAction;
            _choicesUI.OnCookAction
                += ChoicesUI_OnCookAction;
            _choicesUI.OnLeaveAction
                += ChoicesUI_OnLeaveAction;
        }

        private void OnDestroy()
        {
            if (_playerInput != null)
                _playerInput.OnPlayerMoveAction
                    -= PlayerInteraction_OnPlayerMoveAction;

            if (_playerInteraction != null)
                _playerInteraction.OnInteractAction
                    -= PlayerInteraction_OnPlayerInteractAction;

            if (_choicesUI != null)
            {
                _choicesUI.OnAcceptAction
                    -= ChoicesUI_OnAcceptAction;
                _choicesUI.OnGiveAction
                    -= ChoicesUI_OnGiveAction;
                _choicesUI.OnCookAction
                    -= ChoicesUI_OnCookAction;
                _choicesUI.OnLeaveAction
                    -= ChoicesUI_OnLeaveAction;
            }
        }
        
        private void ChoicesUI_OnCookAction()
            => SetCanMove(true);

        private void ChoicesUI_OnGiveAction()
            => SetCanMove(true);

        private void ChoicesUI_OnAcceptAction()
            => SetCanMove(true);

        private void ChoicesUI_OnLeaveAction()
            => SetCanMove(true);

        private void PlayerInteraction_OnPlayerInteractAction()
            => SetCanMove(false);

        private void PlayerInteraction_OnPlayerMoveAction(Vector2 moveDir)
            => _moveDir = moveDir;

        private void SetCanMove(bool canMove)
            => _canMove = canMove;

        private void Update()
        {
            if (!_canMove)
                return;

            // HandleCover();
            HandleMovement();
        }

        private void HandleCover()
        {
            // If colliding then IsBehind, else then  IsInFront
            bool isInFront = !GetCollision(_originOffsetDepth, _boxSizeDepth, _moveDir);

            if (isInFront)
                OnPlayerInFrontAction?.Invoke();
            else
                OnPlayerBehindAction?.Invoke();
        }

        private void HandleMovement()
        {
            Vector2 moveDir = _moveDir;

            RaycastHit2D raycastHit2D = GetCollision(_originOffsetMove, _boxSizeMove, moveDir);

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

            bool canMove = !raycastHit2D;

            if (!canMove)
            {
                // Cannot Move towards moveDir

                //Attemp only X movement
                Vector2 moveDirX = new Vector2(moveDir.x, 0f).normalized;

                canMove = !GetCollision(_originOffsetMove, _boxSizeMove, moveDirX);

                if (canMove)
                    // Can move only on the x
                    moveDir = moveDirX;
                else
                {
                    // Cannot move only on the X

                    // Attempt only Z movement
                    Vector2 moveDirY = new Vector2(0f, moveDir.y).normalized;

                    canMove = !GetCollision(_originOffsetMove, _boxSizeMove, moveDirY);

                    if (canMove)
                        // Can move only on the Z
                        moveDir = moveDirY;
                    else
                    {
                        // Cannot move in any direction
                    }
                }
            }

            if (canMove)
                transform.position += (Vector3)(GetMoveDistance() * moveDir);

            OnPlayerRotateAction?.Invoke(moveDir.x);

            if (moveDir == Vector2.zero)
                OnPlayerIdleAction?.Invoke();
            else
                OnPlayerMoveAction?.Invoke();
        }

        private float GetMoveDistance()
            => _moveSpeed * Time.deltaTime;

        private RaycastHit2D GetCollision(Vector3 originOffset, Vector2 boxSize, Vector2 moveDir)
        {
            Vector2 origin = transform.position + originOffset;
            float angle = 0f;

            return Physics2D.BoxCast(
                origin,
                boxSize,
                angle,
                moveDir,
                GetMoveDistance(),
                _layerMask
            );
        }
    }
}