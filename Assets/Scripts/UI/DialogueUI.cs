using System;
using DashNDine.EnumSystem;
using DashNDine.GameInputSystem;
using DashNDine.MiscSystem;
using DashNDine.ScriptableObjectSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DashNDine.UISystem
{
    public class DialogueUI : SingletonBehaviour<DialogueUI>
    {
        [SerializeField] private Button _dialogueUIButton;
        [SerializeField] private Image _speakerImage;
        [SerializeField] private TextMeshProUGUI _speakerName;
        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private GameObject _skipPrompt;
        [SerializeField] private ChoicesUI _choicesUI;
        [SerializeField] private float _typingSpeed = 0.05f;
        private PlayerInputManager _playerInputManager;
        private QuestSO _questSO;
        private string _dialogue;
        private int _charIndex = 0;
        private float _timer = 0f;
        private bool _isTyping = false;

        protected override void Awake()
        {
            base.Awake();

            _dialogueUIButton.onClick.AddListener(SkipTyping);
            
            _choicesUI.OnAcceptAction
                += ChoicesUI_OnAcceptAction;
            _choicesUI.OnGiveAction
                += ChoicesUI_OnGiveAction;
            _choicesUI.OnCookAction
                += ChoicesUI_OnCookAction;
            _choicesUI.OnLeaveAction
                += ChoicesUI_OnLeaveAction;

            ResetDialogue();
        }

        private void Start()
        {
            _playerInputManager = PlayerInputManager.Instance;

            _playerInputManager.OnDialogueSkipPerformedAction
                += PlayerInputManager_OnDialogueSkipPerformedAction;
        }

        private void OnDestroy()
        {
            if (_playerInputManager != null)
                _playerInputManager.OnDialogueSkipPerformedAction
                    += PlayerInputManager_OnDialogueSkipPerformedAction;

            if (_choicesUI != null)
            {
                _choicesUI.OnAcceptAction
                    -= ChoicesUI_OnAcceptAction;
                _choicesUI.OnGiveAction
                    -= ChoicesUI_OnGiveAction;
                _choicesUI.OnCookAction
                    -= ChoicesUI_OnCookAction;
                _choicesUI.OnLeaveAction
                    -= ChoicesUI_OnLeaveAction;
            }
        }

        private void ChoicesUI_OnCookAction()
            => ResetDialogue();

        private void ChoicesUI_OnGiveAction()
            => ResetDialogue();

        private void ChoicesUI_OnAcceptAction()
            => ResetDialogue();

        private void ChoicesUI_OnLeaveAction()
            => ResetDialogue();

        private void PlayerInputManager_OnDialogueSkipPerformedAction()
            => SkipTyping();

        public void SetDialogueByQuestSO(QuestSO questSO)
        {
            ResetDialogue();
            _questSO = questSO;
            NPCSO npcSO = questSO.GetNPCSO();
            _speakerImage.sprite = npcSO.spriteHead;
            _speakerName.text = npcSO.Name;
            StartTyping(questSO);
            SetVisibility(true);
        }

        private void SkipTyping()
        {
            _dialogueText.text = _dialogue;
            SetChoicesUI();
        }

        private void StartTyping(QuestSO questSO)
        {
            _dialogue = questSO.GetStatus() switch
            {
                QuestStatus.Unlocked => questSO.Prompt,
                QuestStatus.Waiting => questSO.Waiting,
                QuestStatus.Success => questSO.Success,
                QuestStatus.Failure => questSO.Failure,
                _ => ""
            };
            _charIndex = 0;
            _dialogueText.text = "";
            _timer = 0f;
            _isTyping = true;
            _skipPrompt.SetActive(true);
        }

        private void Update()
        {
            if (!_isTyping)
                return;

            _timer += Time.deltaTime;

            if (_timer >= _typingSpeed)
            {
                _timer = 0f;

                _dialogueText.text += _dialogue[_charIndex];
                _charIndex++;

                if (_charIndex >= _dialogue.Length)
                    SetChoicesUI();
            }
        }

        private void SetChoicesUI()
        {
            _isTyping = false;
            _skipPrompt.SetActive(false);
            _choicesUI.SetChoices(_questSO);
        }

        private void ResetDialogue()
        {
            _speakerImage.sprite = null;
            _speakerName.text = "";
            _dialogueText.text = "";
            _skipPrompt.SetActive(false);
            _choicesUI.ResetChoices();
            SetVisibility(false);
        }

        private void SetVisibility(bool isVisibility)
            => gameObject.SetActive(isVisibility);
    }
}