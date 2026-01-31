using UnityEngine;

public class MaskPickup : MonoBehaviour
{
    public MaskManager maskManager;
    [SerializeField] public int maskId;

    void OnMouseDown()
    {
        // Find the player in the scene
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Debug.Log("Mask picked up!");

            // Update global mask storage
            if (maskManager != null)
            {
                maskManager.SetMaskFound(maskId, true);
                maskManager.EquipMask(maskId);
                Debug.Log(maskManager.IsMaskFound(maskId));
            }

            // Destroy the mask object
            Destroy(gameObject);
        }
    }
}


public enum MaskType
{
    None,
    Blood,      // Paprika
    Oracle,    // Turmeric
    Tracker,   // Basil
    Infrared,  // Lavender
    Empathy    // Pepper
}

