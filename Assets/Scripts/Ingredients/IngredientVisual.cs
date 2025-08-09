using UnityEngine;

namespace DashNDine.IngredientSystem
{
    public class IngredientVisual : MonoBehaviour
    {
        [SerializeField] private Ingredient _ingredient;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            if (_spriteRenderer == null)
                _spriteRenderer = GetComponent<SpriteRenderer>();

            _ingredient.OnIngredientSpawned
                += Ingredient_OnIngredientSpawned;
        }

        private void OnDestroy()
        {
            if (_ingredient == null)
                return;

            _ingredient.OnIngredientSpawned
                -= Ingredient_OnIngredientSpawned;
        }

        private void Ingredient_OnIngredientSpawned(Sprite sprite)
            => _spriteRenderer.sprite = sprite;
    }
}