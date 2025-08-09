using DashNDine.CoreSystem;
using DashNDine.EnumSystem;
using DashNDine.ScriptableObjectSystem;
using UnityEngine;

namespace DashNDine.NPCSystem
{
    public class NPC : MonoBehaviour
    {
        [SerializeField] private NPCSO _npcSO;
        [SerializeField] private QuestListSO _questListSO;
        private QuestListSO _personalQuestSOList;
        private ReputationManager _reputationManager;

        private void Awake()
        {
            _personalQuestSOList = ScriptableObject.CreateInstance<QuestListSO>();
            _personalQuestSOList.SOList = _questListSO.GetQuestSOListByNPCSO(_npcSO);
        }

        private void Start()
        {
            _reputationManager = ReputationManager.Instance;

            _reputationManager.OnReputationUpdateAction
                += ReputationManager_OnReputationUpdateAction;
        }

        private void OnDestroy()
        {
            if (_reputationManager == null)
                return;

            _reputationManager.OnReputationUpdateAction
                -= ReputationManager_OnReputationUpdateAction;
        }

        private void ReputationManager_OnReputationUpdateAction(int reputationAMount)
            => UpdateQuestAvailability(reputationAMount);

        public NPCSO GetNPCSO()
            => _npcSO;

        public QuestSO GetQuestSO()
            => _personalQuestSOList.GetLastAvailableQuestSO();

        public void UpdateQuestAvailability(int reputationAMount)
        {
            foreach (QuestSO personalQuestSO in _personalQuestSOList.SOList)
            {
                personalQuestSO.QuestStatus =
                    (personalQuestSO.ReputationRequired == reputationAMount) ?
                    QuestStatus.Unlocked :
                    QuestStatus.Locked;
            }
        }
    }
}