using UnityEngine;

public class PlayerMask : MonoBehaviour
{
    public MaskType currentMask = MaskType.None;
    public BorderController borderController;

    public void EquipMask(MaskType newMask)
    {
        currentMask = newMask;
        if (borderController != null)
            borderController.SetBorder(currentMask);
    }
}
