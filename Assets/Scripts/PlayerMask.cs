using UnityEngine;

public class PlayerMask : MonoBehaviour
{
        // Global access so other scripts can check the mask
    public static PlayerMask Instance { get; private set; }

    // Tracks the currently equipped mask
    public MaskType CurrentMask { get; private set; } = MaskType.None;

    public BorderController borderController;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void EquipMask(MaskType newMask)
    {
        Debug.Log("EquipMask Running");
        CurrentMask = newMask;
        if (borderController != null)
            borderController.SetBorder(CurrentMask);
    }
}
