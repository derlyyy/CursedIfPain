using System;
using System.Collections;
using System.Collections.Generic;
using Sonity;
using TMPro;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public int index;
    public string[] text;
}

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;
    
    public DialogueLine[] lines;
    public float speedText;
    public float autoSkipDelay;
    public TextMeshProUGUI dialogueText;

    private int index = 0;
    private int textIndex = 0;
    public bool isTyping = false;
    private Coroutine autoSkipCoroutine;

    public GameObject dialoguePanel;

    public SoundEvent typeSound;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        dialogueText.text = string.Empty;
    }

    public void StartDialogue(int dialogueIndex)
    {
        if (!isTyping)
        {
            dialoguePanel.SetActive(true);
            DialogueLine selectedLine = FindLineByIndex(dialogueIndex);
            if (selectedLine != null)
            {
                index = dialogueIndex;
                textIndex = 0;
                dialogueText.text = string.Empty;
                StartCoroutine(TypeLine(selectedLine.text[textIndex]));
            }
            else
            {
                Debug.LogWarning("Dialogue index not found.");
            }
        }
    }

    private DialogueLine FindLineByIndex(int dialogueIndex)
    {
        foreach (DialogueLine line in lines)
        {
            if (line.index == dialogueIndex)
            {
                return line;
            }
        }
        return null;
    }

    private IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = string.Empty;
        
        if (autoSkipCoroutine != null)
        {
            StopCoroutine(autoSkipCoroutine);
        }

        int charIndex = 0;
        foreach (char c in line.ToCharArray())
        {
            dialogueText.text += c;
            if (charIndex % 2 == 0) // play sound every 2 characters
            {
                SoundManager.Instance.Play(typeSound, transform);
            }
            charIndex++;
            yield return new WaitForSeconds(speedText);
        }
        
        isTyping = false;
        autoSkipCoroutine = StartCoroutine(AutoSkip());
    }
    
    private IEnumerator AutoSkip()
    {
        yield return new WaitForSeconds(autoSkipDelay);
        NextText();
    }

    public void SkipTextClick()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = lines[index].text[textIndex];
            isTyping = false;
            autoSkipCoroutine = StartCoroutine(AutoSkip());
        }
        else
        {
            NextText();
        }
    }

    private void NextText()
    {
        if (textIndex < lines[index].text.Length - 1)
        {
            textIndex++;
            StartCoroutine(TypeLine(lines[index].text[textIndex]));
        }
        else
        {
            dialoguePanel.SetActive(false);
        }
    }
}