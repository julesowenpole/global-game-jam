using UnityEngine;
using System.Collections;
using System.Collections.Generic;  

public class StoryManager : MonoBehaviour
{
    [System.Serializable]
    public class StoryEvent
    {
        public string eventName;
        public GameObject[] showCharacters;
        public GameObject[] hideCharacters;
        public GameObject dialoguePrefab;
        public float dialogueDuration = 3f;
        public float nextEventDelay = 2f;  // Wait after dialogue before next
    }

    [Header("Characters")]
    public GameObject[] allCharacters;

    [Header("Dialogue Parent")]
    public Transform dialogueParent;

    [Header("Story Events")]
    public StoryEvent[] events;

    private int currentEventIndex = 0;
    private List<GameObject> activeDialogues = new List<GameObject>();

    void Start()
    {
        StartCoroutine(PlayStorySequence());
    }

    IEnumerator PlayStorySequence()
    {
        while (currentEventIndex < events.Length)
        {
            PlayEvent(events[currentEventIndex]);
            yield return new WaitForSeconds(events[currentEventIndex].dialogueDuration + events[currentEventIndex].nextEventDelay);
            currentEventIndex++;
        }
        Debug.Log("Story complete!");
    }

    void PlayEvent(StoryEvent evt)
    {
        // Toggle characters
        foreach (GameObject charObj in evt.showCharacters)
            charObj.SetActive(true);
        foreach (GameObject charObj in evt.hideCharacters)
            charObj.SetActive(false);

        // Instantiate dialogue
        if (evt.dialoguePrefab != null && dialogueParent != null)
        {
            GameObject dialogue = Instantiate(evt.dialoguePrefab, dialogueParent);
            activeDialogues.Add(dialogue);
            Destroy(dialogue, evt.dialogueDuration);
        }
    }
}
