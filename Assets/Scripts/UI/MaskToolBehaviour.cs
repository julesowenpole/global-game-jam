using UnityEngine;
using UnityEngine.UI;

public class MaskToolBehaviour : MonoBehaviour
{
    public MaskManager maskManager;
    public int maskId = 0;
    
    void Update() {
        GetComponent<Image>().enabled = maskManager.IsMaskFound(maskId);
    }
}
