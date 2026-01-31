using UnityEngine;

public class MaskManager : MonoBehaviour
{
    [SerializeField] public bool[] masksFound = new bool[5];

    public bool IsMaskFound(int id)
    {
        return id >= 0 && id < 5 && masksFound[id];
    }

    public void SetMaskFound(int id, bool found)
    {
        if (id >= 0 && id < 5) masksFound[id] = found;
    }
}
