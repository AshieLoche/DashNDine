using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.ComponentModel;

public class DialogueChoiceUI : MonoBehaviour
{
    [SerializeField] private Button[] choiceButtons;
    [SerializeField] private int selectedIndex = 0;
    [SerializeField] private bool isChoosing = false;

    void Start()
    {
        HighlightButton(selectedIndex);
    }

    void Update()
    {
        if (!isChoosing) return;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectedIndex = Mathf.Clamp(selectedIndex-1, 0, 2);
            HighlightButton(selectedIndex);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedIndex = Mathf.Clamp(selectedIndex + 1, 0, 2);
            HighlightButton(selectedIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Z))
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
