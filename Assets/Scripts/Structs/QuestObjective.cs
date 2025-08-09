using System;
using DashNDine.ScriptableObjectSystem;

namespace DashNDine.StructSystem
{
    [Serializable]
    public struct QuestObjective
    {
        public IngredientSO IngredientSO;
        public int RequiredAmount;
        public int CollectedAmount;
        public bool IsCooked;

        public bool CompareAmount()
            => RequiredAmount == CollectedAmount;

        public bool CompareIngredientSO(IngredientSO ingredientSO)
            => IngredientSO == ingredientSO;

        public void CollectIngredient(IngredientSO ingredientSO)
        {
            if (CompareIngredientSO(ingredientSO))
                CollectedAmount++;
        }

        public QuestObjective(IngredientSO ingredientSO, int requiredAmount, int collectedAmount = 0, bool isCooked = false)
        {
            IngredientSO = ingredientSO;
            RequiredAmount = requiredAmount;
            CollectedAmount = collectedAmount;
            IsCooked = isCooked;
        }
    }
}