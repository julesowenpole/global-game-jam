using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class DialogueEntry
{
    public MaskType requiredMask;
    [TextArea] public string dialogueText;
    public Emotion emotion; // optional, for Pepper
}

public enum Emotion
{
    None,
    Sad,
    Nervous,
    Angry,
    Scared
}
