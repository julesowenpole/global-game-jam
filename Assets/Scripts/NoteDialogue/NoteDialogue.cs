using UnityEngine;
using System.Collections;
using TMPro;

public class NoteDialogue : MonoBehaviour
{
    [Header("UI Element")]
    public GameObject uiElement;  // Parent panel with 2 TextMeshPro children

    [Header("Text Content")]
    [TextArea(3, 5)]
    public string defaultText = "You found note 1!";

    [TextArea(3, 5)]
    public string note2Text = "You found note 2!";

    [Header("Timing")]
    public float displayDuration = 3f;
    public Vector3 popupOffset = new Vector3(0, 2, 0);

    [Header("Proximity")]
    public GameObject player;
    public float maxDistance = 3f;

    [Header("Note Check")]
    public bool note2Collected = false; // Replace maskManager check

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

        if (hideCoroutine != null) StopCoroutine(hideCoroutine);

        // Position UI
        RectTransform rect = uiElement.GetComponent<RectTransform>();
        if (rect)
        {
            Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position + popupOffset);
            rect.position = screenPos;
        }

        // Set the 2 TextMeshPro children
        TextMeshProUGUI[] texts = uiElement.GetComponentsInChildren<TextMeshProUGUI>();
        if (texts.Length >= 2)
        {
            texts[0].gameObject.SetActive(!note2Collected);
            texts[0].text = defaultText;

            texts[1].gameObject.SetActive(note2Collected);
            texts[1].text = note2Text;
        }

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
