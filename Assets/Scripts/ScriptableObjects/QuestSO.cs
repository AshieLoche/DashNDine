using System.Collections.Generic;
using DashNDine.EnumSystem;
using DashNDine.StructSystem;

namespace DashNDine.ScriptableObjectSystem
{
    public class QuestSO : BaseSO
    {
        public NPCSO NPCSO;
        public RegionSO RegionSO;
        public string Description;
        public List<QuestObjective> QuestObjectiveList = new List<QuestObjective>();
        public EnumSystem.QuestType QuestType;
        public int MonsterCount;
        public int ReputationRequired;
        public int Reward;
        public int BonusReward;
        public string Prompt;
        public string Waiting;
        public string Success;
        public string Failure;
        public QuestStatus QuestStatus;
    }
}