using UnityEngine;

public class MaskManager : MonoBehaviour
{
    [SerializeField] public bool[] masksFound = new bool[4];

    [Header("Current Mask")]
    [SerializeField] private int currentMaskId = 0;  // ALWAYS 0-3 equipped

    public bool IsMaskFound(int id)
    {
        return id >= 0 && id < 4 && masksFound[id];
    }

    public void SetMaskFound(int id, bool found)
    {
        if (id >= 0 && id < 4) masksFound[id] = found;
    }

    // Get equipped mask ID (always 0-3)
    public int GetCurrentMaskId() => currentMaskId;

    // Cycle to next found mask (wraps around 0-3)
    public void NextMask()
    {
        int start = currentMaskId;
        do
        {
            currentMaskId = (currentMaskId + 1) % 4;
        } while (!masksFound[currentMaskId] && currentMaskId != start);

        Debug.Log($"Switched to mask {currentMaskId}");
    }

    // Previous mask (reverse cycle)
    public void PreviousMask()
    {
        int start = currentMaskId;
        do
        {
            currentMaskId = (currentMaskId + 3) % 4; // -1 mod 4
        } while (!masksFound[currentMaskId] && currentMaskId != start);

        Debug.Log($"Switched to mask {currentMaskId}");
    }

    // Force equip specific mask (if found)
    public void EquipMask(int id)
    {
        if (id >= 0 && id < 4 && masksFound[id])
        {
            currentMaskId = id;
        }
    }
}
