using System.Collections.Generic;
using System.Linq;
using DashNDine.EnumSystem;

namespace DashNDine.ScriptableObjectSystem
{
    public class QuestSO : BaseSO
    {
        public NPCSO NPCSO;
        public RegionSO RegionSO;
        public string Description;
        public IngredientStackListSO QuestObjectiveList;
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

        public List<IngredientStack> GetIngredientStackSOList()
            => QuestObjectiveList.IngredientStackSOList;

        public IngredientStackListSO GetQuestObjectiveList()
            => QuestObjectiveList;

        public void ClearObjectiveListAmount()
            => QuestObjectiveList.ClearObjectiveAmount();

        public void CollectIngredient(IngredientSO ingredientSO)
            => QuestObjectiveList.CollectIngredient(ingredientSO);

        public bool CompareNPCSO(NPCSO nPCSO)
            => NPCSO == nPCSO;

        public bool CheckInventory(IngredientStackListSO inventorySO)
            => QuestObjectiveList.CheckInventory(inventorySO);

        public void Lock()
            => SetStatus(QuestStatus.Locked);

        public bool IsLocked()
            => QuestStatus == QuestStatus.Locked;

        public void Unlock()
            => SetStatus(QuestStatus.Unlocked);

        public void Wait()
            => SetStatus(QuestStatus.Waiting);

        public void Complete()
            => SetStatus(QuestStatus.Success);

        public void Fail()
            => SetStatus(QuestStatus.Failure);

        public void SetStatus(QuestStatus questStatus)
            => QuestStatus = questStatus;

        public QuestStatus GetStatus()
            => QuestStatus;

        public NPCSO GetNPCSO()
            => NPCSO;
    }
}