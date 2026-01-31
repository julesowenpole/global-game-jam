using UnityEngine;

public class BloodClue : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    public void Reveal()
    {

    if (StoryManager.Instance == null)
        Debug.LogError("StoryManager.Instance is NULL");
    spriteRenderer.enabled = true;
    StoryManager.Instance.seenClues.Add("BloodSeen");
    }
}
