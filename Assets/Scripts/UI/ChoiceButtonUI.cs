using DashNDine.EnumSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DashNDine.UISystem
{
    public class ChoiceButtonUI : MonoBehaviour
    {
        [SerializeField] private Button _choiceButton;
        [SerializeField] private TextMeshProUGUI _choiceButtonText;
        private QuestType _questType;

        private void Awake()
        {
            _choiceButton.onClick.AddListener(Click);
        }

        private void Click()
        {
            Debug.Log("CLICKED");
        }

        public void UpdateChoiceButtonText(string newChoice)
            => _choiceButtonText.text = newChoice;

        public void SetQuestType(QuestType questType)
            => _questType = questType;

        // public void SetText(QuestStatus questStatus, QuestType questType)
        // {
        //     string text = switch
        //     // Return = Give

        // }

        private void SetText(string text)
            => _choiceButtonText.text = text;

        public void SetVisibility(bool isVisible)
            => gameObject.SetActive(isVisible);
    }
}