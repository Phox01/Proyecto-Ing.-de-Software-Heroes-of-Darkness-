using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public LocalizationController localizationController;
    public GameObject dialoguePanel;
    public float textSpeed;
    public List<string[]> dialogues;
    private string[] lines;
    private int index;

    void Start()
    {
        if (textComponent == null)
        {
            Debug.LogError("TextMeshProUGUI component is not assigned.");
            return;
        }

        if (localizationController == null)
        {
            Debug.LogError("LocalizationController is not assigned.");
            return;
        }

        textComponent.text = string.Empty;
        dialogues = new List<string[]>();
        localizationController.OnLocalizationReady += OnLocalizationReady;
    }

    void OnLocalizationReady()
    {
        dialogues = localizationController.GetAllLocalizedLines();
    }

    public bool IsDialogueIndexValid(int dialogueIndex)
    {
        return dialogueIndex >= 0 && dialogueIndex < dialogues.Count && dialogues[dialogueIndex] != null && dialogues[dialogueIndex].Length > 0;
    }

    public void InitializeDialogue(int dialogueIndex)
    {
        if (!IsDialogueIndexValid(dialogueIndex))
        {
            Debug.LogError("Dialogue index is out of range or dialogue is empty.");
            return;
        }

        lines = dialogues[dialogueIndex];
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (lines != null && textComponent.text == lines[index])
            {
                NextLine();
            }
            else if (lines != null)
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        index = 0;
        Time.timeScale = 0f; 
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        if (lines == null || lines.Length == 0 || lines[index] == null)
        {
            Debug.LogError("Current line is null or lines array is not initialized properly.");
            yield break;
        }

        float elapsed = 0f;
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            elapsed = 0f;
            while (elapsed < textSpeed)
            {
                elapsed += Time.unscaledDeltaTime; 
                yield return null;
            }
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            dialoguePanel.SetActive(false);
            Time.timeScale = 1f; 
        }
    }
}
