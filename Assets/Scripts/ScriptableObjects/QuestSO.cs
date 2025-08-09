using System.Collections.Generic;
using System.Linq;
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
        public QuestType QuestType;
        public int MonsterCount;
        public int ReputationRequired;
        public int Reward;
        public int BonusReward;
        public string Prompt;
        public string Waiting;
        public string Success;
        public string Failure;
        public QuestStatus QuestStatus;

        public void ResetQuestObjectiveList()
        {
            for (int i = 0; i < QuestObjectiveList.Count; i++)
            {
                QuestObjective questObjective = QuestObjectiveList[i];
                questObjective.CollectedAmount = 0;
                QuestObjectiveList[i] = questObjective;
            }
        }

        public void CollectIngredient(IngredientSO ingredientSO)
        {
            for (int i = 0; i < QuestObjectiveList.Count; i++)
            {
                QuestObjective questObjective = QuestObjectiveList[i];
                questObjective.CollectIngredient(ingredientSO);
                QuestObjectiveList[i] = questObjective;
            }
        }

        public bool CompareAmount()
            => QuestObjectiveList.All(e => e.CompareAmount());
    }
}