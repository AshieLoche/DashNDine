using System;

namespace DashNDine.ScriptableObjectSystem
{
    [Serializable]
    public class IngredientStack
    {
        public IngredientSO IngredientSO;
        public int Amount;

        public IngredientStack(IngredientSO ingredientSO, int amount)
        {
            IngredientSO = ingredientSO;
            Amount = amount;
        }

        public void ClearAmount()
            => Amount = 0;

        public bool CompareIngredientSO(IngredientSO ingredientSO)
            => IngredientSO == ingredientSO;

        public void CollectIngredient(IngredientSO ingredientSO)
        {
            if (CompareIngredientSO(ingredientSO))
                Amount++;
        }

        public bool CheckInventory(IngredientStackListSO inventorySO)
        {
            IngredientStack ingredientStackSO = inventorySO.GetIngredientStackSOByIngredientSO(IngredientSO);

            return ingredientStackSO.Amount >= Amount;
        }

        public void UseIngredient(IngredientStack questObjective)
        {
            if (CompareIngredientSO(questObjective.IngredientSO))
                Amount -= questObjective.Amount;
        } 
    }
}