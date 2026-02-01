using UnityEngine;

public class TeleportController : MonoBehaviour
{
    [Header("Teleport Target")]
    public Transform targetPosition;  // Drag empty GameObject at destination
    
    [Header("Options")]
    public bool deactivateDuringTeleport = true;  // Fixes physics glitches on Rigidbody
    public float activationDelay = 0.01f;  // Brief pause for stability

    public void Teleport()
    {
        if (targetPosition == null)
        {
            Debug.LogWarning("No target position set!");
            return;
        }

        // Optional: Deactivate to reset physics/colliders
        if (deactivateDuringTeleport)
            gameObject.SetActive(false);

        // Instant position set (preserves rotation if desired)
        transform.position = targetPosition.position;

        // Reactivate
        if (deactivateDuringTeleport)
        {
            gameObject.SetActive(true);
            // Extra frame for stability
            StartCoroutine(Reactivate());
        }
    }

    // Smooth reactivate
    System.Collections.IEnumerator Reactivate()
    {
        yield return new WaitForSeconds(activationDelay);
    }

    // Overload: Teleport to Vector3 coords
    public void Teleport(Vector3 position)
    {
        transform.position = position;
    }

    // Trigger-based (e.g., pressure plate)
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  // Customize tag
            Teleport();
    }
}
