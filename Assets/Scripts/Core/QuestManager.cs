using System;
using DashNDine.MiscSystem;
using DashNDine.ScriptableObjectSystem;
using UnityEngine;

namespace DashNDine.CoreSystem
{
    public class QuestManager : SingletonBehaviour<QuestManager>
    {
        public Action OnCollectIngredientAction;

        private QuestListSO _questListSO;

        protected override void Awake()
        {
            base.Awake();

            _questListSO = ScriptableObject.CreateInstance<QuestListSO>();
        }

        private void Start()
        {
            
        }

        public void AddQuest(QuestSO questSO)
            => _questListSO.AddQuestSO(questSO);

        public void CollectIngredient(IngredientSO ingredientSO)
        {
            _questListSO.CollectIngredient(ingredientSO);
            OnCollectIngredientAction?.Invoke();
        }
    }
}