using System;
using DashNDine.MiscSystem;
using DashNDine.ScriptableObjectSystem;
using UnityEngine;

namespace DashNDine.CoreSystem
{
    public class ReputationManager : SingletonBehaviour<ReputationManager>
    {
        public Action<int> OnReputationUpdateAction;

        [SerializeField] private PlayerSO _playerSO;
        private QuestManager _questManager;

        private void Start()
        {
            _playerSO.ClearReputationAmount();

            OnReputationUpdateAction?.Invoke(_playerSO.ReputationAmount);

            _questManager = QuestManager.Instance;

            _questManager.OnCompleteQuestAction
                += QuestManager_OnCompleteQuestAction;
        }

        private void OnDestroy()
        {
            if (_questManager == null)
                return;

            _questManager.OnCompleteQuestAction
                -= QuestManager_OnCompleteQuestAction;
        }

        private void QuestManager_OnCompleteQuestAction(int reputationAmount)
            => AddReputation(reputationAmount);

        private void AddReputation(int reputationAmount)
        {
            _playerSO.ReputationAmount += reputationAmount;
            OnReputationUpdateAction?.Invoke(_playerSO.ReputationAmount);
        }
    }
}