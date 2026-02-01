using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.Events;
using System.Linq;

public class StoryManager : MonoBehaviour
{
    [System.Serializable]
    public class StoryEvent
    {
        public string eventName;

        public GameObject[] showObjects;
        public GameObject[] hideObjects;

        public GameObject dialoguePrefab;
        public string dialogueText;
        public float dialogueDuration = 3f;
        public float nextEventDelay = 2f;

        [Header("Patrol Object 1")]
        public PatrolController patrolObject1;
        public Vector2 patrolPointA1;
        public Vector2 patrolPointB1;

        [Header("Patrol Object 2")]
        public PatrolController patrolObject2;
        public Vector2 patrolPointA2;
        public Vector2 patrolPointB2;

        public bool startPatrolOnEvent;

        [Header("Custom Actions")]
        public UnityEvent onEventStart;
        public UnityEvent onDialogueShow;
        public UnityEvent onEventEnd;
    }

    public GameObject[] allManagedObjects;
    public Transform dialogueParent;
    public StoryEvent[] events;

    private int currentEventIndex;
    private Dictionary<GameObject, bool> objectStates = new();

    void Start()
    {
        DisableAllObjects();
        StartCoroutine(PlayStorySequence());
    }

    IEnumerator PlayStorySequence()
    {
        while (currentEventIndex < events.Length)
        {
            StoryEvent evt = events[currentEventIndex];
            PlayEvent(evt);

            yield return new WaitForSeconds(evt.dialogueDuration + evt.nextEventDelay);

            StopEventPatrols(evt);   // 🔥 STOP AFTER EVENT
            currentEventIndex++;
        }

        StopAllPatrols();           // 🔥 STOP AFTER STORY
        Debug.Log("Story complete!");
    }

    void PlayEvent(StoryEvent evt)
    {
        evt.onEventStart?.Invoke();

        foreach (var obj in evt.showObjects)
            SafeSetActive(obj, true);

        foreach (var obj in evt.hideObjects)
            SafeSetActive(obj, false);

        if (evt.startPatrolOnEvent)
        {
            evt.patrolObject1?.StartPatrol(evt.patrolPointA1, evt.patrolPointB1);
            evt.patrolObject2?.StartPatrol(evt.patrolPointA2, evt.patrolPointB2);
        }

        if (evt.dialoguePrefab && dialogueParent)
        {
            var dialogue = Instantiate(evt.dialoguePrefab, dialogueParent);
            dialogue.GetComponentInChildren<TextMeshProUGUI>().text = evt.dialogueText;
            Destroy(dialogue, evt.dialogueDuration);
        }

        evt.onEventEnd?.Invoke();
    }

    void StopEventPatrols(StoryEvent evt)
    {
        evt.patrolObject1?.StopPatrol();
        evt.patrolObject2?.StopPatrol();
    }

    void StopAllPatrols()
    {
        foreach (var evt in events)
            StopEventPatrols(evt);
    }

    void DisableAllObjects()
    {
        foreach (var obj in allManagedObjects)
        {
            objectStates[obj] = obj.activeSelf;
            obj.SetActive(false);
        }
    }

    void SafeSetActive(GameObject obj, bool active)
    {
        if (!obj || !objectStates.ContainsKey(obj)) return;
        obj.SetActive(active);
        objectStates[obj] = active;
    }
}