using UnityEngine;

namespace DashNDine.PlayerSystem
{
    public class PlayerVisual : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();

            _playerMovement.OnPlayerRotateAction
                += PlayerMovement_OnPlayerRotateAction;
            _playerMovement.OnPlayerInFrontAction
                += PlayerMovement_OnPlayerInFrontAction;
            _playerMovement.OnPlayerBehindAction
                += PlayerMovement_OnPlayerBehindAction;
        }

        private void OnDestroy()
        {
            if (_playerMovement == null)
                return;

            _playerMovement.OnPlayerRotateAction
                -= PlayerMovement_OnPlayerRotateAction;
            _playerMovement.OnPlayerInFrontAction
                -= PlayerMovement_OnPlayerInFrontAction;
            _playerMovement.OnPlayerBehindAction
                -= PlayerMovement_OnPlayerBehindAction;
        }

        private void PlayerMovement_OnPlayerInFrontAction()
        {
            int inFrontSortingOrder = 2;
            UpdateSortingOrder(inFrontSortingOrder);
        }

        private void PlayerMovement_OnPlayerBehindAction()
        {
            int behindSortingOrder = 0;
            UpdateSortingOrder(behindSortingOrder);
        }

        private void UpdateSortingOrder(int sortingOrder)
            => _spriteRenderer.sortingOrder = sortingOrder;

        private void PlayerMovement_OnPlayerRotateAction(float moveDirX)
        {
            if (moveDirX == 0)
                return;

            if (moveDirX > 0)
                _spriteRenderer.flipX = false;
            else if (moveDirX < 0)
                _spriteRenderer.flipX = true;
        }
    }
}