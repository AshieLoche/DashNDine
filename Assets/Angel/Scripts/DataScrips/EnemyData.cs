using UnityEngine;
public enum Difficulty
{
    Easy, Normal, Hard
}

[CreateAssetMenu(menuName = "Scriptables/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string type;
    public int maxHp;
    public Sprite image;
    public float baseSpeed;
    public int baseDmg;
    public Difficulty difficulty;
    public RuntimeAnimatorController controller;
    public string notes;
}
