using UnityEngine;

public class MaskPickup : MonoBehaviour
{
    public MaskType maskType;
    [SerializeField] public int maskId;

    void OnMouseDown()
    {
        // Find the player in the scene
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Debug.Log("Mask picked up!");

            // Reveal all blood clues
            foreach (var blood in FindObjectsOfType<BloodClue>())
                blood.Reveal();

            // Update global mask storage
            MaskManager maskManager = Object.FindFirstObjectByType<MaskManager>();
            if (maskManager != null)
            {
                maskManager.SetMaskFound(maskId, true);
                Debug.Log(maskManager.IsMaskFound(0));
            }

            // Equip mask on player
            PlayerMask playerMask = player.GetComponent<PlayerMask>();
            if (playerMask != null)
                playerMask.EquipMask(maskType);

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

