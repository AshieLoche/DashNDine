using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.ComponentModel;
using UnityEngine.InputSystem;

public class DialogueChoiceUI : MonoBehaviour
{
    [SerializeField] private Button[] choiceButtons;
    private int selectedIndex = 0;
    private bool isChoosing = false;
    private InputAction uiNavInput;

    void Start()
    {
        uiNavInput = InputSystem.actions.FindAction("Navigate");
        HighlightButton(selectedIndex);
    }

    void Update()
    {
        if (uiNavInput.ReadValue<Vector2>().x != 0) StartChoosing();

        if (!isChoosing) return;

        if (uiNavInput.ReadValue<Vector2>().x < 0)
        {
            selectedIndex = Mathf.Clamp(selectedIndex - 1, 0, 2);
            HighlightButton(selectedIndex);
        }
        else if (uiNavInput.ReadValue<Vector2>().x > 0)
        {
            selectedIndex = Mathf.Clamp(selectedIndex + 1, 0, 2);
            HighlightButton(selectedIndex);
        }
        else if (InputSystem.actions.FindAction("Submit").WasPerformedThisFrame())
        {
            choiceButtons[selectedIndex].onClick.Invoke();
            isChoosing = false;
        }
    }

    public void StartChoosing()
    {
        isChoosing = true;
        selectedIndex = 0;
        HighlightButton(selectedIndex);
    }

    private void HighlightButton(int index)
    {
        EventSystem.current.SetSelectedGameObject(choiceButtons[index].gameObject);

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            ColorBlock colors = choiceButtons[i].colors;
            colors.normalColor = (i == index) ? Color.yellow : Color.white;
            choiceButtons[i].colors = colors;
        }
    }
}
