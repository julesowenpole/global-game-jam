using UnityEngine;

public class MaskPickup : MonoBehaviour
{
    public MaskType maskType;

    void OnMouseDown()
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Mask picked up!");
            foreach (var blood in FindObjectsOfType<BloodClue>())
                blood.Reveal();
            other.GetComponent<PlayerMask>().EquipMask(maskType);
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

