using UnityEngine;
using System.Collections;
using TMPro;

public class NPCDialogue : MonoBehaviour
{
    [Header("UI Element")]
    public GameObject uiElement;  // Parent panel with 2 TextMeshPro children

    [Header("Text Content (Inspector Plain Text)")]
    [TextArea(3, 5)]
    public string defaultText = "Find mask 3!";

    [TextArea(3, 5)]
    public string mask3Text = "You've found it! Use fire mask.";

    [Header("Timing")]
    public float displayDuration = 3f;
    public Vector3 popupOffset = new Vector3(0, 2, 0);

    [Header("Proximity")]
    public GameObject player;
    public float maxDistance = 3f;

    [Header("Mask Check")]
    public MaskManager maskManager;

    private Coroutine hideCoroutine;

    void Start()
    {
        if (player == null) player = GameObject.FindWithTag("Player");
        if (uiElement) uiElement.SetActive(false);
    }

    void OnMouseDown()
    {
        if (WithinRange())
        {
            ShowDialogue();
        }
    }

    bool WithinRange()
    {
        if (player == null) return true;
        return Vector2.Distance(player.transform.position, transform.position) <= maxDistance;
    }

    void ShowDialogue()
    {
        if (uiElement == null) return;

        // Stop prior
        if (hideCoroutine != null) StopCoroutine(hideCoroutine);

        // Position UI
        RectTransform rect = uiElement.GetComponent<RectTransform>();
        if (rect)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position + popupOffset);
            rect.position = screenPos;
        }

        // Find 2 TextMeshPro children & set based on mask[3]
        TextMeshProUGUI[] texts = uiElement.GetComponentsInChildren<TextMeshProUGUI>();
        if (texts.Length >= 2)
        {
            bool mask3Found = maskManager != null && maskManager.IsMaskFound(3);

            texts[0].gameObject.SetActive(!mask3Found);
            texts[0].text = defaultText;

            texts[1].gameObject.SetActive(mask3Found);
            texts[1].text = mask3Text;
        }

        // Show & timer
        uiElement.SetActive(true);
        hideCoroutine = StartCoroutine(HideAfterDelay());
    }

    IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        if (uiElement) uiElement.SetActive(false);
        hideCoroutine = null;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, maxDistance);
    }
}
