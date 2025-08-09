using DashNDine.EnumSystem;
using UnityEngine;

namespace DashNDine.ScriptableObjectSystem
{
    public class IngredientSO : BaseSO
    {
        public RegionSO RegionSO;
        public SpawnLocation SpawnLocation;
        public IngredientType IngredientType;
        public string PotDefenseEffect;
        public Sprite Sprite;
        public Transform PrefabTransform;
    }
}