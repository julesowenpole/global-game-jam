using UnityEngine;

public class MaskPickup : MonoBehaviour
{
    public MaskType maskType;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Mask picked up!");

            // Update global storage FIRST mask only (id 0)
            Object.FindFirstObjectByType<MaskManager>().SetMaskFound(0, true);
            Debug.Log(Object.FindFirstObjectByType<MaskManager>().IsMaskFound(0));

            other.GetComponent<PlayerMask>().EquipMask(maskType);
            Destroy(gameObject);
        }
    }

}

public enum MaskType
{
    None,
    Fire,
    Shadow,
    Light
}
