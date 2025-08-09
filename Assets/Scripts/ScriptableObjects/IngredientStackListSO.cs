using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DashNDine.ScriptableObjectSystem
{
    public class IngredientStackListSO : ScriptableObject
    {
        public List<IngredientStack> IngredientStackSOList = new List<IngredientStack>();

        public void Clear()
            => IngredientStackSOList.Clear();

        public List<IngredientSO> GetIngredientSOList()
            => IngredientStackSOList.Select(e => e.IngredientSO).ToList();

        public void Add(IngredientStack ingredientStack)
            => IngredientStackSOList.Add(ingredientStack);

        public int Count
            => IngredientStackSOList.Count;

        public IngredientStack this[int index]
        {
            get { return IngredientStackSOList[index]; }
            set { IngredientStackSOList[index] = value; }
        }

        public void ClearObjectiveAmount()
        {
            foreach (IngredientStack ingredientStack in IngredientStackSOList)
            {
                ingredientStack.ClearAmount();
            }
        }

        public int GetIndex(IngredientStack ingredientStack)
            => IngredientStackSOList.FindIndex(e => e == ingredientStack);

        public void CollectIngredient(IngredientSO ingredientSO)
        {
            foreach (IngredientStack ingredientStack in IngredientStackSOList)
            {
                ingredientStack.CollectIngredient(ingredientSO);
            }
        }

        public bool CheckInventory(IngredientStackListSO inventorySO)
            => IngredientStackSOList.All(e => e.CheckInventory(inventorySO));

        public IngredientStack GetIngredientStackSOByIngredientSO(IngredientSO ingredientSO)
            => IngredientStackSOList.Find(e => e.CompareIngredientSO(ingredientSO));

        public void UseIngredients(IngredientStack questObjective)
        {
            foreach (IngredientStack ingredientStackSO in IngredientStackSOList)
            {
                ingredientStackSO.UseIngredient(questObjective);
            }
        }
    }
}