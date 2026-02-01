using UnityEngine;

public class PlayerMaskVisual : MonoBehaviour
{
    public MaskManager maskManager;
    public SpriteRenderer maskRenderer;
    public Sprite[] maskSprites; // size 4, index = maskId

    void Update()
    {
        int maskId = maskManager.GetCurrentMaskId();

        if (maskId == -1)
        {
            maskRenderer.enabled = false;
        }
        else
        {
            maskRenderer.enabled = true;
            maskRenderer.sprite = maskSprites[maskId];
        }
    }
}
