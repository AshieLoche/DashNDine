using UnityEngine;

public class GameManager : MonoBehaviour
{
    public QuestManager questManager;
    public DialogueManager dialogueManager;
    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
