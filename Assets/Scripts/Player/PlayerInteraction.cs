using System;
using DashNDine.GameInputSystem;
using UnityEngine;

namespace DashNDine.PlayerSystem
{
    public class PlayerInteraction : MonoBehaviour
    {
        public Action<Vector2> OnPlayerMoveAction;

        private PlayerInputManager _playerInputs;

        private void Start()
        {
            _playerInputs = PlayerInputManager.Instance;

            _playerInputs.OnInteractPerformedAction
                += PlayerInputs_OnInteractPerformedAction;
            _playerInputs.OnMovePerformedAction
                += PlayerInputs_OnMovePerformedAction;
        }

        private void OnDestroy()
        {
            if (_playerInputs == null)
                return;

            _playerInputs.OnInteractPerformedAction
                -= PlayerInputs_OnInteractPerformedAction;
            _playerInputs.OnMovePerformedAction
                -= PlayerInputs_OnMovePerformedAction;
        }

        private void PlayerInputs_OnInteractPerformedAction()
        {
            Debug.Log("Player Interacted");
        }

        private void PlayerInputs_OnMovePerformedAction(Vector2 direction)
            => OnPlayerMoveAction?.Invoke(direction);
    }
}