using DashNDine.EnumSystem;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DashNDine.UISystem
{
    public class ChoiceButtonUI : MonoBehaviour
    {
        [SerializeField] private ChoicesUI _choicesUI;
        [SerializeField] private Button _choiceButton;
        [SerializeField] private TextMeshProUGUI _choiceButtonText;
        [SerializeField] private ActionType _actionType;
        private ActionType _completeActionType;
        private bool _isComplete = false;

        private void Awake()
        {
            _choicesUI.OnSetAction
                += ChoicesUI_OnSetAction;
            _choicesUI.OnUpdateAction
                += ChoicesUI_OnUpdateAction;
            _choicesUI.OnResetAction
                += ChoicesUI_OnResetAction;

            _choiceButton.onClick.AddListener(Click);

            SetVisibility(false);
        }

        private void OnDestroy()
        {
            if (_choicesUI == null)
                return;

            _choicesUI.OnSetAction
                -= ChoicesUI_OnSetAction;
            _choicesUI.OnUpdateAction
                -= ChoicesUI_OnUpdateAction;
            _choicesUI.OnResetAction
                -= ChoicesUI_OnResetAction;
        }

        private void ChoicesUI_OnUpdateAction(bool isComplete)
        {
            _isComplete = isComplete;
            SelectButton(_completeActionType);
        }

        private void ChoicesUI_OnSetAction(ActionType actionType)
        {
            _completeActionType = actionType;
            SelectButton(actionType);
            SetVisibility((actionType & _actionType) == _actionType);
        }

        private void SelectButton(ActionType actionType)
        {
            if (actionType == (ActionType.Accept | ActionType.Leave))
            {
                if ((_actionType & ActionType.Accept) == ActionType.Accept)
                {
                    _choiceButton.Select();
                }
            }
            else if (actionType == (ActionType.Cook | ActionType.Give | ActionType.Leave))
            {
                if (_isComplete)
                {
                    _choiceButton.interactable = true;
                    if ((_actionType & ActionType.Cook) == ActionType.Cook)
                        _choiceButton.Select();
                }
                else
                {
                    if ((_actionType & ActionType.Leave) == ActionType.Leave)
                        _choiceButton.Select();
                    else
                        _choiceButton.interactable = false;
                }
            }
            else if (actionType == (ActionType.Give | ActionType.Leave))
            {
                if (_isComplete)
                {
                    _choiceButton.interactable = true;
                    if ((_actionType & ActionType.Give) == ActionType.Give)
                        _choiceButton.Select();
                }
                else
                {
                    if ((_actionType & ActionType.Leave) == ActionType.Leave)
                        _choiceButton.Select();
                    else
                        _choiceButton.interactable = false;
                }
            }
            else if (actionType == (ActionType.Cook | ActionType.Leave))
            {
                if (_isComplete)
                {
                    _choiceButton.interactable = true;
                    if ((_actionType & ActionType.Cook) == ActionType.Cook)
                        _choiceButton.Select();
                }
                else
                {
                    if ((_actionType & ActionType.Leave) == ActionType.Leave)
                        _choiceButton.Select();
                    else
                        _choiceButton.interactable = false;
                }
            }
            else if (actionType == ActionType.Leave)
                _choiceButton.Select();
        }

        private void ChoicesUI_OnResetAction()
            => SetVisibility(false);

        private void Click()
        {
            if ((_actionType & ActionType.Accept) == ActionType.Accept)
                _choicesUI.OnAccept();
            else if ((_actionType & ActionType.Leave) == ActionType.Leave)
                _choicesUI.OnLeave();
            else if ((_actionType & ActionType.Give) == ActionType.Give)
            {
                _isComplete = false;
                _choicesUI.OnGive();
            }
            else if ((_actionType & ActionType.Cook) == ActionType.Cook)
            {
                _isComplete = false;
                _choicesUI.OnCook();
            }

            EventSystem.current.SetSelectedGameObject(null);
        }

        public void SetVisibility(bool isVisible)
            => gameObject.SetActive(isVisible);
    }
}