using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "NPC/NPC Data")]
public class NPCData : ScriptableObject
{
    public int regionID;
    public int npcID;
    public string npcName;
    public Sprite npcImage;
    public int completedQuests;
}
