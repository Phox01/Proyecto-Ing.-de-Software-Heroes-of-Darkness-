using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject[] panels;
    private int currentPanelIndex = 0;
    public Dialogue dialogue;

    void Start()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(i == 0);
        }

        if (dialogue != null)
        {
            dialogue.localizationController.OnLocalizationReady += OnLocalizationReady;
        }
    }

    void OnDestroy()
    {
        if (dialogue != null)
        {
            dialogue.localizationController.OnLocalizationReady -= OnLocalizationReady;
            dialogue.OnDialogueFinished -= HandleDialogueFinished;
        }
    }

    private void OnLocalizationReady()
    {
        if (dialogue != null)
        {
            dialogue.OnDialogueFinished += HandleDialogueFinished;
        }

        StartDialogue(currentPanelIndex);
    }

    private void HandleDialogueFinished()
    {
        if (currentPanelIndex < panels.Length)
        {
            panels[currentPanelIndex].SetActive(false);
            Debug.Log("Desactivando panel: " + currentPanelIndex);
        }

        currentPanelIndex++;

        if (currentPanelIndex >= panels.Length)
        {
            Debug.Log("Se han mostrado todos los paneles.");
            currentPanelIndex = panels.Length - 1;
            return;
        }

        if (currentPanelIndex < panels.Length)
        {
            panels[currentPanelIndex].SetActive(true);
            Debug.Log("Activando panel: " + currentPanelIndex);
            StartDialogue(currentPanelIndex);
        }
    }

    void StartDialogue(int dialogueIndex)
    {
        if (dialogue != null)
        {
            dialogue.StartDialogue(dialogueIndex, false);
        }
    }

}