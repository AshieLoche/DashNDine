using UnityEngine;

[CreateAssetMenu(menuName = "Scriptables/Player Data")]
public class PlayerData : ScriptableObject
{
    public int maxHP = 10;
    public int currentHP = 10;
    public int Reputation = 0;
    public int killedEnemies = 0;
    public bool inArena = false;
    public float maxAmbushCDTime = 10, minAmbushCDTime = 7, ambushTimer = 0;
}
