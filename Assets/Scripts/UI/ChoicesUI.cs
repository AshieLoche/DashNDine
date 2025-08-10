using System;
using DashNDine.CoreSystem;
using DashNDine.EnumSystem;
using DashNDine.IngredientSystem;
using DashNDine.MiscSystem;
using DashNDine.ScriptableObjectSystem;
using UnityEngine;

namespace DashNDine.UISystem
{
    public class ChoicesUI : SingletonBehaviour<ChoicesUI>
    {
        // Actions
        public Action OnAcceptAction;
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

        private void QuestManager_OnCollectIngredientAction(IngredientStackListSO inventorySO)
        {
            bool _isComplete = _questSO.CheckInventory(inventorySO);
            OnUpdateAction?.Invoke(_isComplete);
        }

        public void SetChoices(QuestSO questSO)
        {
            _questSO = questSO;

            ActionType actionType = ActionType.None;

            switch (questSO.GetStatus())
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
            _questSO.SetStatus(QuestStatus.Waiting);
            IngredientSpawner.Instance.SetIngredientSpawner(_questSO);
            QuestManager.Instance.AddQuest(_questSO);
            OnAcceptAction?.Invoke();
        }

        public void OnLeave()
            => OnLeaveAction?.Invoke();

        public void OnGive()
        {
            _questManager.CompleteQuest(_questSO);
            OnGiveAction?.Invoke();
        }

        public void OnCook()
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDataManager>().CookFood(_questSO);
            OnCookAction?.Invoke();
        }
    }
}