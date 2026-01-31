using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class DialogueUI : MonoBehaviour
{
    public static DialogueUI Instance;

    public GameObject dialogueUI;
    public TextMeshProUGUI textBox;
    public float typeSpeed = 0.05f;

    private Coroutine typingCoroutine;
    private Queue<string> dialogueQueue = new Queue<string>();
    private bool isTyping = false;

    void Awake()
    {
        Instance = this;

        if (dialogueUI == null) dialogueUI = gameObject;
        if (textBox == null) textBox = GetComponentInChildren<TextMeshProUGUI>();

        //dialogueUI.SetActive(false);
    }

    void Update()
    {
        if (dialogueUI.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
            {
                // Finish current line instantly
                FinishTyping();
            }
            else
            {
                // Show next line or close dialogueUI
                DisplayNext();
            }
        }
    }

    public void ShowDialogue(List<string> lines)
    {
        dialogueQueue.Clear();
        foreach (var line in lines)
            dialogueQueue.Enqueue(line);

        Debug.Log("setting dialogueUI as active");
        GameObject<Image>.SetActive(true);
        DisplayNext();
    }



    private void DisplayNext()
    {
        if (dialogueQueue.Count == 0)
        {
            dialogueUI.SetActive(false);
            return;
        }

        string line = dialogueQueue.Dequeue();

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(line));
    }

    private IEnumerator TypeText(string line)
    {
        isTyping = true;
        textBox.text = "";
        foreach (char c in line)
        {
            textBox.text += c;
            yield return new WaitForSeconds(typeSpeed);
        }
        isTyping = false;
    }

    private void FinishTyping()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        textBox.text = textBox.text + string.Join("", dialogueQueue); // optional safety
        isTyping = false;
    }
}
