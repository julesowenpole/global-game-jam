using UnityEngine;
using System.Collections.Generic;



public class NPCDialogue : MonoBehaviour
{
    public string npcName;
    public List<DialogueEntry> dialogues = new List<DialogueEntry>();

    private int currentIndex = 0;

    private void OnMouseDown()
    {
        if (PlayerMask.Instance == null)
    {
        Debug.LogError("PlayerMask.Instance is null!");
        return;
    }

    Debug.Log("Current mask: " + PlayerMask.Instance.CurrentMask);
    Talk(PlayerMask.Instance.CurrentMask);
    }

    public void Talk(MaskType currentMask)
    {
        if (dialogues.Count == 0) return;

        // Show the current dialogue that matches the mask
        while (currentIndex < dialogues.Count)
        {
            var entry = dialogues[currentIndex];
            currentIndex++;

            if (entry.requiredMask == currentMask || entry.requiredMask == MaskType.None)
            {
                DialogueUI.Instance.ShowDialogue(new List<string> { entry.dialogueText });
                return;
            }
        }

        // Reset index if at the end
        currentIndex = 0;
    }

    public void ResetDialogue()
    {
        currentIndex = 0;
    }
}
