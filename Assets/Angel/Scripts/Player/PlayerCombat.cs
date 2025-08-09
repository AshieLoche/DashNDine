using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using ColorUtility = UnityEngine.ColorUtility;

public class PlayerCombatController : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;

    [SerializeField] private TextMeshProUGUI sequenceLabel;
    [SerializeField] private GameObject skillPanel;
    [SerializeField] private GameObject skill1CDImg, skill2CDImg, skill3CDImg;
    [SerializeField] private GameObject skill1, skill2, skill3;
    [SerializeField] float skillCD, skill1Timer, skill2Timer, skill3Timer;
    [SerializeField] List<int> skill1QTE, skill2QTE, skill3QTE;

    private string sequenceTxt;
    private List<string> seqHolder, selectedSeq;

    private List<GameObject> enemiesInRange;

    private InputAction skillAction;
    private InputAction skillSelectAction;
    private InputAction quickTimeEventAction;

    private bool isUsingSkill;
    private bool isSkill1CD, isSkill2CD, isSkill3CD;
    private int keyPressCount;
    private int selectedSkill;

    private void Awake()
    {
        selectedSeq = new List<string>();
        seqHolder = new List<string>();
        enemiesInRange = new List<GameObject>();
        skillAction = InputSystem.actions.FindAction("Skill");
        skillSelectAction = InputSystem.actions.FindAction("SkillSelect");
        quickTimeEventAction = InputSystem.actions.FindAction("QuickTimeEvent");

        skillAction.performed += OnSkillPressed;
        skillAction.canceled += OnSkillReleased;
        skillSelectAction.performed += OnSkillSelect;
    }

    private void Update()
    {
        sequenceLabel.text = sequenceTxt;
        if (isSkill1CD)
        {
            skill1Timer = Mathf.Clamp(skill1Timer - Time.deltaTime, 0, skillCD);
            skill1CDImg.transform.localScale = new Vector2(skill1CDImg.transform.localScale.x, (skill1Timer / skillCD));
            if (skill1Timer == 0) isSkill1CD = false;
        }
        if (isSkill2CD)
        {
            skill2Timer = Mathf.Clamp(skill1Timer - Time.deltaTime, 0, skillCD);
            skill2CDImg.transform.localScale = new Vector2(skill2CDImg.transform.localScale.x, (skill1Timer / skillCD));
            if (skill2Timer == 0) isSkill2CD = false;
        }
        if (isSkill3CD)
        {
            skill3Timer = Mathf.Clamp(skill3Timer - Time.deltaTime, 0, skillCD);
            skill3CDImg.transform.localScale = new Vector2(skill3CDImg.transform.localScale.x, (skill1Timer / skillCD));
            if (skill3Timer == 0) isSkill3CD = false;
        }
    }

    private void OnSkillPressed(InputAction.CallbackContext context)
    {
        if (!playerData.inArena)
        {
            sequenceTxt = "";
            isUsingSkill = true;
            Time.timeScale = 0.5f;
            skillPanel.SetActive(true);
        }
        else return;
    }

    private void OnSkillReleased(InputAction.CallbackContext context)
    {
        StopSkillUse();
    }

    private void OnSkillSelect(InputAction.CallbackContext context)
    {
        if (isUsingSkill)
        {
            var control = context.control.name;

            switch (control)
            {
                case "upArrow":
                    if (!isSkill1CD)
                    {
                        quickTimeEventAction.performed += OnQuickTimeEvent;
                        selectedSkill = 1;
                        Debug.Log("Up skill selected");
                    }
                    break;
                case "leftArrow":
                    if (!isSkill2CD)
                    {
                        quickTimeEventAction.performed += OnQuickTimeEvent;
                        selectedSkill = 2;
                        Debug.Log("Left skill selected");
                    }
                    break;
                case "rightArrow":
                    if (!isSkill3CD)
                    {
                        quickTimeEventAction.performed += OnQuickTimeEvent;
                        selectedSkill = 3;
                        Debug.Log("Right skill selected");
                    }
                    break;
            }
            SetQTERequired();
            keyPressCount = 0;
        }
    }
    private void StopSkillUse()
    {
        if(isUsingSkill)
        {
            isUsingSkill = false;
            Time.timeScale = 1f;
            skillPanel.SetActive(false);
            quickTimeEventAction.performed -= OnQuickTimeEvent;
            sequenceTxt = " ";
        }
    }

    private void OnQuickTimeEvent(InputAction.CallbackContext context)
    {

        // Which control triggered this?
        var control = context.control;

        if (control is KeyControl keyControl)
        {
            if (keyControl.keyCode == Key.Digit1)
            {
                Debug.Log("1 has been Pressed");
                CheckQTEInput(1);
            }
            if (keyControl.keyCode == Key.Digit2)
            {
                Debug.Log("2 has been Pressed");
                CheckQTEInput(2);
            }
            if (keyControl.keyCode == Key.Digit3)
            {
                Debug.Log("3 has been Pressed");
                CheckQTEInput(3);
            }
            if (keyControl.keyCode == Key.Digit4)
            {
                Debug.Log("4 has been Pressed");
                CheckQTEInput(4);
            }
            if (keyControl.keyCode == Key.Digit5)
            {
                Debug.Log("5 has been Pressed");
                CheckQTEInput(5);
            }
            if (keyControl.keyCode == Key.Digit6)
            {
                Debug.Log("6 has been Pressed");
                CheckQTEInput(6);
            }
        }
    }
    void SetQTERequired()
    {
        selectedSeq.Clear();
        seqHolder.Clear();
        switch (selectedSkill)
        {
            case 1:
                if (!isSkill1CD)
                {
                    foreach (int num in skill1QTE)
                    {
                        selectedSeq.Add(num.ToString());
                        seqHolder.Add(num.ToString());
                    }
                }
                break;
            case 2:
                if (!isSkill2CD)
                {
                    foreach (int num in skill2QTE)
                    {
                        selectedSeq.Add(num.ToString());
                        seqHolder.Add(num.ToString());
                    }
                }
                break;
            case 3:
                if (!isSkill3CD)
                {
                    foreach (int num in skill3QTE)
                    {
                        selectedSeq.Add(num.ToString());
                        seqHolder.Add(num.ToString());
                    }
                }
                break;
            default: break;
        }
        UpdateSequenceText();
    }
    void CheckQTEInput(int inputtedKey)
    {

        if (inputtedKey.ToString() == selectedSeq[keyPressCount])
        {
            keyPressCount++;
            if (keyPressCount == 6)
            {
                SkillEffect();
            }
                
        }
        else
        {
            keyPressCount = 0;
            switch(selectedSkill)
            {
                case 1:
                    skill1Timer = skillCD;
                    isSkill1CD = true;
                    break;
                case 2:
                    skill2Timer = skillCD;
                    isSkill2CD = true;
                    break;
                case 3:
                    skill3Timer = skillCD;
                    isSkill3CD = true;
                    break;
            }
            StopSkillUse();
        }
        IndicateCorrectInput(keyPressCount);
    }
    public void IndicateCorrectInput(int correctInputs)
    {
        Debug.Log("Retruned " + correctInputs);
        Color myColor = Color.green;
        string hexColor = "";

        if (correctInputs == 0)
        {
            myColor = Color.white;
            hexColor = ColorUtility.ToHtmlStringRGB(myColor);
            Debug.Log("Is it " + selectedSeq.Count);

            for (int i = 0; i < selectedSeq.Count; i++)
            {
                seqHolder[i] = $"<color=#{hexColor}>{selectedSeq[i]}</color>";
            }

            UpdateSequenceText();
        }

        else if (correctInputs > 0)
        {
            hexColor = ColorUtility.ToHtmlStringRGB(myColor);

            for (int i = 0; i < correctInputs && i < selectedSeq.Count; i++)
            {
                seqHolder[i] = $"<color=#{hexColor}>{selectedSeq[i]}</color>";
            }
            UpdateSequenceText();
        }

        UpdateSequenceText();
    }
    private void UpdateSequenceText()
    {
        sequenceTxt = "";
        foreach (var s in seqHolder)
        {
            sequenceTxt += " " + s + " ";
        }
    }
    void SkillEffect()
    {
        switch(selectedSkill)
        {
            case 1:
                skill1.SetActive(true) ;
                if (enemiesInRange != null)
                {
                    foreach (var e in enemiesInRange)
                    {
                        e.GetComponent<EnemyManager>().BurnEnemy(1);
                    }
                }
                skill1Timer = skillCD;
                isSkill1CD = true;

                break;
            case 2:
                skill2.SetActive(true);
                if (enemiesInRange != null)
                {
                    foreach (var e in enemiesInRange)
                    {
                        e.GetComponent<EnemyManager>().SlowEnemy(0.5f);
                    }
                }
                skill2Timer = skillCD;
                isSkill2CD = true;
                break;
            case 3:
                skill3.SetActive(true);
                skill3Timer = skillCD;
                isSkill3CD = true;
                break;
            default:
                Debug.Log("Skills Not Found.");
                break;
        }
        StopSkillUse();
    }

    private void OnDestroy()
    {
        skillAction.performed -= OnSkillPressed;
        skillAction.canceled -= OnSkillReleased;
        skillSelectAction.performed -= OnSkillSelect;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            enemiesInRange.Add(collision.gameObject);
        }
    }
}
