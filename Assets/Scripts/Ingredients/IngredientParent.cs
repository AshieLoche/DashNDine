using UnityEngine;

namespace DashNDine.IngredientSystem
{
    public class IngredientParent : MonoBehaviour
    {
        private Ingredient _ingredient;

        public Transform GetTransform()
            => transform;

        public void SetIngredient(Ingredient ingredient)
            => _ingredient = ingredient;

        public Ingredient GetIngredient()
            => _ingredient;

        public void ClearIngredient()
            => _ingredient = null;

        public bool HasIngredient()
            => _ingredient != null;
    }
}