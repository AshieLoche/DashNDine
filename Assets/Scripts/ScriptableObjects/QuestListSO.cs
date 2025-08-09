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
    }
}