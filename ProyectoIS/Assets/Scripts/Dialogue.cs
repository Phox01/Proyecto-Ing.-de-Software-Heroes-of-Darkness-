using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public LocalizationController localizationController;
    public GameObject dialoguePanel;
    public float textSpeed;

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
        localizationController.OnLocalizationReady += OnLocalizationReady;
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
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    void StartDialogue()
    {
        if (lines == null || lines.Length == 0)
        {
            Debug.LogError("Lines array is not initialized or empty.");
            return;
        }

        index = 0;
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
            yield return new WaitForSeconds(textSpeed);
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
        }
    }
}
