using UnityEngine;

public class MaskPickup : MonoBehaviour
{
    public MaskType maskType;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Mask picked up!");
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
