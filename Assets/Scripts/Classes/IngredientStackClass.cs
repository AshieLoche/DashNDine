using System;
using DashNDine.ScriptableObjectSystem;

namespace DashNDine.ClassSystem
{
    [Serializable]
    public class IngredientStackClass
    {
        public IngredientSO IngredientSO;
        public int Amount;

        public IngredientStackClass(IngredientSO ingredientSO, int amount)
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
            IngredientStackClass ingredientStackclass = inventorySO.GetIngredientStackSOByIngredientSO(IngredientSO);

            return ingredientStackclass.Amount >= Amount;
        }

        public void UseIngredient(IngredientStackClass questObjective)
        {
            if (CompareIngredientSO(questObjective.IngredientSO))
                Amount -= questObjective.Amount;
        }
    }
}