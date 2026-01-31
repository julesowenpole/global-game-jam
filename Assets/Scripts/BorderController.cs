using UnityEngine;
using UnityEngine.UI;

public class BorderController : MonoBehaviour
{
    public Image borderImage; 

    public Color noneColor = Color.white;
    public Color bloodColor = Color.red;
    public Color empathyColor = Color.gray;
    public Color oracleColor = Color.yellow;

    public void SetBorder(MaskType mask)
    {
        switch (mask)
        {
            case MaskType.Blood:
                borderImage.color = bloodColor;
                break;
            case MaskType.Empathy:
                borderImage.color = empathyColor;
                break;
            case MaskType.Oracle:
                borderImage.color = oracleColor;
                break;
            case MaskType.None:
            default:
                borderImage.color = noneColor;
                break;
        }
        Debug.Log("Mask: " + mask + " Colour: " + borderImage.color);
    }
}
