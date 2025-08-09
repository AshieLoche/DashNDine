using UnityEngine;

namespace DashNDine.ScriptableObjectSystem
{
    public class IngredientListSO : BaseListSO<IngredientSO>
    {
        public IngredientSO GetRandomIngredientSO()
        {
            int index = Random.Range(0, SOList.Count);

            return SOList[index];
        }
    }
}