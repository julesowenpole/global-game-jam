using UnityEngine;
using UnityEngine.UI;

public class BorderController : MonoBehaviour
{
    public Image borderImage; 

    public Color noneColor = Color.white;
    public Color fireColor = Color.red;
    public Color shadowColor = Color.gray;
    public Color lightColor = Color.yellow;

    public void SetBorder(MaskType mask)
    {
        switch (mask)
        {
            case MaskType.Fire:
                borderImage.color = fireColor;
                break;
            case MaskType.Shadow:
                borderImage.color = shadowColor;
                break;
            case MaskType.Light:
                borderImage.color = lightColor;
                break;
            case MaskType.None:
            default:
                borderImage.color = noneColor;
                break;
        }
    }
}
