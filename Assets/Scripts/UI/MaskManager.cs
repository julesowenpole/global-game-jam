using UnityEngine;

public class MaskManager : MonoBehaviour
{
    [SerializeField] public bool[] masksFound = new bool[3];

    [Header("Current Mask")]
    [SerializeField] private int currentMaskId = 0;  // ALWAYS 0-2 equipped

    public bool IsMaskFound(int id)
    {
        return id >= 0 && id < 3 && masksFound[id];
    }

    public void SetMaskFound(int id, bool found)
    {
        if (id >= 0 && id < 3) masksFound[id] = found;
    }

    // Get equipped mask ID (always 0-2)
    public int GetCurrentMaskId() => currentMaskId;

    // Cycle to next found mask (wraps around 0-2)
    public void NextMask()
    {
        int start = currentMaskId;
        do
        {
            currentMaskId = (currentMaskId + 1) % 3;
        } while (!masksFound[currentMaskId] && currentMaskId != start);

        Debug.Log($"Switched to mask {currentMaskId}");
    }

    // Previous mask (reverse cycle)
    public void PreviousMask()
    {
        int start = currentMaskId;
        do
        {
            currentMaskId = (currentMaskId + 2) % 3; // -1 mod 3
        } while (!masksFound[currentMaskId] && currentMaskId != start);

        Debug.Log($"Switched to mask {currentMaskId}");
    }

    // Force equip specific mask (if found)
    public bool EquipMask(int id)
    {
        if (id >= 0 && id < 3 && masksFound[id])
        {
            currentMaskId = id;
            return true;
        }
        return false;
    }
}
