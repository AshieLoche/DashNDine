using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyUI : MonoBehaviour
{
    private List<string> sequenceHolder = new List<string>();
    private List<string> sequence = new List<string>();

    [SerializeField] private TextMeshProUGUI sequenceLabel;
    private string sequenceTxt = "";

    private void Awake()
    {
        // Auto-find TextMeshProUGUI if not set
        if (sequenceLabel == null)
        {
            sequenceLabel = GetComponentInChildren<TextMeshProUGUI>();
            if (sequenceLabel == null)
                Debug.LogWarning($"{name}: No sequenceLabel assigned or found!");
        }
    }

    private void Update()
    {
        if (sequenceLabel != null)
            sequenceLabel.text = sequenceTxt;
    }

    public void SetSequence(TextMeshProUGUI qteText, List<int> seq)
    {
        if (qteText != null)
            sequenceLabel = qteText;

        if (seq == null)
        {
            Debug.LogError($"{name}: QTE sequence list is null!");
            return;
        }

        sequence.Clear();
        sequenceHolder.Clear();
        foreach (int num in seq)
        {
            sequence.Add(num.ToString());
            sequenceHolder.Add(num.ToString());
        }

        UpdateSequenceText();
    }

    public void IndicateCorrectInput(int correctInputs)
    {
        Debug.Log("Retruned " +correctInputs);
        Color myColor = Color.green;
        string hexColor = "";

        if (correctInputs == 0)
        {
            myColor = Color.white;
            hexColor = ColorUtility.ToHtmlStringRGB(myColor);
            Debug.Log("Is it " + sequence.Count);

            for (int i = 0; i < sequence.Count; i++)
            {
                sequenceHolder[i] = $"<color=#{hexColor}>{sequence[i]}</color>";
            }

            UpdateSequenceText();
        }

        else if(correctInputs > 0)
        {
            hexColor = ColorUtility.ToHtmlStringRGB(myColor);

            for (int i = 0; i < correctInputs && i < sequence.Count; i++)
            {
                sequenceHolder[i] = $"<color=#{hexColor}>{sequence[i]}</color>";
            }
            UpdateSequenceText();
        }

        UpdateSequenceText();
    }

    private void UpdateSequenceText()
    {
        sequenceTxt = "";
        foreach (var s in sequenceHolder)
        {
            sequenceTxt += " "+ s + " ";
        }
    }
}
