using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // Drag object to follow
    public float smoothSpeed = 0.125f;
    public Vector3 offset = new Vector3(0, 0, -10f);

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPos = target.position + offset;
            Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed);
            transform.position = smoothedPos;
        }
    }

    // PUBLIC: StoryManager calls this
    public void FollowTarget(Transform newTarget)
    {
        target = newTarget;
        Debug.Log($"Camera now following {newTarget?.name}");
    }

    public void StopFollowing()
    {
        target = null;
    }
}
