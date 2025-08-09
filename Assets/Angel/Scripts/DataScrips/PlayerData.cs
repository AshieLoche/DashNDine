using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Player Data")]
public class PlayerData : ScriptableObject
{
    public int maxHP;
    public int currentHP;
    public int Reputation;
    public int killedEnemies;

    public float maxAmbushCDTime, minAmbushCDTime, ambushTimer;
}
