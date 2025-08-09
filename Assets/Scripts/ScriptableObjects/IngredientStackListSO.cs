using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DashNDine.ScriptableObjectSystem
{
    public class IngredientStackListSO : ScriptableObject
    {
        public List<IngredientStackSO> IngredientStackSOList = new List<IngredientStackSO>();

        public void Clear()
            => IngredientStackSOList.Clear();

        public List<IngredientSO> GetIngredientSOList()
            => IngredientStackSOList.Select(e => e.IngredientSO).ToList();

        public void Add(IngredientStackSO ingredientStackSO)
            => IngredientStackSOList.Add(ingredientStackSO);

        public int Count
            => IngredientStackSOList.Count;

        public IngredientStackSO this[int index]
        {
            get { return IngredientStackSOList[index]; }
            set { IngredientStackSOList[index] = value; }
        }

        public void ClearObjectiveAmount()
        {
            foreach (IngredientStackSO ingredientStackSO in IngredientStackSOList)
            {
                ingredientStackSO.ClearAmount();
            }
        }

        public int GetIndex(IngredientStackSO ingredientStackSO)
            => IngredientStackSOList.FindIndex(e => e == ingredientStackSO);

        public void CollectIngredient(IngredientSO ingredientSO)
        {
            foreach (IngredientStackSO ingredientStackSO in IngredientStackSOList)
            {
                ingredientStackSO.CollectIngredient(ingredientSO);
            }
        }

        public bool CheckInventory(IngredientStackListSO inventorySO)
            => IngredientStackSOList.All(e => e.CheckInventory(inventorySO));

        public IngredientStackSO GetIngredientStackSOByIngredientSO(IngredientSO ingredientSO)
            => IngredientStackSOList.Find(e => e.CompareIngredientSO(ingredientSO));

        public void UseIngredients(IngredientStackListSO ingredientStackListSO)
        {
            // foreach (IngredientStackSO ingredientStackSO in ingredientStackListSO)
            // {

            // }
        }
    }
}