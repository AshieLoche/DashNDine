using System.Collections.Generic;
using UnityEngine;

public enum Region
{
    One, Two, Three, Four
}

public enum Location
{
    World, TowerTop
}

[CreateAssetMenu(menuName = "Scriptables/Ingredient Data")]
public class IngredientData : ScriptableObject
{
    public int ingredientID;
    public string ingredientName;
    public Sprite ingredientImage;
    public bool isSpecial;
    public Region region;
    public Location location;
    public string AdditionalEffect;
}
