using System.Collections.Generic;
using DashNDine.EnumSystem;

namespace DashNDine.ScriptableObjectSystem
{
    public class QuestListSO : BaseListSO<QuestSO>
    {
        public List<QuestSO> GetQuestSOListByNPCSO(NPCSO npcSO)
            => SOList.FindAll(e => e.NPCSO == npcSO);

        public QuestSO GetLastAvailableQuestSO()
            => SOList.FindLast(e => e.QuestStatus != QuestStatus.Locked);

        public void AddQuestSO(QuestSO questSO)
        {
            if (SOList.Contains(questSO))
            {
                int index = SOList.FindIndex(e => e == questSO);
                SOList[index] = questSO;
            }
            else
                SOList.Add(questSO);
        }

        public void ResetQuestObjectList()
        {
            foreach (QuestSO questSO in SOList)
            {
                questSO.ResetQuestObjectiveList();
            }
        }

        public void CollectIngredient(IngredientSO ingredientSO)
        {
            foreach (QuestSO questSO in SOList)
            {
                questSO.CollectIngredient(ingredientSO);
            }
        }
    }
}