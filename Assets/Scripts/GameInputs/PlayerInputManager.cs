using System;
using DashNDine.EnumSystem;
using DashNDine.MiscSystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace DashNDine.GameInputSystem
{
    public class PlayerInputManager : SingletonBehaviour<PlayerInputManager>, InputSystem_Actions.IPlayerActions
    {
        public Action OnDialoguePerformedAction;
        public Action OnInteractPerformedAction;
        public Action<Vector2> OnMoveAction;
        public Action<QuickTimeEventButton> OnQuickTimeEventPerformedAction;
        public Action OnSkillPerformedAction;
        public Action<SkillSelectButton> OnSkillSelectPerformedAction;

        private InputSystem_Actions _inputSystemActions;
        private InputSystem_Actions.PlayerActions _playerActions;

        protected override void Awake()
        {
            base.Awake();

            _inputSystemActions = new InputSystem_Actions();

            _playerActions = _inputSystemActions.Player;
            _playerActions.AddCallbacks(this);
        }

        private void OnDestroy()
        {
            _playerActions.RemoveCallbacks(this);
            _inputSystemActions.Dispose();
        }

        private void OnEnable()
            => _playerActions.Enable();

        private void OnDisable()
            => _playerActions.Disable();

        // TODO: ADD ESCAPE TO UI INPUTS

        // TODO: MOVE DIALOGUE TO UI INPUTS
        public void OnDialogue(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnDialoguePerformedAction?.Invoke();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnInteractPerformedAction?.Invoke();
        }

        public void OnMove(InputAction.CallbackContext context)
            =>  OnMoveAction?.Invoke(context.ReadValue<Vector2>());

        public void OnQuickTimeEvent(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            if (context.control is KeyControl keyControl)
            {
                string stringModifierFunc(string keyControlDisplayName)
                {
                    return Utils.NumberToName(keyControlDisplayName);
                }

                InvokeActionFromKey(keyControl, stringModifierFunc, OnQuickTimeEventPerformedAction);
            }
        }

        public void OnSkill(InputAction.CallbackContext context)
        {
            if (context.performed)
                OnSkillPerformedAction?.Invoke();
        }

        public void OnSkillSelect(InputAction.CallbackContext context)
        {
            if (!context.performed)
                return;

            if (context.control is KeyControl keyControl)
            {
                string stringModifierFunc(string keyControlDisplayName)
                {
                    return keyControlDisplayName + "Arrow";
                };

                InvokeActionFromKey(keyControl, stringModifierFunc, OnSkillSelectPerformedAction);
            }
        }

        private void InvokeActionFromKey<Tenum>(KeyControl keyControl, Func<string, string> stringModifierFunc, Action<Tenum> action) where Tenum : struct, Enum
        {
            string keyControlDisplayName = keyControl.displayName;
            string enumValueName = stringModifierFunc?.Invoke(keyControlDisplayName);

            if (Utils.TryConvertStringToEnum(enumValueName, out Tenum newEnum))
                action?.Invoke(newEnum);
            else
                Debug.LogError($"String to {typeof(Tenum).Name} parsing failed in {typeof(PlayerInput).Name} at {name}");
        }
    }
}