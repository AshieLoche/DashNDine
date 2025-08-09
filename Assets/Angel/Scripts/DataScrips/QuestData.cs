using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public enum QuestType1
{
    Return, Defense, Choice
}
[CreateAssetMenu(menuName = "Scriptables/Quest Data")]
public class QuestData : ScriptableObject
{
    public int questID;
    public int npcID;
    public string Description;
    public QuestType1 type;
    public int enemyNumbers;
    public List<GameObject> requiredIngredients;
    public int reputation;
}
