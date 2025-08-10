using System.Collections.Generic;

namespace DashNDine.ScriptableObjectSystem
{
    public class QuestListSO : BaseListSO<QuestSO>
    {
        public List<QuestSO> GetQuestSOListByNPCSO(NPCSO npcSO)
            => SOList.FindAll(e => e.CompareNPCSO(npcSO));

        public QuestSO GetLastAvailableQuestSO()
            => SOList.FindLast(e => !e.IsLocked());
            
        public void LockAll()
        {
            foreach (QuestSO questSO in SOList)
            {
                questSO.Lock();
            }
        }

        public void AddQuestSO(QuestSO questSO)
        {
            if (SOList.Contains(questSO))
            {
                int index = GetIndex(questSO);
                SOList[index] = questSO;
            }
            else
                SOList.Add(questSO);
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