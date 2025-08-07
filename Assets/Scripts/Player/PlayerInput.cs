using System;
using DashNDine.GameInputSystem;
using UnityEngine;

namespace DashNDine.PlayerSystem
{
    public class PlayerInput : MonoBehaviour
    {
        public Action OnPlayerInteractAction;
        public Action<Vector2> OnPlayerMoveAction;

        private PlayerInputManager _playerInputs;

        private void Start()
        {
            _playerInputs = PlayerInputManager.Instance;

            _playerInputs.OnInteractPerformedAction
                += PlayerInputs_OnInteractPerformedAction;
            _playerInputs.OnMoveAction
                += PlayerInputs_OnMoveAction;
        }

        private void OnDestroy()
        {
            if (_playerInputs == null)
                return;

            _playerInputs.OnInteractPerformedAction
                -= PlayerInputs_OnInteractPerformedAction;
            _playerInputs.OnMoveAction
                -= PlayerInputs_OnMoveAction;
        }

        private void PlayerInputs_OnInteractPerformedAction()
            => OnPlayerInteractAction?.Invoke();

        private void PlayerInputs_OnMoveAction(Vector2 direction)
            => OnPlayerMoveAction?.Invoke(direction);
    }
}