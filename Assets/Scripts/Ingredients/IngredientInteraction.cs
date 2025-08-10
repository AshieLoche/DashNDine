using DashNDine.CoreSystem;
using UnityEngine;

namespace DashNDine.IngredientSystem
{
    public class IngredientInteraction : BaseInteraction
    {
        [SerializeField] private Ingredient _ingredient;

        public override void Interact()
        {
            base.Interact();

            QuestManager.Instance.CollectIngredient(_ingredient.GetIngredientSO());

            _ingredient.DestroySelf();
        }
    }
}