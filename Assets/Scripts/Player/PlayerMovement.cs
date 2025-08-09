using System;
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
        public bool _canMove = true;

        private void Awake()
        {
            _playerInput.OnPlayerMoveAction
                += PlayerInteraction_OnPlayerMoveAction;
            _playerInteraction.OnInteractAction
                += PlayerInteraction_OnPlayerInteractAction;
        }

        private void OnDestroy()
        {
            if (_playerInput != null)
                _playerInput.OnPlayerMoveAction
                    -= PlayerInteraction_OnPlayerMoveAction;

            if (_playerInteraction != null)
                _playerInteraction.OnInteractAction
                    -= PlayerInteraction_OnPlayerInteractAction;
        }

        private void PlayerInteraction_OnPlayerInteractAction()
            => _canMove = false;

        private void PlayerInteraction_OnPlayerMoveAction(Vector2 moveDir)
            => _moveDir = moveDir;

        private void Update()
        {
            if (!_canMove)
                return;

            HandleCover();
            HandleMovement();
        }

        private void HandleCover()
        {
            // If colliding then IsBehind, else then  IsInFront
            bool isInFront = !IsColliding(_originOffsetDepth, _boxSizeDepth, _moveDir);

            if (isInFront)
                OnPlayerInFrontAction?.Invoke();
            else
                OnPlayerBehindAction?.Invoke();
        }

        private void HandleMovement()
        {
            bool canMove = !IsColliding(_originOffsetMove, _boxSizeMove, _moveDir);

            if (!canMove)
            {
                // Cannot Move towards moveDir

                //Attemp only X movement
                Vector2 moveDirX = new Vector2(_moveDir.x, 0f).normalized;

                canMove = !IsColliding(_originOffsetMove, _boxSizeMove, moveDirX);

                if (canMove)
                    // Can move only on the x
                    _moveDir = moveDirX;
                else
                {
                    // Cannot move only on the X

                    // Attempt only Z movement
                    Vector2 moveDirY = new Vector2(0f, _moveDir.y).normalized;

                    canMove = !IsColliding(_originOffsetMove, _boxSizeMove, moveDirY);

                    if (canMove)
                        // Can move only on the Z
                        _moveDir = moveDirY;
                    else
                    {
                        // Cannot move in any direction
                    }
                }
            }

            if (canMove)
                transform.position += (Vector3)(GetMoveDistance() * _moveDir);

            OnPlayerRotateAction?.Invoke(_moveDir.x);

            if (_moveDir == Vector2.zero)
                OnPlayerIdleAction?.Invoke();
            else
                OnPlayerMoveAction?.Invoke();
        }

        private float GetMoveDistance()
            => _moveSpeed * Time.deltaTime;

        private bool IsColliding(Vector3 originOffset, Vector2 boxSize, Vector2 moveDir)
        {
            Vector2 origin = transform.position + originOffset;
            float angle = 0f;

            bool isColliding = Physics2D.BoxCast(
                origin,
                boxSize,
                angle,
                moveDir,
                GetMoveDistance(),
                _layerMask
            );

            return isColliding;
        }
    }
}