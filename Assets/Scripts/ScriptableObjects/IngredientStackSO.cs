using System;

namespace DashNDine.ScriptableObjectSystem
{
    [Serializable]
    public class IngredientStackSO
    {
        public IngredientSO IngredientSO;
        public int Amount;

        public IngredientStackSO(IngredientSO ingredientSO, int amount)
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
            IngredientStackSO ingredientStackSO = inventorySO.GetIngredientStackSOByIngredientSO(IngredientSO);

            return Amount == ingredientStackSO.Amount;
        }
    }
}