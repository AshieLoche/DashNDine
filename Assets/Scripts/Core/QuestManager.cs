using System;
using DashNDine.MiscSystem;
using DashNDine.ScriptableObjectSystem;
using UnityEngine;

namespace DashNDine.CoreSystem
{
    public class QuestManager : SingletonBehaviour<QuestManager>
    {
        public Action<IngredientStackListSO> OnCollectIngredientAction;

        [SerializeField] private IngredientStackListSO _inventorySO;
        private QuestListSO _questListSO;

        protected override void Awake()
        {
            base.Awake();

            _inventorySO.ClearObjectiveAmount();
            _questListSO = ScriptableObject.CreateInstance<QuestListSO>();
        }

        public void AddQuest(QuestSO questSO)
            => _questListSO.AddQuestSO(questSO);

        public void CollectIngredient(IngredientSO ingredientSO)
        {
            _inventorySO.CollectIngredient(ingredientSO);
            OnCollectIngredientAction?.Invoke(_inventorySO);
        }

        public void CompleteQuest(QuestSO questSO)
        {
            _questListSO.RemoveQuestSO(questSO);
            foreach (IngredientStack ingredientStack in questSO.GetIngredientStackSOList())
            {
                _inventorySO.UseIngredients(ingredientStack);
            }
            questSO.Complete();
        }
    }
}