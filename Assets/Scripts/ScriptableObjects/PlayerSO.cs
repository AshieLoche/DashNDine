using UnityEngine;

namespace DashNDine.ScriptableObjectSystem
{
    [CreateAssetMenu(fileName = "newPlayerSO", menuName = "Player SO")]
    public class PlayerSO : BaseSO
    {
        public int ReputationAmount;
        public IngredientStackListSO InventorySO;

        public void ClearReputationAmount()
            => ReputationAmount = 0;
    }
}