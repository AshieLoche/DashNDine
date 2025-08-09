using System.Collections.Generic;
using System.Linq;
using DashNDine.MiscSystem;
using DashNDine.ScriptableObjectSystem;
using UnityEngine;

namespace DashNDine.IngredientSystem
{
    public class IngredientSpawner : SingletonBehaviour<IngredientSpawner>
    {
        [SerializeField] private List<IngredientParent> _ingredientParentList;
        [SerializeField] private float _spawnInterval = 2.5f;
        [SerializeField] private int _spawnCountMax = 5;
        private float _timer = 0f;
        private bool _isSpawning = false;
        private bool _canSpawn = true;
        private IngredientListSO _questIngredientListSO;
        private List<Ingredient> _spawnedIngredientList = new List<Ingredient>();

        private void Update()
        {
            _canSpawn =
                _ingredientParentList.Any(e => !e.HasIngredient()) &&
                _spawnedIngredientList.Count < _spawnCountMax;

            if (!_canSpawn)
                return;

            if (!_isSpawning)
                return;

            _timer -= Time.deltaTime;

            if (_timer <= 0)
            {
                _timer = _spawnInterval;

                List<IngredientParent> unoccupiedIPList = _ingredientParentList.Where(e => !e.HasIngredient()).ToList();
                int unoccupiedIPListIndex = Random.Range(0, unoccupiedIPList.Count);
                IngredientParent unoccupiedIP = unoccupiedIPList[unoccupiedIPListIndex];

                IngredientSO ingredientSO = _questIngredientListSO.GetRandomIngredientSO();

                Ingredient ingredient = Ingredient.SpawnIngredient(ingredientSO, unoccupiedIP);

                _spawnedIngredientList.Add(ingredient);
            }
        }

        public void SetIngredientSpawner(QuestSO questSO)
        {
            IngredientStackListSO questObjectiveList = questSO.QuestObjectiveList;
            _questIngredientListSO = ScriptableObject.CreateInstance<IngredientListSO>();
            _questIngredientListSO.SOList = questObjectiveList.GetIngredientSOList();
            _timer = _spawnInterval;
            _canSpawn = true;
            _isSpawning = true;
        }

        public void RemoveIngredient(Ingredient ingredient)
            => _spawnedIngredientList.Remove(ingredient);
    }
}