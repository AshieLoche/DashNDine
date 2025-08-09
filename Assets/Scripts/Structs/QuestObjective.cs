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

        public QuestObjective(IngredientSO ingredientSO, int requiredAmount, int collectedAmount)
        {
            IngredientSO = ingredientSO;
            RequiredAmount = requiredAmount;
            CollectedAmount = collectedAmount;
        }
    }
}