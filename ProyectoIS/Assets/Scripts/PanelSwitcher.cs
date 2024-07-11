using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelSwitcher : MonoBehaviour
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
        dialogue.SetupDialogue();
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
        }
        currentPanelIndex++;

        if (currentPanelIndex >= panels.Length)
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            currentPanelIndex = panels.Length - 1;
            if (currentSceneIndex == 5){
            SceneManager.LoadScene(4);}
            else{
                SceneManager.LoadScene(0);
            }
            return;
        }

        if (currentPanelIndex < panels.Length)
        {
            panels[currentPanelIndex].SetActive(true);
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