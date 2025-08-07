using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class IngredientSpawner : MonoBehaviour
{
    [Header("World Locations")]
    [SerializeField] List<Vector2> region1SpawnSpots;
    [SerializeField] List<Vector2> region2SpawnSpots;
    [SerializeField] List<Vector2> region3SpawnSpots;
    [SerializeField] List<Vector2> region4SpawnSpots;

    [Header("World Locations")]
    [SerializeField] List<Vector2> towerTopSpawnSpots;

    public void SpawnIngredients(List<GameObject> ingredients)
    {
        foreach(var ingredient in ingredients)
        {
            Instantiate(ingredient, GetSpawnSpot(ingredient.GetComponent<IngredientManager>().Region), Quaternion.identity);
        }
    }
    public Vector2 GetSpawnSpot(Region reg)
    {
        List<Vector2> possibleSpots = GetPossiblePositions(reg);
        var num = Random.Range(0, possibleSpots.Count - 1);
        return possibleSpots[num];
    }
    public List<Vector2> GetPossiblePositions(Region reg)
    {
        switch(reg)
        {
            case Region.One: 
                return region1SpawnSpots;
            case Region.Two:
                return region2SpawnSpots;
            case Region.Three:
                return region3SpawnSpots;
            case Region.Four:
                return region4SpawnSpots;
            default:
                return null;
        }
    }
}
