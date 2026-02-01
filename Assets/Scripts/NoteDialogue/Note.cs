using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class NoteEntry
{
    public MaskType requiredMask;
    [TextArea] public string dialogueText;
}
