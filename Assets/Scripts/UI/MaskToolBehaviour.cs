using UnityEngine;
using UnityEngine.UI;

public class MaskToolBehaviour : MonoBehaviour
{
    public MaskManager maskManager;
    [SerializeField] public int maskId;

    void Start()
    {
        GetComponent<Image>().enabled = false;
    }

    void Update()
    {
        GetComponent<Image>().enabled = maskManager.IsMaskFound(maskId);
    }
}
