using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Events;

// Manages timed story sequences with automatic object state management
public class StoryManager : MonoBehaviour
{
    [System.Serializable]
    public class StoryEvent
    {
        public string eventName;
        public GameObject[] showObjects;     // Objects to SHOW (activates if inactive)
        public GameObject[] hideObjects;     // Objects to HIDE (deactivates if active)
        public GameObject dialoguePrefab;
        public string dialogueText = "Default dialogue";
        public float dialogueDuration = 3f;
        public float nextEventDelay = 2f;

        [Header("Custom Actions")]
        public UnityEvent onEventStart;
        public UnityEvent onDialogueShow;
        public UnityEvent onEventEnd;
    }

    [Header("Master Object List")]
    [Tooltip("ALL objects that get shown/hidden - DISABLED BY DEFAULT at start")]
    public GameObject[] allManagedObjects;  // Master list - auto-disabled on Start

    [Header("Dialogue Parent")]
    public Transform dialogueParent;

    [Header("Story Events")]
    public StoryEvent[] events;

    private int currentEventIndex = 0;
    private List<GameObject> activeDialogues = new List<GameObject>();
    private Dictionary<GameObject, bool> objectStates = new Dictionary<GameObject, bool>(); // Tracks states

    void Start()
    {
        // DISABLE ALL managed objects at start (default hidden)
        DisableAllObjects();
        StartCoroutine(PlayStorySequence());
    }

    // NEW: Auto-disable everything from master list
    void DisableAllObjects()
    {
        foreach (GameObject obj in allManagedObjects)
        {
            if (obj != null)
            {
                objectStates[obj] = obj.activeSelf;  // Remember original state
                obj.SetActive(false);
            }
        }
        Debug.Log($"Disabled {allManagedObjects.Length} managed objects");
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
        evt.onEventStart?.Invoke();

        // SHOW specified objects (only if in master list)
        foreach (GameObject obj in evt.showObjects)
            SafeSetActive(obj, true);

        // HIDE specified objects (only if in master list)
        foreach (GameObject obj in evt.hideObjects)
            SafeSetActive(obj, false);

        if (evt.dialoguePrefab != null && dialogueParent != null)
        {
            GameObject dialogue = Instantiate(evt.dialoguePrefab, dialogueParent);
            TextMeshProUGUI textComp = dialogue.GetComponentInChildren<TextMeshProUGUI>();
            if (textComp != null)
                textComp.text = evt.dialogueText;
            else
                Debug.LogWarning("No TextMeshProUGUI found in dialogue prefab!");

            evt.onDialogueShow?.Invoke();
            activeDialogues.Add(dialogue);
            Destroy(dialogue, evt.dialogueDuration);
        }

        evt.onEventEnd?.Invoke();
    }

    // Safely toggle - only affects master list objects
    void SafeSetActive(GameObject obj, bool active)
    {
        if (obj == null || !System.Array.Exists(allManagedObjects, x => x == obj))
        {
            Debug.LogWarning($"Object {obj?.name} not in master list - ignored");
            return;
        }

        if (objectStates.ContainsKey(obj))
        {
            objectStates[obj] = active;
            obj.SetActive(active);
        }
    }

    // Optional: Restore original states at end
    public void ResetAllObjects()
    {
        foreach (var kvp in objectStates)
            kvp.Key.SetActive(kvp.Value);
    }
}
