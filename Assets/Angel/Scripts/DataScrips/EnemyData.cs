using UnityEngine;
public enum Difficulty
{
    Easy, Normal, Hard
}

[CreateAssetMenu(menuName = "Scriptables/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public string type;
    public Sprite image;
    public Difficulty difficulty;
    public string notes;
}
