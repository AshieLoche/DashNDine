using System.Collections.Generic;

namespace DashNDine.ScriptableObjectSystem
{
    public class QuestListSO : BaseListSO<QuestSO>
    {   
        public List<QuestSO> GetQuestSOListByNPCSO(NPCSO npcSO)
            => SOList.FindAll(e => e.CompareNPCSO(npcSO));

        public QuestSO GetLastAvailableQuestSO()
            => SOList.FindLast(e => !e.IsLocked());

        public void AddQuestSO(QuestSO questSO)
        {
            List<QuestSO> questSOList = SOList;

            if (questSOList.Contains(questSO))
            {
                int index = GetIndex(questSO);
                questSOList[index] = questSO;
            }
            else
                questSOList.Add(questSO);
        }

        public void RemoveQuestSO(QuestSO questSO)
        {
            List<QuestSO> questSOList = SOList;

            if (!questSOList.Contains(questSO))
                return;

            questSOList.Remove(questSO);
        }
    }
}