using UnityEngine;

public class MaskInput : MonoBehaviour
{
    [SerializeField] private MaskManager maskManager;

    [Header("Keys")]
    [SerializeField] private KeyCode nextMaskKey = KeyCode.E;
    [SerializeField] private KeyCode prevMaskKey = KeyCode.Q;

    void Update()
    {
        if (Input.GetKeyDown(nextMaskKey))
        {
            maskManager.NextMask();
        }

        if (Input.GetKeyDown(prevMaskKey))
        {
            maskManager.PreviousMask();
        }
    }
}
