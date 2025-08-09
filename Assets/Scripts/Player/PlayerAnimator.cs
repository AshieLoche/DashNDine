using System;
using UnityEngine;

namespace DashNDine.PlayerSystem
{
    public class PlayerAnimator : MonoBehaviour
    {
        private const string IDLE_TRIGGER = "Idle";
        private const string WALK_TRIGGER = "Walk";

        [SerializeField] private PlayerMovement _playerMovement;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _animationSpeed;

        private void Awake()
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();

            _playerMovement.OnPlayerIdleAction
                += PlayerMovement_OnPlayerIdleAction;
            _playerMovement.OnPlayerMoveAction
                += PlayerMovement_OnPlayerMoveAction;
        }

        private void OnDestroy()
        {
            if (_playerMovement == null)
                return;

            _playerMovement.OnPlayerIdleAction
                -= PlayerMovement_OnPlayerIdleAction;
            _playerMovement.OnPlayerMoveAction
                -= PlayerMovement_OnPlayerMoveAction;
        }

        private void PlayerMovement_OnPlayerMoveAction()
            => TriggerAnimation(WALK_TRIGGER);

        private void PlayerMovement_OnPlayerIdleAction()
            => TriggerAnimation(IDLE_TRIGGER);

        private void TriggerAnimation(string trigger)
        {
            _animator.speed = _animationSpeed;
            ResetAllTriggerAnimation();
            _animator.SetTrigger(trigger);
        }

        private void ResetTriggerAnimation(string trigger)
            => _animator.ResetTrigger(trigger);

        private void ResetAllTriggerAnimation()
        {
            ResetTriggerAnimation(IDLE_TRIGGER);
            ResetTriggerAnimation(WALK_TRIGGER);
        }
    }
}