using UnityEngine;

public class PlayerMask : MonoBehaviour
{
    public MaskType currentMask;
    public BorderController borderController;

    public void EquipMask(MaskType newMask)
    {
        currentMask = newMask;
        borderController.SetBorder(newMask);
    }
}
