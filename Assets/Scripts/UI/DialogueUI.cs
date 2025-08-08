using DashNDine.EnumSystem;
using DashNDine.MiscSystem;
using DashNDine.ScriptableObjectSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DashNDine.UISystem
{
    public class DialogueUI : SingletonBehaviour<DialogueUI>
    {
        [SerializeField] private Image _speakerImage;
        [SerializeField] private TextMeshProUGUI _speakerName;
        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private GameObject skipPrompt;
        [SerializeField] private ChoiceButtonUI[] _choiceButtonUIArray;
        [SerializeField] private float typingSpeed = 0.05f;
        private string dialogueTxtHolder;
        private QuestSO _questSO;

        protected override void Awake()
        {
            base.Awake();

            ResetDialogue();
        }

        public void SetDialogueByQuestSO(QuestSO questSO)
        {
            _questSO = questSO;
            NPCSO npcSO = questSO.NPCSO;
            _speakerImage.sprite = npcSO.spriteHead;
            _speakerName.text = npcSO.Name;
            _dialogueText.text = questSO.QuestStatus switch
            {
                QuestStatus.Unlocked => questSO.Prompt,
                QuestStatus.Waiting => questSO.Waiting,
                QuestStatus.Success => questSO.Success,
                QuestStatus.Failure => questSO.Failure,
                _ => ""
            };
            // foreach (ChoiceButtonUI choiceButtonUI in _choiceButtonUIArray)
            // {
            //     if (questSO.QuestStatus == QuestStatus.Unlocked)

            //     choiceButtonUI.SetVisibility(false);
            // }
            SetVisibility(true);
        }

        private void ResetDialogue()
        {
            _speakerImage.sprite = null;
            _speakerName.text = "";
            _dialogueText.text = "";
            skipPrompt.SetActive(false);
            foreach (ChoiceButtonUI choiceButtonUI in _choiceButtonUIArray)
            {
                choiceButtonUI.SetVisibility(false);
            }
            SetVisibility(false);
        }

        private void SetVisibility(bool isVisibility)
            => gameObject.SetActive(isVisibility);
    }
}