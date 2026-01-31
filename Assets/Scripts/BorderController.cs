using UnityEngine;
using UnityEngine.UI;

public class BorderController : MonoBehaviour
{
    public Image borderImage;
    public Sprite fireBorder;
    public Sprite shadowBorder;
    public Sprite lightBorder;

    public void SetBorder(MaskType mask)
    {
        switch (mask)
        {
            case MaskType.Fire:
                borderImage.sprite = fireBorder;
                break;
            case MaskType.Shadow:
                borderImage.sprite = shadowBorder;
                break;
            case MaskType.Light:
                borderImage.sprite = lightBorder;
                break;
        }
    }
}
