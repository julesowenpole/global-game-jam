using UnityEngine;
using UnityEngine.UI;

public class MaskManager : MonoBehaviour
{
    [SerializeField] public bool[] masksFound = new bool[4];

    [Header("Current Mask")]
    [SerializeField] private int currentMaskId = -1;  // -1=none, 0-3 equipped

    [Header("Mask Sprites (Drag THIS object's Image/SpriteRenderer here)")]
    public Image maskImage;     // Single UI Image on this GameObject (if UI)
    public SpriteRenderer maskSpriteRenderer;  // OR SpriteRenderer on this GameObject (if world sprite)

    [Header("Mask Textures (Drag sprites 0-3 here)")]
    public Sprite[] maskTextures = new Sprite[4];

    public bool IsMaskFound(int id)
    {
        return id >= 0 && id < 4 && masksFound[id];
    }

    public void SetMaskFound(int id, bool found)
    {
        if (id >= 0 && id < 4) masksFound[id] = found;
        UpdateDisplay();
    }

    public int GetCurrentMaskId() => currentMaskId;

    public void NextMask()
    {
        int start = currentMaskId;
        do
        {
            currentMaskId = (currentMaskId + 1) % 4;
        } while (!IsMaskFound(currentMaskId) && currentMaskId != start);
        UpdateDisplay();
        Debug.Log($"Switched to mask {currentMaskId}");
    }

    public void PreviousMask()
    {
        int start = currentMaskId;
        do
        {
            currentMaskId = (currentMaskId + 3) % 4;
        } while (!IsMaskFound(currentMaskId) && currentMaskId != start);
        UpdateDisplay();
        Debug.Log($"Switched to mask {currentMaskId}");
    }

    public void EquipMask(int id)
    {
        if (id >= 0 && id < 4 && IsMaskFound(id))
        {
            currentMaskId = id;
            UpdateDisplay();
            Debug.Log($"Equipped mask {id}");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H) && IsMaskFound(0)) EquipMask(0);
        if (Input.GetKeyDown(KeyCode.J) && IsMaskFound(1)) EquipMask(1);
        if (Input.GetKeyDown(KeyCode.K) && IsMaskFound(2)) EquipMask(2);
        if (Input.GetKeyDown(KeyCode.L) && IsMaskFound(3)) EquipMask(3);
    }

    void Start()
    {
        masksFound[0] = true;  // Mask 0 found at start
        EquipMask(0);  // Equip it immediately
        UpdateDisplay();
    }

    void UpdateDisplay()
    {
        // Handle UI Image on this GameObject
        if (maskImage != null)
        {
            if (currentMaskId >= 0 && IsMaskFound(currentMaskId))
            {
                maskImage.enabled = true;
                maskImage.sprite = maskTextures[currentMaskId];
                maskImage.color = Color.white;
            }
            else
            {
                maskImage.enabled = false;
            }
        }

        // Handle SpriteRenderer on this GameObject (non-UI)
        if (maskSpriteRenderer != null)
        {
            if (currentMaskId >= 0 && IsMaskFound(currentMaskId))
            {
                maskSpriteRenderer.enabled = true;
                maskSpriteRenderer.sprite = maskTextures[currentMaskId];
            }
            else
            {
                maskSpriteRenderer.enabled = false;
            }
        }
    }
}
