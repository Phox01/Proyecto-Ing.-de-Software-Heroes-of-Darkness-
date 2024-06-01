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
    private List<string[]> dialogues;
    private string[] lines;
    private int index;
    private Dictionary<int, bool> dialogueStarted;

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
        dialogueStarted = new Dictionary<int, bool>();
        localizationController.OnLocalizationReady += OnLocalizationReady;
    }

    void OnLocalizationReady()
    {
        dialogues = localizationController.GetAllLocalizedLines();
    }

    public void SetupDialogue()
    {
        dialogues = localizationController.GetAllLocalizedLines();
    }

    public bool IsDialogueIndexValid(int dialogueIndex)
    {
        return dialogueIndex >= 0 && dialogueIndex < dialogues.Count && dialogues[dialogueIndex] != null && dialogues[dialogueIndex].Length > 0;
    }

    public void StartDialogue(int dialogueIndex, bool isRepeatable)
    {
        if (!dialogueStarted.ContainsKey(dialogueIndex))
        {
            dialogueStarted[dialogueIndex] = false;
        }

        if (!dialogueStarted[dialogueIndex] || isRepeatable)
        {
            if (IsDialogueIndexValid(dialogueIndex))
            {
                StopAllCoroutines();  
                textComponent.text = string.Empty; 
                lines = dialogues[dialogueIndex];
                dialogueStarted[dialogueIndex] = !isRepeatable;
                StartDialogue();
            }
            else
            {
                Debug.LogError("Invalid dialogue index.");
            }
        }
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

        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSecondsRealtime(textSpeed); 
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
            textComponent.text = string.Empty;  
            dialoguePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}
