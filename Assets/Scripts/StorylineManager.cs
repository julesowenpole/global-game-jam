using UnityEngine;
using System.Collections.Generic;

public class StoryManager : MonoBehaviour
{
    public static StoryManager Instance;

    public HashSet<string> talkedTo = new HashSet<string>();
    public HashSet<string> seenClues = new HashSet<string>();
    public HashSet<MaskType> unlockedMasks = new HashSet<MaskType>();

    void Awake()
    {
        Instance = this;
    }
}

