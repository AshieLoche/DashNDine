using System.Collections.Generic;
using System.Linq;
using DashNDine.ClassSystem;
using UnityEngine;

namespace DashNDine.ScriptableObjectSystem
{
    public class IngredientStackListSO : ScriptableObject
    {
        public List<IngredientStackClass> IngredientStackClassList = new List<IngredientStackClass>();

        public void Clear()
            => IngredientStackClassList.Clear();

        public List<IngredientSO> GetIngredientSOList()
            => IngredientStackClassList.Select(e => e.IngredientSO).ToList();

        public void Add(IngredientStackClass ingredientStackClass)
        {
            if (GetIngredientSOList().Contains(ingredientStackClass.IngredientSO))
            {
                int index = GetIndex(ingredientStackClass);
                IngredientStackClassList[index] = ingredientStackClass;
            }
            else
                IngredientStackClassList.Add(ingredientStackClass);
        }

        public int Count
            => IngredientStackClassList.Count;

        public IngredientStackClass this[int index]
        {
            get { return IngredientStackClassList[index]; }
            set { IngredientStackClassList[index] = value; }
        }

        public void ClearAmount()
        {
            foreach (IngredientStackClass ingredientStackClass in IngredientStackClassList)
            {
                ingredientStackClass.ClearAmount();
            }
        }

        public int GetIndex(IngredientStackClass ingredientStackClass)
            => IngredientStackClassList.FindIndex(e => e == ingredientStackClass);

        public void CollectIngredient(IngredientSO ingredientSO)
        {
            foreach (IngredientStackClass ingredientStackClass in IngredientStackClassList)
            {
                ingredientStackClass.CollectIngredient(ingredientSO);
            }
        }

        public bool CheckInventory(IngredientStackListSO inventorySO)
            => IngredientStackClassList.All(e => e.CheckInventory(inventorySO));

        public IngredientStackClass GetIngredientStackSOByIngredientSO(IngredientSO ingredientSO)
            => IngredientStackClassList.Find(e => e.CompareIngredientSO(ingredientSO));

        public void UseIngredients(IngredientStackClass questObjective)
        {
            foreach (IngredientStackClass ingredientStackSO in IngredientStackClassList)
            {
                ingredientStackSO.UseIngredient(questObjective);
            }
        }
    }
}