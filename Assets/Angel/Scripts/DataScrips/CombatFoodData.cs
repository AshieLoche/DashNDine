using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Combat Food Data")]
public class CombatFoodData : ScriptableObject
{
    public string foodName;
    public Sprite image;
    public int[] qteChain;
}
