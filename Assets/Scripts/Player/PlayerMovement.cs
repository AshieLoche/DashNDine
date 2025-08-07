using UnityEngine;

namespace DashNDine.PlayerSystem
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private PlayerInteraction _playerInteraction;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _moveSpeed;

        private void Awake()
        {
            _playerInteraction.OnPlayerMoveAction
                += PlayerInteraction_OnPlayerMoveAction;
        }

        private void OnDestroy()
        {
            if (_playerInteraction == null)
                return;
                
            _playerInteraction.OnPlayerMoveAction
                -= PlayerInteraction_OnPlayerMoveAction;
        }

        private void PlayerInteraction_OnPlayerMoveAction(Vector2 direction)
            => _rigidbody2D.linearVelocity = _moveSpeed * direction;
    }
}