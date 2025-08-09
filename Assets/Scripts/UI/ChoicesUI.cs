using System;
using DashNDine.CoreSystem;
using DashNDine.EnumSystem;
using DashNDine.MiscSystem;
using DashNDine.ScriptableObjectSystem;

namespace DashNDine.UISystem
{
    public class ChoicesUI : SingletonBehaviour<ChoicesUI>
    {
        // Actions
        public Action<QuestSO> OnAcceptAction;
        public Action OnLeaveAction;
        public Action OnGiveAction;
        public Action OnCookAction;

        // UIs
        public Action<ActionType> OnSetAction;
        public Action<bool> OnUpdateAction;
        public Action OnResetAction;

        private QuestSO _questSO;
        private QuestManager _questManager;

        private void Start()
        {
            _questManager = QuestManager.Instance;

            _questManager.OnCollectIngredientAction
                += QuestManager_OnCollectIngredientAction;
        }

        private void OnDestroy()
        {
            if (_questManager == null)
                return;
            
            _questManager.OnCollectIngredientAction
                -= QuestManager_OnCollectIngredientAction;
        }

        private void QuestManager_OnCollectIngredientAction()
        {
            bool _isComplete = _questSO.CompareAmount();
            OnUpdateAction?.Invoke(_isComplete);
        }

        public void SetChoices(QuestSO questSO)
        {
            _questSO = questSO;

            ActionType actionType = ActionType.None;

            switch (questSO.QuestStatus)
            {
                case QuestStatus.Locked:
                    break;
                case QuestStatus.Unlocked:
                    actionType |= ActionType.Accept;
                    actionType |= ActionType.Leave;
                    break;
                case QuestStatus.Waiting: // TODO: FIX ONCE OBJECTIVE TRACKING IS IMPLEMENTED
                    switch (questSO.QuestType)
                    {
                        case QuestType.Return:
                            actionType |= ActionType.Give;
                            actionType |= ActionType.Leave;
                            break;
                        case QuestType.Cook:
                            actionType |= ActionType.Cook;
                            actionType |= ActionType.Leave;
                            break;
                        case QuestType.Choose:
                        default:
                            actionType |= ActionType.Cook;
                            actionType |= ActionType.Give;
                            actionType |= ActionType.Leave;
                            break;
                    }
                    break;
                case QuestStatus.Success:
                case QuestStatus.Failure:
                    actionType |= ActionType.Leave;
                    break;
            }

            OnSetAction?.Invoke(actionType);
        }

        public void ResetChoices()
            => OnResetAction?.Invoke();

        public void OnAccept()
        {
            _questSO.QuestStatus = QuestStatus.Waiting;
            QuestManager.Instance.AddQuest(_questSO);
            OnAcceptAction?.Invoke(_questSO);
        }

        public void OnLeave()
            => OnLeaveAction?.Invoke();

        public void OnGive()
            => OnGiveAction?.Invoke();

        public void OnCook()
            => OnCookAction?.Invoke();
    }
}