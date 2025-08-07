using NUnit.Framework;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    [SerializeField] private IngredientData ingredientData;
    [SerializeField] private bool isAcquired = false;
    public bool IsAcquired => isAcquired;
    public Region Region => ingredientData.region;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.
        GetComponent<SpriteRenderer>().sprite = ingredientData.ingredientImage;
    }
    public void SetInPosition(Vector3 location)
    {
        transform.position = location;
    }


    public void Harvest()
    {
        isAcquired = true;
    }
}
