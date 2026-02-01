using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Events;  // Custom actions support

// Manages timed story sequences - toggles characters, spawns dialogue with custom text, runs game actions
public class StoryManager : MonoBehaviour
{
    // Serializable class for Inspector-editable story events
    [System.Serializable]
    public class StoryEvent
    {
        public string eventName;           // Debug label for events
        public GameObject[] showCharacters; // Characters to activate (SetActive(true))
        public GameObject[] hideCharacters; // Characters to deactivate (SetActive(false))
        public GameObject dialoguePrefab;   // UI prefab to spawn (null = skip)
        public string dialogueText = "Default dialogue";  // Custom text per event
        public float dialogueDuration = 3f; // How long dialogue stays visible
        public float nextEventDelay = 2f;   // Pause AFTER dialogue before next event

        // Custom actions - drag ANY function here!
        [Header("Custom Actions")]
        public UnityEvent onEventStart;     // Runs when event begins
        public UnityEvent onDialogueShow;   // Runs when dialogue spawns  
        public UnityEvent onEventEnd;       // Runs before next event
    }

    [Header("Characters")]
    public GameObject[] allCharacters;     // Optional: All possible characters (not used directly)

    [Header("Dialogue Parent")]
    public Transform dialogueParent;       // Canvas child where dialogue prefabs spawn

    [Header("Story Events")]
    public StoryEvent[] events;            // Array of events - plays sequentially

    private int currentEventIndex = 0;     // Tracks current story position (0 to events.Length-1)
    private List<GameObject> activeDialogues = new List<GameObject>(); // Tracks spawned dialogues

    // Kicks off the entire story sequence on scene load
    void Start()
    {
        StartCoroutine(PlayStorySequence());
    }

    // Main coroutine - plays events with perfect timing, no player input needed
    IEnumerator PlayStorySequence()
    {
        // Loop through all events until end
        while (currentEventIndex < events.Length)
        {
            PlayEvent(events[currentEventIndex]);  // Execute single event
            // Wait total time: dialogue display + pause before next
            yield return new WaitForSeconds(events[currentEventIndex].dialogueDuration + events[currentEventIndex].nextEventDelay);
            currentEventIndex++;  // Advance to next event
        }
        Debug.Log("Story complete!");  // End marker
    }

    // Executes one story event: character swaps + dialogue spawn + custom actions
    void PlayEvent(StoryEvent evt)
    {
        // Run start actions FIRST (mask switch, camera zoom, etc.)
        evt.onEventStart?.Invoke();

        // Activate specified characters (NPCs, player, etc.)
        foreach (GameObject charObj in evt.showCharacters)
            charObj.SetActive(true);

        // Deactivate specified characters
        foreach (GameObject charObj in evt.hideCharacters)
            charObj.SetActive(false);

        // Spawn dialogue prefab if provided + SET CUSTOM TEXT
        if (evt.dialoguePrefab != null && dialogueParent != null)
        {
            GameObject dialogue = Instantiate(evt.dialoguePrefab, dialogueParent); // Parent to Canvas

            // Find TextMeshPro child and set event-specific text
            TextMeshProUGUI textComp = dialogue.GetComponentInChildren<TextMeshProUGUI>();
            if (textComp != null)
                textComp.text = evt.dialogueText;
            else
                Debug.LogWarning("No TextMeshProUGUI found in dialogue prefab!");

            // Run dialogue-specific actions (sound, effects)
            evt.onDialogueShow?.Invoke();

            activeDialogues.Add(dialogue);  // Track for potential cleanup
            Destroy(dialogue, evt.dialogueDuration);  // Auto-destroy after duration
        }

        // Run end actions BEFORE timing (cleanup, transitions)
        evt.onEventEnd?.Invoke();
    }
}
