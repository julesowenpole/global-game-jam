using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    void Awake()
    {
        Instance = this;
    }

    public void Show(DialogueEntry entry, string speaker)
    {
        // UI hook
        Debug.Log($"{speaker}: {entry.dialogueText}");

        // Gibberish sound hook
        // Emotion hook (Pepper mask)
    }
}
