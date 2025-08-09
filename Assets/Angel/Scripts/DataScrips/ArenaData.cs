using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Arena Information")]
public class ArenaData: ScriptableObject
{
    public enemyType enemyType;
    public Difficulty difficulty;
}
