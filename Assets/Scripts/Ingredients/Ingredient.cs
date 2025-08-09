using System;
using DashNDine.ScriptableObjectSystem;
using UnityEngine;

namespace DashNDine.IngredientSystem
{
    public class Ingredient : MonoBehaviour
    {
        public Action<Sprite> OnIngredientSpawned;

        private IngredientSO _ingredientSO;
        private IngredientParent _ingredientParent;

        public void SetIngredientSO(IngredientSO ingredientSO)
            => _ingredientSO = ingredientSO;

        public IngredientSO GetIngredientSO()
            => _ingredientSO;

        public void SetIngredientParent(IngredientParent ingredientParent)
        {
            if (_ingredientParent != null)
                _ingredientParent.ClearIngredient();

            if (ingredientParent.HasIngredient())
                Debug.LogError($"{ingredientParent.name} already has an ingredient.");

            _ingredientParent = ingredientParent;
            ingredientParent.SetIngredient(this);
            transform.parent = ingredientParent.transform;
            transform.localPosition = Vector3.zero;
        }

        public IngredientParent GetIngredientParent()
            => _ingredientParent;

        public void DestroySelf()
        {
            _ingredientParent.ClearIngredient();

            Destroy(gameObject);
        }

        public static Ingredient SpawnIngredient(IngredientSO ingredientSO, IngredientParent ingredientParent)
        {
            Transform ingredientTransform = Instantiate(ingredientSO.PrefabTransform, ingredientParent.transform);

            if (ingredientTransform.TryGetComponent(out Ingredient ingredient))
            {
                ingredient.SetIngredientSO(ingredientSO);
                ingredient.SetIngredientParent(ingredientParent);

                return ingredient;
            }

            Debug.LogError($"Unable to spawn {ingredientSO.name}");
            return null;
        }
    }
}