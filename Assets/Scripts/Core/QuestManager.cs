using System;
using DashNDine.ClassSystem;
using DashNDine.MiscSystem;
using DashNDine.ScriptableObjectSystem;
using UnityEngine;

namespace DashNDine.CoreSystem
{
    public class QuestManager : SingletonBehaviour<QuestManager>
    {
        public Action<IngredientStackListSO> OnCollectIngredientAction;
        public Action<int> OnCompleteQuestAction;

        [SerializeField] private QuestListSO _questListSO;
        [SerializeField] private IngredientStackListSO _inventorySO;
        private QuestListSO _chosenQuestListSO;

        protected override void Awake()
        {
            base.Awake();

            _questListSO.LockAll();
            _inventorySO.ClearAmount();
            _chosenQuestListSO = ScriptableObject.CreateInstance<QuestListSO>();
        }

        public void AddQuest(QuestSO questSO)
            => _chosenQuestListSO.AddQuestSO(questSO);

        public void CollectIngredient(IngredientSO ingredientSO)
        {
            _inventorySO.CollectIngredient(ingredientSO);
            OnCollectIngredientAction?.Invoke(_inventorySO);
        }

        public void CompleteQuest(QuestSO questSO)
        {
            _chosenQuestListSO.RemoveQuestSO(questSO);
            foreach (IngredientStackClass ingredientStack in questSO.GetIngredientStackClassList())
            {
                _inventorySO.UseIngredients(ingredientStack);
            }
            questSO.Complete();
            int reputationAmount = questSO.Reward + (questSO.HasSuccessfullyCooked ? questSO.BonusReward : 0);
            OnCompleteQuestAction?.Invoke(reputationAmount);
        }
    }
}