using System.Collections;
using UnityEngine;
using TMPro;
using System;

[Serializable]
public class DialogueData
{
    public string[] localizationKeys;
}

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public LocalizationController localizationController;
    public GameObject dialoguePanel;
    public float textSpeed;
    public string[][] localizationKeys; // Array of arrays for multiple dialogues

    private string[][] lines;
    private int dialogueIndex;
    private int lineIndex;

    public void Start()
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

        if (localizationKeys == null || localizationKeys.Length == 0)
        {
            Debug.LogError("Localization keys are not assigned or empty.");
            return;
        }

        textComponent.text = string.Empty;
        localizationController.OnLocalizationReady += OnLocalizationReady;
        localizationController.InitializeKeys(localizationKeys); // Pass the keys to the localization controller
    }

    void OnLocalizationReady()
    {
        lines = localizationController.GetLocalizedLines();
        StartDialogue();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (textComponent.text == lines[dialogueIndex][lineIndex])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[dialogueIndex][lineIndex];
            }
        }
    }

    void StartDialogue()
    {
        if (lines == null || lines.Length == 0 || lines[dialogueIndex].Length == 0)
        {
            Debug.LogError("Lines array is not initialized or empty.");
            return;
        }

        dialoguePanel.SetActive(true);
        lineIndex = 0;
        Time.timeScale = 0f; // Pause game time
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        if (lines == null || lines.Length == 0 || lines[dialogueIndex].Length == 0 || lines[dialogueIndex][lineIndex] == null)
        {
            Debug.LogError("Current line is null or lines array is not initialized properly.");
            yield break;
        }

        float elapsed = 0f;
        foreach (char c in lines[dialogueIndex][lineIndex].ToCharArray())
        {
            textComponent.text += c;
            elapsed = 0f;
            while (elapsed < textSpeed)
            {
                elapsed += Time.unscaledDeltaTime; // Use Time.unscaledDeltaTime to ignore the timeScale
                yield return null;
            }
        }
    }

    void NextLine()
    {
        if (lineIndex < lines[dialogueIndex].Length - 1)
        {
            lineIndex++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            dialoguePanel.SetActive(false);
            Time.timeScale = 1f; // Resume game time
            if (dialogueIndex < lines.Length - 1)
            {
                dialogueIndex++;
                StartDialogue();
            }
        }
    }
}
